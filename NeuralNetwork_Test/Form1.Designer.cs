namespace NeuralNetwork_Test
{
    partial class Form1
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
            this.newLayer = new System.Windows.Forms.Button();
            this.trainButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newLayer
            // 
            this.newLayer.Location = new System.Drawing.Point(12, 12);
            this.newLayer.Name = "newLayer";
            this.newLayer.Size = new System.Drawing.Size(126, 44);
            this.newLayer.TabIndex = 1;
            this.newLayer.Text = "New Layer";
            this.newLayer.UseVisualStyleBackColor = true;
            this.newLayer.Click += new System.EventHandler(this.newLayer_Click);
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(145, 13);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(112, 43);
            this.trainButton.TabIndex = 2;
            this.trainButton.Text = "Train";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.train_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 909);
            this.Controls.Add(this.trainButton);
            this.Controls.Add(this.newLayer);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button newLayer;
        private System.Windows.Forms.Button trainButton;
    }
}

