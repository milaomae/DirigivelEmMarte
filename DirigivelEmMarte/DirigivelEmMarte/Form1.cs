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


        //cor de desenho de caminho padrão
        Color corDesenho = Color.Red;

        public Form1()
        {
            InitializeComponent();
            caminhoMatriz = new Caminho[23, 23];
        }        

        public void LerCaminhos()
        {
            StreamReader arquivo = new StreamReader("C:\\Users\\comae\\Desktop\\DirigivelEmMarte\\CaminhosEntreCidadesMarte.txt");
            String linha = "";
            int cidadeAtual, cidadeDestino, tempo;
            double preco, distancia;

            while (!arquivo.EndOfStream)
            {
                linha = arquivo.ReadLine();
                cidadeAtual = Convert.ToInt32(linha.Substring(0, 2).Trim());
                cidadeDestino = Convert.ToInt32(linha.Substring(2, 3).Trim());
                tempo = Convert.ToInt32(linha.Substring(5, 6).Trim());
                preco = Convert.ToDouble(linha.Substring(11, 4).Trim());
                distancia = Convert.ToDouble(linha.Substring(15).Trim());

                caminhoMatriz[cidadeAtual, cidadeDestino] = new Caminho(cidadeAtual, cidadeDestino, tempo, preco, distancia);

            }

            arquivo.Close();
        }

        public void LerCidades()
        {
            StreamReader arq = new StreamReader("C:\\Users\\comae\\Desktop\\DirigivelEmMarte\\CidadesMarte.txt");
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
                cordx = (Convert.ToInt32(linha.Substring(19, 4).Trim()));
                cordy = (Convert.ToInt32(linha.Substring(23).Trim()));

                listaCidades.InserirAposFim(new Cidade(codigo, cidade, cordx, cordy));

            }
            arq.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LerCidades();            

            //limpando combobox
            cb_cidadeDestino.Items.Clear();
            cb_cidadeSaida.Items.Clear();
            cb_melhorCaminho.Items.Clear();

            //preenchendo comboBox com o nome das cidades
            listaCidades.IniciarPercursoSequencial();
            while (!listaCidades.ChegouNoFim())
            {
                cb_cidadeDestino.Items.Add(listaCidades.Atual.Info.NomeCidade);
                cb_cidadeSaida.Items.Add(listaCidades.Atual.Info.NomeCidade);

                listaCidades.Avancar();
            }
            
            //inserindo valor e item para o filtro de Melhor Caminho
            cb_melhorCaminho.Items.Insert(0, "Tempo");
            cb_melhorCaminho.Items.Insert(1, "Dinheiro");
            cb_melhorCaminho.Items.Insert(2, "Distância");
            
            LerCaminhos();
        }

        //adicionar caso em que a cidade origem = cidade destino...
        public void buscarCaminho()
        {
            int cidadeAtual, cidadeDestino;
            //pega o valor do comboBox de melhor caminho
            caso = cb_melhorCaminho.SelectedIndex;

            listaCidades.IniciarPercursoSequencial();
            while (!listaCidades.ChegouNoFim())
            {
                if (listaCidades.Atual.Info.NomeCidade.Equals(cb_cidadeSaida.SelectedItem.ToString()))
                    cidadeInicial = listaCidades.Atual.Info.Cod;

                if (listaCidades.Atual.Info.NomeCidade.Equals(cb_cidadeDestino.SelectedItem.ToString()))
                    cidadeFinal = listaCidades.Atual.Info.Cod;

                listaCidades.Avancar();
            }

            //cidadeInicial = 0;
            //cidadeFinal = 8;
            
            cidadeAtual = cidadeInicial;
            cidadeDestino = 0;
            bool achou = false;

            switch (caso)
            {
                case -1: //Busca caminho sem nenhum filtro
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
                             caminhoEncontrado.Empilhar(caminhoPilha.Desempilhar());
                            if (!caminhoPilha.EstaVazia())
                            {
                                cidadeAtual = caminhoPilha.oTopo().CidadeAtual;
                                cidadeDestino = caminhoPilha.oTopo().CidadeDestino;
                            }
                            

                            DesenhaCaminho(corDesenho, pbAreaDesenho.CreateGraphics());                         

                            
                            

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

            //pbAreaDesenho.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e)
        {
            stMensagem.Items[0].Text = e.X + "," + e.Y;
        }

      

        private void btnTracarCam_Click(object sender, EventArgs e)
        {
            //usuário traça caminhos específicos 
            //if ((!(cb_cidadeSaida.Text == "") && !(cb_cidadeDestino.Text == "")) 
            //        || (!(cb_cidadeSaida.Text == "") && cb_cidadeDestino.Text == "") 
            //        || (cb_cidadeSaida.Text == "" && !(cb_cidadeDestino.Text == "")))
            //{
            //    if (!(cb_cidadeSaida.Text == "") && !(cb_cidadeDestino.Text == ""))
            //    {
            //        //usuário escolhe um melhor caminho a ser traçado
            //        if (!(cb_melhorCaminho.Text == ""))
            //        {
            //            //não há um melhor caminho a ser traçado
            //        }
            //        else
            //        {
            //            //caso tenha um melhor caminho a tracar
            //        }
            //    }
            //    else
            //        MessageBox.Show("Por favor preencha a cidade saída ou destino");

            //}
            //else
            //{

            //}

            buscarCaminho();

            //escolher saída e destino
            //recolher dados dos caminhos..pilha
            // pegar cod de cidades intermediárias do caminho encontrado..
            // listaCaminhosP.inserirAoFim(new caminhoPintado());

            //definir caminho pelo preço/distancia/tempo

            //para traçar todos os caminhos --> pegar do leitura de caminhos..

            //4096 x 2048 coordenadas originais

        }
        

        public void DesenhaCaminho(Color cor, Graphics g)
        {
            int cidadeAtual, cidadeDestino, xAtual, yAtual, xDestino, yDestino;
            xAtual = 0;
            yAtual = 0;
            xDestino = 0;
            yDestino = 0;
            double encontraNovaCordX, encontraNovaCordY;
            encontraNovaCordX = Math.Round((Convert.ToSingle(pbAreaDesenho.Size.Width) / 4096)*100);
            encontraNovaCordY = Math.Round((Convert.ToSingle(pbAreaDesenho.Size.Width) / 2048)*100);
            Console.WriteLine(encontraNovaCordY + "    " + encontraNovaCordX);

            while (!caminhoEncontrado.EstaVazia())
            {
                listaCidades.IniciarPercursoSequencial();
                cidadeAtual = caminhoEncontrado.oTopo().CidadeAtual;
                cidadeDestino = caminhoEncontrado.oTopo().CidadeDestino;

                while (!listaCidades.ChegouNoFim())
                {
                    if (listaCidades.Atual.Info.Cod == cidadeAtual)
                    {
                        xAtual = (listaCidades.Atual.Info.CoordX * Convert.ToInt32(encontraNovaCordX)) / 100;
                        yAtual = (listaCidades.Atual.Info.CoordY * Convert.ToInt32(encontraNovaCordY)) / 100;

                    }
                    else
                    {
                        if (listaCidades.Atual.Info.Cod == cidadeDestino)
                        {
                            xDestino = (listaCidades.Atual.Info.CoordX * Convert.ToInt32(encontraNovaCordX)) / 100;
                            yDestino = (listaCidades.Atual.Info.CoordY * Convert.ToInt32(encontraNovaCordY)) / 100;
                        }
                    }

                    listaCidades.Avancar();
                }

                Pen pen = new Pen(cor); //Alguma cor
                g.DrawLine(pen, xAtual, yAtual, // ponto inicial
                    xDestino, yDestino); // ponto final
                                                    //Aqui você desenha a linha do xAtual e yAtual até o xDestino e yDestino
                
                caminhoEncontrado.Desempilhar();
            }
            //pbAreaDesenho.Invalidate();
        }
    }

}
