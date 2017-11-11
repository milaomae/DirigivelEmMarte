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

        public void LerCaminhos()
        {
            StreamReader arquivo = new StreamReader("CaminhosEntreCidadesMarte.txt");
            String lin = "";
            int cidadeAtual, cidadeDestino, tempo;
            double preco, distancia;

            while (!arquivo.EndOfStream)
            {
                lin = arquivo.ReadLine();
                cidadeAtual = Convert.ToInt32(lin.Substring(0, 2).Trim());
                cidadeDestino = Convert.ToInt32(lin.Substring(2, 3).Trim());
                tempo = Convert.ToInt32(lin.Substring(5, 6).Trim());
                preco = Convert.ToDouble(lin.Substring(11, 4).Trim());
                distancia = Convert.ToDouble(lin.Substring(15).Trim());

                caminhoMatriz[cidadeAtual, cidadeDestino] = new Caminho(cidadeAtual, cidadeDestino, tempo, preco, distancia);
                //adicionar caminho inverso
                caminhoMatriz[cidadeDestino, cidadeAtual] = new Caminho(cidadeDestino, cidadeAtual, tempo, preco, distancia);

            }

            arquivo.Close();
        }

        public void LerCidades()
        {
            StreamReader arq = new StreamReader("CidadesMarte.txt");
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
            pbAreaDesenho.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            pbAreaDesenho.Invalidate();

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
        //verifica se há uma saída direta para a cidade Final
        //retorna um int, ou com o valor do codigo da cidade final
        //ou com o valor da cidade próxima cidade a ser verificada na sequência
        public int verificaSaidaDireta(int destino, int final)
        {
            int retornado = destino;
            while(destino < listaCidades.QuantosNos && caminhoMatriz[destino, final] != null)
            {
                if (destino == final)
                    retornado = destino;

                destino++;
            }
            return retornado;
        }
        
        public bool buscarCaminho()
        {
            
            int cidadeAtual, cidadeDestino;

            //verifica se já passou por um caminho
            bool[] jaPassou = new bool[listaCidades.QuantosNos];
            for (int i = 0; i < listaCidades.QuantosNos; i++)  //inicializa o vetor com todas as posições = false
                jaPassou[i] = false;                           //pois nenhum caminho foi percorrido ainda

            //pega o valor do comboBox de melhor caminho
            caso = cb_melhorCaminho.SelectedIndex;

            //procura o código das cidades selecionadas nos comboBoxs para inicializar as variaveis
            listaCidades.IniciarPercursoSequencial();
            while (!listaCidades.ChegouNoFim())
            {
                if (listaCidades.Atual.Info.NomeCidade.Equals(cb_cidadeSaida.SelectedItem.ToString()))
                    cidadeInicial = listaCidades.Atual.Info.Cod;

                if (listaCidades.Atual.Info.NomeCidade.Equals(cb_cidadeDestino.SelectedItem.ToString()))
                    cidadeFinal = listaCidades.Atual.Info.Cod;

                listaCidades.Avancar();
            }
                        
            cidadeAtual = cidadeInicial; //cidade que começará o percurso 
            cidadeDestino = 0;
            int aux = 0;
            bool achou = false;

            switch (caso)
            {
                case -1: //Busca caminho sem nenhum filtro
                    do
                    {
                        for (; cidadeDestino < listaCidades.QuantosNos; cidadeDestino++)
                        {
                            if (caminhoMatriz[cidadeAtual, cidadeDestino] != null && !jaPassou[cidadeDestino]) //verifica se há um caminho possível na matriz e,
                            {                                                                                  // se a próxima cidade já foi verificada
                                //faz a verificação se existe uma saída direta na cidadeAtual
                                aux = verificaSaidaDireta(cidadeDestino, cidadeFinal);
                                if(aux == cidadeFinal && !jaPassou[aux])
                                {
                                    caminhoPilha.Empilhar(caminhoMatriz[cidadeAtual, aux]);
                                    jaPassou[cidadeAtual] = true;
                                    cidadeAtual = aux;
                                    cidadeDestino = 0;
                                    break;
                                }

                                caminhoPilha.Empilhar(caminhoMatriz[cidadeAtual, cidadeDestino]);
                                //resetar a cidade fim com false, para procurar outros caminhos possíveis caso um caminho já tenha sido encontrado
                                jaPassou[cidadeFinal] = false;

                                //PARA TESTE : ver os caminhos que estão sendo empilhados
                               // Console.WriteLine("CAMINHO EMPILHADO(caminhoPilha): " +caminhoPilha.oTopo().CidadeAtual + "  " + caminhoPilha.oTopo().CidadeDestino);
                                
                                //a cidade que ele irá será marcada, para saber que esse caminho já foi feito
                                jaPassou[cidadeAtual] = true;
                                //próxima cidade que irá procurar destinos é a cidade destino do caminho que acaba de encontrar
                                cidadeAtual = cidadeDestino;
                                cidadeDestino = 0;          //reinicializa cidadeDestino para procurar próximos caminhos 
                                break;                      
                            }
                        }
                        //verifica se a cidade que acaba de chegar é o destino final
                        if (cidadeFinal == caminhoPilha.oTopo().CidadeDestino)
                        {
                            //encontrou um caminho
                            achou = true;
                            //empilha o caminho encontrado para ser desenhado
                            while (!caminhoPilha.EstaVazia())
                            {
                             //   Console.WriteLine("CAMINHO ENCONTRADO(caminhoEncontrado): " + caminhoPilha.oTopo().CidadeAtual + "  " + caminhoPilha.oTopo().CidadeDestino);
                                caminhoEncontrado.Empilhar(caminhoPilha.Desempilhar());
                            }

                            //retorna o caminho para buscar outros possíveis
                            caminhoPilha = DesenhaCaminho(Color.Red, pbAreaDesenho.CreateGraphics());
                            
                            //desempilha um caminho, e tenta achar os outros...
                            if (!caminhoPilha.EstaVazia())
                            {
                                //adiciona a lista de caminhos já passados para quando não ir mais a esse destino já verificado                                
                                jaPassou[caminhoPilha.oTopo().CidadeDestino] = true;
                                //reinicializa a cidade anterior com false, para percorrer o próximo caminho existente nela
                                jaPassou[caminhoPilha.oTopo().CidadeAtual] = false;
                                //Console.WriteLine("CAMINHO A SER RETIRADO QUANDO UM CAMINHO JA FOI ENCONTRADO(caminhoPilha): " + caminhoPilha.oTopo().CidadeAtual + "  " + caminhoPilha.oTopo().CidadeDestino);

                                if (!caminhoPilha.EstaVazia())
                                {
                                    cidadeAtual = caminhoPilha.oTopo().CidadeAtual;
                                    cidadeDestino = 0;
                                    caminhoPilha.Desempilhar();
                                }
                                else               //a pilha de caminhos está vazia, portanto, volta ao ponto de partida
                                {
                                    //a primeira cidade deve receber false, pois ainda nao foi empilhada
                                    jaPassou[cidadeInicial] = false;
                                    cidadeAtual = cidadeInicial;
                                    cidadeDestino = 0;

                                    //achar o próximo caminho a partir do ponto de partida
                                    for (; cidadeDestino < 23; cidadeDestino++)
                                    {
                                        if (caminhoMatriz[cidadeAtual, cidadeDestino] != null && !jaPassou[cidadeDestino])
                                        {   
                                            
                                            caminhoPilha.Empilhar(caminhoMatriz[cidadeAtual, cidadeDestino]);
                                            //Console.WriteLine("ENCONTRANDO O PROXIMO CAMINHO A SER EMPILHADO DEPOIS DE ACHAR UM CAMINHO E DESEMPILHAR(caminhoPilha): " + caminhoPilha.oTopo().CidadeAtual + "  " + caminhoPilha.oTopo().CidadeDestino);
                                
                                            //a cidade que ele irá será marcada, para saber que esse caminho já foi feito
                                            jaPassou[cidadeAtual] = true;
                                            cidadeAtual = cidadeDestino;
                                            cidadeDestino = 0;
                                            break;
                                        }
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            //todos os caminhos foram verificados
                            if (cidadeDestino >= listaCidades.QuantosNos && !caminhoPilha.EstaVazia())
                            {
                                //adiciona a lista de caminhos já passados para quando não ir mais a esse destino já verificado
                                jaPassou[caminhoPilha.oTopo().CidadeDestino] = true;
                                //reinicializa a cidade anterior com false, para percorrer o próximo caminho existente nela
                                jaPassou[caminhoPilha.oTopo().CidadeAtual] = false;
                                //Console.WriteLine("CAMINHO A SER DESEMPILHADO APOS TODOS OS CAMINHOS TEREM SIDO VERIFICADOS(caminhoPilha): " + caminhoPilha.oTopo().CidadeAtual + "  " + caminhoPilha.oTopo().CidadeDestino);

                                cidadeAtual = caminhoPilha.oTopo().CidadeAtual;
                                caminhoPilha.Desempilhar();                         //desempilha um caminho após todos terem sido verificados                     
                                cidadeDestino = 0;                                  //recomeça novamente a busca a partir da cidade anterior
                                
                                if(caminhoPilha.EstaVazia()) // se estiver vazia, voltou ao ponto de partida
                                {
                                    //a primeira cidade deve receber false, pois ainda nao foi empilhada
                                    jaPassou[cidadeInicial] = false;
                                    cidadeAtual = cidadeInicial;
                                    cidadeDestino = 0;

                                    //achar o próximo caminho do ponto de partida
                                    for (; cidadeDestino < 23; cidadeDestino++)
                                    {
                                        if (caminhoMatriz[cidadeAtual, cidadeDestino] != null && !jaPassou[cidadeDestino])
                                        {
                                            caminhoPilha.Empilhar(caminhoMatriz[cidadeAtual, cidadeDestino]);
                                            //Console.WriteLine("CAMINHO EMPILHADO APOS VOLTAR UM CAMINHO--> DPS QUE PROCUROU TODOS OS POSSIVEIS NO ANTERIOR(caminhoPilha): " + caminhoPilha.oTopo().CidadeAtual + "  " + caminhoPilha.oTopo().CidadeDestino);
                                            achou = false;              //se empilhou, quer dizer que ainda pode haver outro caminho
                                            //a cidade que ele irá será marcada, para saber que esse caminho já foi feito
                                            jaPassou[cidadeAtual] = true;
                                            cidadeAtual = cidadeDestino;
                                            cidadeDestino = 0;
                                            break;
                                        }
                                    }                                                                       
                                }

                            }
                        }

                    } while (!achou && !caminhoPilha.EstaVazia());
                    break;

                //filtro por tempo
                    //OBS : enviar cor diferente para o método desenhar!!
                case 0:                  
                    break;

                    //filtro por dinheiro
                case 1:
                    break;

                    //filtro por distancia
                case 2:
                    break;

                default:
                    break;
            }

            //verifica se, pelo menos, um caminho foi encontrado
            return achou;
            
        }

        private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e)
        {
            stMensagem.Items[0].Text = e.X + "," + e.Y;
        }
        

        private void btnTracarCam_Click(object sender, EventArgs e)
        {
            //limpa a tela
            pbAreaDesenho.Refresh();
            //esvazia as pilhas 
            while(!caminhoEncontrado.EstaVazia())
                caminhoEncontrado.Desempilhar();

            while (!caminhoPilha.EstaVazia())    
                caminhoPilha.Desempilhar();

            bool verificaSeAchou = false;
            //caso em que há filtros por cidade destino e cidade saída
            if (!(cb_cidadeSaida.Text == "") && !(cb_cidadeDestino.Text == ""))
                if (cb_cidadeSaida.Text != cb_cidadeDestino.Text)
                {
                    verificaSeAchou = buscarCaminho();
                    if (!verificaSeAchou)
                        MessageBox.Show("Não há caminho possível");
                }
                else
                    MessageBox.Show("Por favor coloque uma cidade destino diferente da cidade de saída");
            //sem cidade destino e cidade saída 
            else
            {
                if (cb_melhorCaminho.Text == "")
                {
                    //percorre a matriz de caminhos e desenha
                    for (int i = 0; i < listaCidades.QuantosNos; i++)
                    {
                        for (int j = 0; j < listaCidades.QuantosNos; j++)
                        {
                            if (caminhoMatriz[i, j] != null)
                                caminhoEncontrado.Empilhar(caminhoMatriz[i, j]);
                        }
                    }

                    DesenhaCaminho(Color.Violet, pbAreaDesenho.CreateGraphics());
                    //esvazia a pilha de caminhoEncontrado
                    while (!caminhoEncontrado.EstaVazia())
                        caminhoEncontrado.Desempilhar();
                }
                else
                    MessageBox.Show("A opção filtro necessita de uma cidade saída e uma cidade destino");
            }
            
        }
        

        private PilhaLista<Caminho> DesenhaCaminho(Color cor, Graphics g)
        {
            
            int cidadeAtual, cidadeDestino, xAtual, yAtual, xDestino, yDestino;
            xAtual = 0;
            yAtual = 0;
            xDestino = 0;
            yDestino = 0;
            //encontra a porcentagem que as coordenadas devem apresentar com o novo tamanho do mapa 
            double encontraNovaCordX, encontraNovaCordY;
            encontraNovaCordX = (Convert.ToSingle(pbAreaDesenho.Size.Width) / 4096)*100;
            encontraNovaCordY = (Convert.ToSingle(pbAreaDesenho.Size.Height) / 2048)*100;
            //Console.WriteLine(encontraNovaCordY + "    " + encontraNovaCordX); teste
            //pilha auxiliar para retornar a pilha original na ordem correta
            PilhaLista<Caminho> pilhaAuxiliar = new PilhaLista<Caminho>();
            while(!caminhoEncontrado.EstaVazia())
                pilhaAuxiliar.Empilhar(caminhoEncontrado.Desempilhar());

            while (!pilhaAuxiliar.EstaVazia())
            {
                listaCidades.IniciarPercursoSequencial();
                cidadeAtual = pilhaAuxiliar.oTopo().CidadeAtual;
                cidadeDestino = pilhaAuxiliar.oTopo().CidadeDestino;

                //encontrar as coordenadas das cidades
                while (!listaCidades.ChegouNoFim())
                {
                    if (listaCidades.Atual.Info.Cod == cidadeAtual)
                    {
                        xAtual = Convert.ToInt32(Math.Round((listaCidades.Atual.Info.CoordX * encontraNovaCordX) / 100));
                        yAtual = Convert.ToInt32(Math.Round((listaCidades.Atual.Info.CoordY * encontraNovaCordY) / 100));

                    }
                    else
                    {
                        if (listaCidades.Atual.Info.Cod == cidadeDestino)
                        {
                            xDestino = Convert.ToInt32(Math.Round((listaCidades.Atual.Info.CoordX * encontraNovaCordX) / 100));
                            yDestino = Convert.ToInt32(Math.Round((listaCidades.Atual.Info.CoordY * encontraNovaCordY) / 100));
                        }
                    }

                    listaCidades.Avancar();
                }

                Pen pen = new Pen(cor, Convert.ToSingle(5)); //Alguma cor
                g.DrawLine(pen, xAtual, yAtual, // ponto inicial
                    xDestino, yDestino); // ponto final
                                         //Aqui você desenha a linha do xAtual e yAtual até o xDestino e yDestino
                
                //retorna para a lista de caminho encontrado..
                 caminhoEncontrado.Empilhar(pilhaAuxiliar.Desempilhar());
            }

            //reempilha para ficar como a original
            while (!caminhoEncontrado.EstaVazia())
                pilhaAuxiliar.Empilhar(caminhoEncontrado.Desempilhar());          

            //retorna a pilha com a forma da original
            return pilhaAuxiliar;

            
        }

        private PilhaLista<Caminho> AcharCaminhoMaisRapido(PilhaLista<Caminho> p1, PilhaLista<Caminho> p2)
        {
            int tempoTotal1 = 0, tempoTotal2 = 0;
            Caminho caminhoAux = null;
            PilhaLista<Caminho> pilhaAux = new PilhaLista<Caminho>();
            //obtem o preco total da primeira pilha de caminho
            while (!p1.EstaVazia())
            {
                caminhoAux = p1.Desempilhar();
                tempoTotal1 += caminhoAux.Tempo;
                pilhaAux.Empilhar(caminhoAux);
            }
            //recupera pilha original e esvazia pilhaAux
            while (!pilhaAux.EstaVazia())
                p1.Empilhar(pilhaAux.Desempilhar());

            //obtem o preco total da segunda pilha de caminho
            while (!p2.EstaVazia())
            {
                caminhoAux = p2.Desempilhar();
                tempoTotal2 += caminhoAux.Tempo;
                pilhaAux.Empilhar(caminhoAux);
            }
            //recupera pilha original e esvazia pilhaAux
            while (!pilhaAux.EstaVazia())
                p2.Empilhar(pilhaAux.Desempilhar());

            if (tempoTotal1 > tempoTotal2)
                return p2;

            return p1;
        }

        private PilhaLista<Caminho> AcharCaminhoMaisBarato(PilhaLista<Caminho> p1, PilhaLista<Caminho> p2)
        {
            double precoTotal1 = 0, precoTotal2 = 0;
            Caminho caminhoAux = null;
            PilhaLista<Caminho> pilhaAux = new PilhaLista<Caminho>();
            //obtem o preco total da primeira pilha de caminho
            while (!p1.EstaVazia())
            {
                caminhoAux = p1.Desempilhar();
                precoTotal1 += caminhoAux.Preco;
                pilhaAux.Empilhar(caminhoAux);
            }
            //recupera pilha original e esvazia pilhaAux
            while (!pilhaAux.EstaVazia())
                p1.Empilhar(pilhaAux.Desempilhar());

            //obtem o preco total da segunda pilha de caminho
            while (!p2.EstaVazia())
            {
                caminhoAux = p2.Desempilhar();
                precoTotal2 += caminhoAux.Preco;
                pilhaAux.Empilhar(caminhoAux);
            }
            //recupera pilha original e esvazia pilhaAux
            while (!pilhaAux.EstaVazia())
                p2.Empilhar(pilhaAux.Desempilhar());

            if (precoTotal1 > precoTotal2)
                return p2;
            
            return p1;
        }

        

    }

}
