using NeuralNetwork_Test.Neural_Networks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork_Test
{
    public partial class NetworkDesigner : Form
    {
        
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
            var nn = new NeuralNetwork();
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
                    else if(visualLayerIndex == Layers.Count - 1)
                    {
                        var last = nn.Layers[visualLayerIndex-1];
                        thisLayer.Add(new OutputNeuron(r, ref last));
                        foreach (Neuron n in last)
                        {
                            n.Children = thisLayer;
                        }
                    }
                    else
                    {
                        var last = nn.Layers[visualLayerIndex - 1];
                        thisLayer.Add(new HiddenNeuron(r, ref last));
                        foreach (Neuron n in last)
                        {
                            n.Children = thisLayer;
                        }
                    }
                }

                nn.AddLayer(thisLayer);
                visualLayerIndex++;
            }
            
            for (var i = 0; i < 50000; i++)
            {
                nn.Train(new float[2] { 0, 0 }, new float[1] { 0 });
                nn.Train(new float[2] { 0, 1 }, new float[1] { 1 });
                nn.Train(new float[2] { 1, 0 }, new float[1] { 1 });
                nn.Train(new float[2] { 1, 1 }, new float[1] { 0 });
            }

            Program.VHDL(nn);

            new OutputForm(nn,1,1,true).Show();
        }
    }
}
