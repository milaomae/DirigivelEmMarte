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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnTracarCam = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_cidadeSaida = new System.Windows.Forms.ComboBox();
            this.cb_cidadeDestino = new System.Windows.Forms.ComboBox();
            this.cd_melhorCaminho = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_tracarMelhores = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DirigivelEmMarte.Properties.Resources.Mapa_Marte_sem_rotas;
            this.pictureBox1.Location = new System.Drawing.Point(43, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(631, 355);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(631, 355);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(631, 355);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // btnTracarCam
            // 
            this.btnTracarCam.Location = new System.Drawing.Point(374, 411);
            this.btnTracarCam.Margin = new System.Windows.Forms.Padding(4);
            this.btnTracarCam.Name = "btnTracarCam";
            this.btnTracarCam.Size = new System.Drawing.Size(236, 53);
            this.btnTracarCam.TabIndex = 1;
            this.btnTracarCam.Text = "Traçar Caminho";
            this.btnTracarCam.UseVisualStyleBackColor = true;
            this.btnTracarCam.Click += new System.EventHandler(this.btnTracarCam_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 392);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cidade de saída:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cidade destino:";
            // 
            // cb_cidadeSaida
            // 
            this.cb_cidadeSaida.FormattingEnabled = true;
            this.cb_cidadeSaida.Location = new System.Drawing.Point(162, 411);
            this.cb_cidadeSaida.Name = "cb_cidadeSaida";
            this.cb_cidadeSaida.Size = new System.Drawing.Size(155, 24);
            this.cb_cidadeSaida.TabIndex = 4;
            // 
            // cb_cidadeDestino
            // 
            this.cb_cidadeDestino.FormattingEnabled = true;
            this.cb_cidadeDestino.Location = new System.Drawing.Point(162, 467);
            this.cb_cidadeDestino.Name = "cb_cidadeDestino";
            this.cb_cidadeDestino.Size = new System.Drawing.Size(155, 24);
            this.cb_cidadeDestino.TabIndex = 5;
            // 
            // cd_melhorCaminho
            // 
            this.cd_melhorCaminho.FormattingEnabled = true;
            this.cd_melhorCaminho.Location = new System.Drawing.Point(162, 522);
            this.cd_melhorCaminho.Name = "cd_melhorCaminho";
            this.cd_melhorCaminho.Size = new System.Drawing.Size(155, 24);
            this.cd_melhorCaminho.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 503);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Melhor caminho por:";
            // 
            // btn_tracarMelhores
            // 
            this.btn_tracarMelhores.Location = new System.Drawing.Point(374, 485);
            this.btn_tracarMelhores.Name = "btn_tracarMelhores";
            this.btn_tracarMelhores.Size = new System.Drawing.Size(236, 53);
            this.btn_tracarMelhores.TabIndex = 8;
            this.btn_tracarMelhores.Text = "Traçar todos melhores caminhos";
            this.btn_tracarMelhores.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 589);
            this.Controls.Add(this.btn_tracarMelhores);
            this.Controls.Add(this.cd_melhorCaminho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_cidadeDestino);
            this.Controls.Add(this.cb_cidadeSaida);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTracarCam);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(737, 627);
            this.MinimumSize = new System.Drawing.Size(737, 627);
            this.Name = "Form1";
            this.Text = "Dirigível em Marte";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnTracarCam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_cidadeSaida;
        private System.Windows.Forms.ComboBox cb_cidadeDestino;
        private System.Windows.Forms.ComboBox cd_melhorCaminho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_tracarMelhores;
    }
}

