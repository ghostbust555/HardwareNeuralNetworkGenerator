using NeuralNetwork_Test.Neural_Networks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork_Test
{
    public partial class NetworkDesigner : Form
    {
        NeuralNetwork NN = new NeuralNetwork();
        int Cutoff = 0;
        List<List<float>> trainingData = new List<List<float>>();
        private bool PlacingNeuron = false;
        public List<List<Point>> Layers = new List<List<Point>>();
        int size = 30;
        int gridsize = 40;
        private int MaxVectorsInLayer = 10;        

        public NetworkDesigner()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (PlacingNeuron)
            {
                Point local = this.PointToClient(Cursor.Position);
                e.Graphics.FillEllipse(Brushes.Red, 
                    gridsize * ((local.X - size / 2 + gridsize / 2) /gridsize),
                    gridsize * ((local.Y - size / 2 + gridsize / 2) / gridsize),
                    size, size);
            }

            for (int j = 0; j < Layers.Count; j++)
            {
                for (int i = 0; i < Layers[j].Count; i++)
                {
                    var vn = Layers[j][i];
                    Brush brush = Brushes.Aqua;
                    if (j == 0)
                    {
                        brush = Brushes.Red;
                    }
                    else if (j == Layers.Count-1)
                    {
                        brush = Brushes.Blue;
                    }

                    var yOffset = (MaxVectorsInLayer - Layers[j].Count) * gridsize / 2;
                    var xOffset = gridsize * j;
                    e.Graphics.FillEllipse(brush, vn.X+ xOffset, vn.Y+ yOffset, size, size);
                    e.Graphics.DrawEllipse(Pens.Black, vn.X + xOffset, vn.Y + yOffset, size, size);
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    e.Graphics.DrawString(i + 1 + "", this.Font, Brushes.Black, vn.X + size / 2 + xOffset, vn.Y + size / 2 + yOffset, sf);
                }
            }
        }

        private void newNeuron_Click(object sender, EventArgs e)
        {
            PlacingNeuron = true;
            this.Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (PlacingNeuron)
            //{
            //    PlacingNeuron = false;
            //    var point = new Point(
            //        gridsize * ((e.X - size / 2 + gridsize / 2) / gridsize),
            //        gridsize * ((e.Y - size / 2 + gridsize / 2) / gridsize));

            //    if(!VisualNeurons.Contains(point))
            //        VisualNeurons.Add(point);
            //}
        }

        private void newLayer_Click(object sender, EventArgs e)
        {
            var f = new InputLayerSizeForm();
            var result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                var layer = new List<Point>();
                int vectorCount = f.VectorCount;
                for(int i = 0; i < vectorCount; i++)
                {
                    layer.Add(new Point(gridsize * 2, gridsize * (i + 1)));
                }
                Layers.Add(layer);
                this.Invalidate();
            }
        }

        private void train_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog fd = new OpenFileDialog();
            fd.ValidateNames = true;
            fd.DefaultExt = ".csv";
            fd.Filter = "training data|*.csv";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                path = fd.FileName;

                trainingData = new List<List<float>>(); 

                System.IO.StreamReader file = new System.IO.StreamReader(path);
                string line = "";
                
                while ((line = file.ReadLine()) != null)
                {
                    var rowstring = line.Split(',');
                    var row = new List<float>();
                    foreach(var number in rowstring)
                    {
                        row.Add(float.Parse(number));
                    }

                    trainingData.Add(row);
                }

                file.Close();

                NN = new NeuralNetwork();
                var r = new Random();

                int visualLayerIndex = 0;
                foreach (var visualLayer in Layers)
                {
                    var thisLayer = new List<Neuron>();

                    foreach (var visualNeuron in visualLayer)
                    {
                        if (visualLayerIndex == 0)
                        {
                            thisLayer.Add(new InputNeuron());
                        }
                        else if (visualLayerIndex == Layers.Count - 1)
                        {
                            var last = NN.Layers[visualLayerIndex - 1];
                            thisLayer.Add(new OutputNeuron(r, ref last));
                            foreach (Neuron n in last)
                            {
                                n.Children = thisLayer;
                            }
                        }
                        else
                        {
                            var last = NN.Layers[visualLayerIndex - 1];
                            thisLayer.Add(new HiddenNeuron(r, ref last));
                            foreach (Neuron n in last)
                            {
                                n.Children = thisLayer;
                            }
                        }
                    }

                    NN.AddLayer(thisLayer);
                    visualLayerIndex++;
                }

                Cutoff = NN.Layers[0].Count;

                for (var i = 0; i < 50000; i++)
                {
                    foreach (var row in trainingData)
                    {
                        var thisTrainingSetIn = new List<float>();
                        var thisTrainingSetOut = new List<float>();
                        int count = 0;
                        foreach (var entry in row) {
                            if(count < Cutoff)
                            {
                                thisTrainingSetIn.Add(entry);
                            }
                            else
                            {
                                thisTrainingSetOut.Add(entry);
                            }

                            count++;
                        }
                        NN.Train(thisTrainingSetIn.ToArray(), thisTrainingSetOut.ToArray());
                    }                    
                }

                Program.VHDL(NN);

                //new OutputForm(nn, 1, 1, true).Show();
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            var o = new List<float[]>();
            foreach (var row in trainingData)
            {
                var thisTrainingSetIn = new List<float>();
                int count = 0;
                foreach (var entry in row)
                {
                    if (count < Cutoff)
                    {
                        thisTrainingSetIn.Add(entry);
                    }

                    count++;
                }

                o.Add(NN.Fire(thisTrainingSetIn.ToArray()));
            }

            var vf = new VectorOutputForm(o);
            vf.ShowDialog();
        }
    }
}
