namespace NeuralNetwork_Test
{
    partial class inputLayerSizeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.vectorCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.vectorCountUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "How many vectors in this layer?";
            // 
            // vectorCountUpDown
            // 
            this.vectorCountUpDown.Location = new System.Drawing.Point(22, 41);
            this.vectorCountUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.vectorCountUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.vectorCountUpDown.Name = "vectorCountUpDown";
            this.vectorCountUpDown.Size = new System.Drawing.Size(225, 26);
            this.vectorCountUpDown.TabIndex = 1;
            this.vectorCountUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 82);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 34);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(138, 82);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(123, 34);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // inputLayerSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 128);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.vectorCountUpDown);
            this.Controls.Add(this.label1);
            this.Name = "inputLayerSizeForm";
            this.Text = "inputLayerSizeForm";
            ((System.ComponentModel.ISupportInitialize)(this.vectorCountUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown vectorCountUpDown;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}