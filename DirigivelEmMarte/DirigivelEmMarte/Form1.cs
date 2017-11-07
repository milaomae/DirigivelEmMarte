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
        
        ListaSimples<Cidade> listaCidades = new ListaSimples<Cidade>();
        ListaSimples<caminhoPintado> listaCaminhosP = new ListaSimples<caminhoPintado>();
        

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnTracarCam_Click(object sender, EventArgs e)
        {
            //usuário traça caminhos específicos 
            if ((!(cb_cidadeSaida.Text == "") && !(cb_cidadeDestino.Text == "")) 
                    || (!(cb_cidadeSaida.Text == "") && cb_cidadeDestino.Text == "") 
                    || (cb_cidadeSaida.Text == "" && !(cb_cidadeDestino.Text == "")))
            {
                if (!(cb_cidadeSaida.Text == "") && !(cb_cidadeDestino.Text == ""))
                {
                    //usuário escolhe um melhor caminho a ser traçado
                    if (!(cb_melhorCaminho.Text == ""))
                    {
                        //não há um melhor caminho a ser traçado
                    }
                    else
                    {
                        //caso tenha um melhor caminho a tracar
                    }
                }
                else
                    MessageBox.Show("Por favor preencha a cidade saída ou destino");
                
            }
            else
            {

            }

            //escolher saída e destino
            //recolher dados dos caminhos..pilha
            // pegar cod de cidades intermediárias do caminho encontrado..
            // listaCaminhosP.inserirAoFim(new caminhoPintado());

            //definir caminho pelo preço/distancia/tempo

            //para traçar todos os caminhos --> pegar do leitura de caminhos..

            //4096 x 2048 coordenadas originais

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

                //dividindo as coordenadas pelo tamanho do mapa original obtemos uma razão que indica
                //aonde o ponto se encontra proporcionalmente ao tamanho do mapa, dessa forma, quando 
                //quisermos encontrar a coordenada, multiplicamos o valor desta pela dimensão do mapa 
                //exibido na tela
                cordx =  (Convert.ToInt32(linha.Substring(19, 4).Trim()))/4096;
                cordy = (Convert.ToInt32(linha.Substring(23).Trim()))/2048;

                listaCidades.InserirAntesDoInicio(new Cidade(codigo, cidade, cordx, cordy));
            }

            arq.Close();
        }
                 

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
                Graphics g = e.Graphics;
                //caminhoPintado é uma listaSimples que armazenará os caminhos a serem mostrados
                while (!listaCaminhosP.ChegouNoFim())
                {
                    caminhoPintado cp = listaCaminhosP.Atual.Info;
                    cp.DesenharCaminho(Color.Red, g);
                }
            
        }
        
    }
}
