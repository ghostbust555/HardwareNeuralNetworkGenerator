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
    public partial class InputLayerSizeForm : Form
    {
        public int VectorCount = 10;

        public InputLayerSizeForm()
        {
            InitializeComponent();
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            VectorCount = (int)vectorCountUpDown.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
