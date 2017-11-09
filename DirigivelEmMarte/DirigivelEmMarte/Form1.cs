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
                                caminhoPilha.empilhar(caminhoMatriz[cidadeAtual, cidadeDestino]);
                                cidadeAtual = cidadeDestino;
                                cidadeDestino = 0;
                                break;
                            }
                        }

                        if (cidadeFinal == caminhoPilha.oTopo().cidadeDestino)
                        {
                            //Achei
                            //pb.Invalidate();
                            caminhoPilha.desempilhar();
                            cidadeAtual = caminhoPilha.oTopo().cidadeAtual;
                            cidadeDestino = caminhoPilha.oTopo().cidadeDestino;

                        }
                        else
                        {
                            if (cidadeDestino >= 23)
                            {
                                caminhoPilha.desempilhar();
                                cidadeAtual = caminhoPilha.oTopo().cidadeAtual;
                                cidadeDestino = caminhoPilha.oTopo().cidadeDestino;

                            }
                        }


                    } while (!achou && !caminhoPilha.estaVazia());
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

        }
    }

}
