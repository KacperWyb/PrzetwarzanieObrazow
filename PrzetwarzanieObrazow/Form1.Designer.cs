
namespace PrzetwarzanieObrazow
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ConvertToGrayscale = new System.Windows.Forms.Button();
            this.AdjustBrightnessContrastGammaButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(69, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(682, 294);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(42, 335);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ConvertToGrayscale
            // 
            this.ConvertToGrayscale.Location = new System.Drawing.Point(159, 335);
            this.ConvertToGrayscale.Name = "ConvertToGrayscale";
            this.ConvertToGrayscale.Size = new System.Drawing.Size(187, 23);
            this.ConvertToGrayscale.TabIndex = 2;
            this.ConvertToGrayscale.Text = "Konwersja na skalę szarności";
            this.ConvertToGrayscale.UseVisualStyleBackColor = true;
            this.ConvertToGrayscale.Click += new System.EventHandler(this.ConvertToGrayscale_Click);
            // 
            // AdjustBrightnessContrastGammaButton
            // 
            this.AdjustBrightnessContrastGammaButton.Location = new System.Drawing.Point(378, 335);
            this.AdjustBrightnessContrastGammaButton.Name = "AdjustBrightnessContrastGammaButton";
            this.AdjustBrightnessContrastGammaButton.Size = new System.Drawing.Size(188, 23);
            this.AdjustBrightnessContrastGammaButton.TabIndex = 3;
            this.AdjustBrightnessContrastGammaButton.Text = "Jasnosc Kontrast Gamma";
            this.AdjustBrightnessContrastGammaButton.UseVisualStyleBackColor = true;
            this.AdjustBrightnessContrastGammaButton.Click += new System.EventHandler(this.AdjustBrightnessContrastGammaButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AdjustBrightnessContrastGammaButton);
            this.Controls.Add(this.ConvertToGrayscale);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button ConvertToGrayscale;
        private System.Windows.Forms.Button AdjustBrightnessContrastGammaButton;
    }
}

