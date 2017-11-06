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

namespace DirigivelEmMarte
{
    public partial class Form1 : Form
    {
        //ListaSimplesP<Cidade> listaCidades = new ListaSimplesP<Cidade>;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnTracarCam_Click(object sender, EventArgs e)
        {
            


        }

        public void LerCidades()
        {

            StreamReader arq = new StreamReader("C:\\Users\\u17393\\Desktop\\DirigivelEmMarte");
            String linha = "";
            int codigo = -1;
            string cidade = "";
            int cordx, cordy = 0;
            while (!arq.EndOfStream)
            {
                linha = arq.ReadLine();
                codigo = Convert.ToInt32(linha.Substring(0, 2).Trim());
                cidade = linha.Substring(2, 17).Trim();
                cordx =  Convert.ToInt32(linha.Substring(19, 4).Trim());
                cordy = Convert.ToInt32(linha.Substring(23).Trim());

                //listaCidades.InserirAntesDoInicio(new Cidade(codigo, cidade, cordx, cordy));
            }

            arq.Close();
        }

        public void DesenharCaminho(PaintEventArgs e)
        {
            Color cor = Color.Red;
            Pen pen = new Pen(cor);

            Graphics g = e.Graphics;
            g.DrawLine(pen, new Point(10, 20), new Point(500, 1000)); 
        }

        private void btnTracarCam_Paint(object sender, PaintEventArgs e)
        {
            DesenharCaminho(e);
        }
    }
}
