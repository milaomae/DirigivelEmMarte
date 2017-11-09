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

        private Caminho[,] caminhoMatriz;
        private Caminho caminho;
        private int cidadeInicial, cidadeFinal, caso;
        private PilhaLista<Caminho> caminhoPilha = new PilhaLista<Caminho>();
        PilhaLista<Caminho> caminhoEncontrado = new PilhaLista<Caminho>();


        ListaSimples<Cidade> listaCidades = new ListaSimples<Cidade>();
        //ListaSimples<caminhoPintado> listaCaminhosP = new ListaSimples<caminhoPintado>();
        

        public Form1()
        {
            InitializeComponent();
            caminhoMatriz = new Caminho[23, 23];
        }

        private void btn_ler_caminhos(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader arquivo = new StreamReader(fileDialog.FileName);
                String linha = arquivo.ReadLine();


                while ((linha = arquivo.ReadLine()) != null)
                {

                    int cidadeAtual = Convert.ToInt32(linha.Substring(0, 2).Trim());
                    int cidadeDestino = Convert.ToInt32(linha.Substring(2, 3).Trim());
                    int tempo = Convert.ToInt32(linha.Substring(5, 6).Trim());
                    double preco = Convert.ToDouble(linha.Substring(11, 4).Trim());
                    double distancia = Convert.ToDouble(linha.Substring(15).Trim());

                    caminhoMatriz[cidadeAtual, cidadeDestino] = new Caminho(cidadeAtual, cidadeDestino, tempo, preco, distancia);

                }

                arquivo.Close();
            }
        }

        public void buscarCaminho()
        {
            int cidadeAtual, cidadeDestino;
            cidadeAtual = cidadeInicial;
            cidadeDestino = 0;
            bool achou = false;

            switch (caso)
            {
                case 0: //Busca caminho sem nenhum filtro
                    do
                    {
                        for (; cidadeDestino < 23; cidadeDestino++)
                        {
                            if (caminhoMatriz[cidadeAtual, cidadeDestino] != null)
                            {
                                caminhoPilha.Empilhar(caminhoMatriz[cidadeAtual, cidadeDestino]);
                                cidadeAtual = cidadeDestino;
                                cidadeDestino = 0;
                                break;
                            }
                        }

                        if (cidadeFinal == caminhoPilha.oTopo().CidadeDestino)
                        {
                            //Achei
                            pbAreaDesenho.Invalidate();
                            caminhoPilha.Desempilhar();
                            cidadeAtual = caminhoPilha.oTopo().CidadeAtual;
                            cidadeDestino = caminhoPilha.oTopo().CidadeDestino;

                        }
                        else
                        {
                            if (cidadeDestino >= 23)
                            {
                                caminhoPilha.Desempilhar();
                                cidadeAtual = caminhoPilha.oTopo().CidadeAtual;
                                cidadeDestino = caminhoPilha.oTopo().CidadeDestino;

                            }
                        }


                    } while (!achou && !caminhoPilha.EstaVazia());
                    break;

                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                default:
                    break;
            }

            pbAreaDesenho.SizeMode = PictureBoxSizeMode.StretchImage;
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
               
        }

        private void pbAreaDesenho_Paint(object sender, PaintEventArgs e)
        {
            int cidadeAtual, cidadeDestino, xAtual, yAtual, xDestino, yDestino;
            xAtual = 0;
            yAtual = 0;
            xDestino = 0;
            yDestino = 0;
            Graphics g = e.Graphics;    // acessa contexto gráfico

            while (!caminhoEncontrado.EstaVazia())
            {

                listaCidades.IniciarPercursoSequencial();
                cidadeAtual = caminhoEncontrado.oTopo().CidadeAtual; // Não sei se é assim que pega, estou fazendo no notepad
                cidadeDestino = caminhoEncontrado.oTopo().CidadeDestino;

                while (!listaCidades.ChegouNoFim())
                {
                    if (listaCidades.Atual.Info.Cod == cidadeAtual)
                    {
                        xAtual = listaCidades.Atual.Info.CoordX;
                        yAtual = listaCidades.Atual.Info.CoordY;

                    }
                    else
                    {
                        if (listaCidades.Atual.Info.Cod == cidadeDestino)
                        {
                            xDestino = listaCidades.Atual.Info.CoordX;
                            yDestino = listaCidades.Atual.Info.CoordY;

                        }
                    }
                }

                //Pen pen = new Pen(corDesenho); //Alguma cor
                g.DrawLine(new Pen(Color.Black), xAtual, yAtual, // ponto inicial
                        xDestino, yDestino); // ponto final
                                             //Aqui você desenha a linha do xAtual e yAtual até o xDestino e yDestino
                caminhoEncontrado.Desempilhar();
            }
        }

    }

}
