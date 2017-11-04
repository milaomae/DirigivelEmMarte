namespace DirigivelEmMarte
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
            this.btnAbrirCaminhos = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnAbrirCaminhos
            // 
            this.btnAbrirCaminhos.Location = new System.Drawing.Point(3, 2);
            this.btnAbrirCaminhos.Name = "btnAbrirCaminhos";
            this.btnAbrirCaminhos.Size = new System.Drawing.Size(101, 23);
            this.btnAbrirCaminhos.TabIndex = 0;
            this.btnAbrirCaminhos.Text = "Ler caminhos";
            this.btnAbrirCaminhos.UseVisualStyleBackColor = true;
            this.btnAbrirCaminhos.Click += new System.EventHandler(this.btn_ler_caminhos);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 330);
            this.Controls.Add(this.btnAbrirCaminhos);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAbrirCaminhos;
        private System.Windows.Forms.OpenFileDialog fileDialog;
    }
}

