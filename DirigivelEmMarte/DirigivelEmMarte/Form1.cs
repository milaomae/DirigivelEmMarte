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
        
        //ListaSimples<Cidade> listaCidades = new ListaSimples<Cidade>;
        ListaSimples<caminhoPintado> listaCaminhosP = new ListaSimples<caminhoPintado>;
        
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnTracarCam_Click(object sender, EventArgs e)
        {
            
            //escolher saída e destino
            //recolher dados dos caminhos..pilha
            // pegar cod de cidades intermediárias do caminho encontrado..
            // listaCaminhosP.inserirAoFim(new caminhoPintado());
            
            //definir caminho pelo preço/distancia/tempo
            
            //para traçar todos os caminhos --> pegar do leitura de caminhos..

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

        
        //private void btnTracarCam_Paint(object sender, PaintEventArgs e)
        //{
        //    DesenharCaminho(e);
        //}
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;            
            //caminhoPintado é uma listaSimples que armazenará os caminhos a serem mostrados
            while(!listaCaminhosP.ChegouNoFim()){
                  caminhoPintado cp = listaCaminhosP.Atual.Info;
                  cp.DesenharCaminho(Color.Red, g);
            }
        }
    }
}
