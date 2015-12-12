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
    public partial class OutputForm : Form
    {
        NeuralNetwork Nn;
        int MaxX, MaxY;
        int BoundaryResolution = 60;
        bool Binary;
        public OutputForm(NeuralNetwork n, int maxX, int maxY, bool binary = false)
        {
            MaxX = maxX;
            MaxY = maxY;
            Binary = binary;
            Nn = n;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for(int x = 0; x < this.Width; x += this.Width / BoundaryResolution)
            {
                for (int y = 0; y < this.Height; y += this.Height / BoundaryResolution)
                {
                    var output = Nn.Fire(new float[2] { ((float)x / this.Width) * MaxX, ((float)y / this.Height) * MaxY });
                    SolidBrush brush = null;
                    if (Binary)
                    {
                        brush = new SolidBrush(Color.FromArgb(255, (output[0] > .5 ? 255:0), 50, 50));
                    }
                    else
                    {
                        brush = new SolidBrush(Color.FromArgb(255, ((int)(255 * output[0])), 50, 50));
                    }
                    e.Graphics.FillEllipse(brush, x - this.Width / BoundaryResolution/2, y - this.Height / BoundaryResolution/2, (int)(1.5*this.Width / BoundaryResolution), (int)(1.5 * this.Height / BoundaryResolution));
                }
            }            

            e.Graphics.FillEllipse(Brushes.Black, -5, -5, 10, 10);
            e.Graphics.FillEllipse(Brushes.Red, this.Width - 20, -5, 10, 10);
            e.Graphics.FillEllipse(Brushes.Red, -5, this.Height - 45, 10, 10);
            e.Graphics.FillEllipse(Brushes.Green, this.Width - 20, this.Height - 45, 10, 10);
        }
    }
}
