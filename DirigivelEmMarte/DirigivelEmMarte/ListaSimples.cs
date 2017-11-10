using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DirigivelEmMarte
{
    public class ListaSimples<Dado> where Dado : IComparable<Dado>
    {
        NoLista<Dado> primeiro, ultimo, atual, anterior;
        int quantosNos;

        public ListaSimples()
        {
            Primeiro = null;
            Ultimo = null;
            quantosNos = 0;
        }

        public int QuantosNos
        {
            get { return quantosNos; }

            set
            {
                if (value >= 0)
                    quantosNos = value;
                else
                    throw
                    new Exception("Quantidade de nós deve ser positiva!");
            }
        }

        public void IniciarPercursoSequencial()
        {
            anterior = null;
            atual = Primeiro;
        }

        public bool ChegouNoFim()
        {
            return atual == null;
        }

        public Dado InfoAtual()
        {
            if (atual != null)
                return atual.Info;

            throw new Exception("Tentativa de acesso a informação inacessível!");
        }

        public void Avancar()
        {
            if (atual != null)
                atual = atual.Prox;
        }

        public void InserirAposFim(Dado informacao)
        {
            NoLista<Dado> novoNo = new NoLista<Dado>(informacao, null);
            if (EstaVazia)
                Primeiro = novoNo;
            else
                Ultimo.Prox = novoNo;
            Ultimo = novoNo;
            quantosNos++;
        }

        public void InserirAntesDoInicio(Dado informacao)
        {
            var novoNo = new NoLista<Dado>(informacao, null);
            if (EstaVazia)
                Ultimo = novoNo;
            else
                novoNo.Prox = Primeiro;
            Primeiro = novoNo;
            quantosNos++;
        }

        public void InserirAntesDoInicio(NoLista<Dado> novoNo)
        {
            if (EstaVazia)       // se a lista está vazia, estamos
                ultimo = novoNo; // incluindo o 1o e o último nós!

            novoNo.Prox = primeiro; // faz o novo nó apontar para o nó
            primeiro = novoNo;      // atualmente no início da lista
            quantosNos++;           // (que pode ser null)
        }

        public bool ExisteDado(Dado outroProcurado)
        {
            anterior = null;
            atual = Primeiro;

            // Em seguida, é verificado se a lista está vazia. Caso esteja, é
            // retornado false ao local de chamada, indicando que a chave não foi
            // encontrada, e atual e anterior ficam valendo null

            if (EstaVazia)
                return false;

            // a lista não está vazia, possui nós
            // dado procurado é menor que o primeiro dado da lista:
            // portanto, dado procurado não existe

            if (outroProcurado.CompareTo(Primeiro.Info) < 0)
                return false;

            // dado procurado é maior que o último dado da lista:
            // portanto, dado procurado não existe

            if (outroProcurado.CompareTo(Ultimo.Info) > 0)
            {
                anterior = Ultimo;
                atual = null;
                return false;
            }

            // caso não tenha sido definido que a chave está fora dos limites de
            // chaves da lista, vamos procurar no seu interior
            // o apontador atual indica o primeiro nó da lista e consideraremos que
            // ainda não achou a chave procurada nem chegamos ao final da lista
            
            bool achou = false;
            bool fim = false;
            
            // repete os comandos abaixo enquanto não achou o RA nem chegou ao
            // final da lista
            while (!achou && !fim)
            
                // se o apontador atual vale null, indica final da lista
                if (atual == null)
                    fim = true;
            
                        // se não chegou ao final da lista, verifica o valor da chave atual
                else
                
                // verifica igualdade entre chave procurada e chave do nó atual
                if (outroProcurado.CompareTo(atual.Info) == 0)
                    achou = true;
                else
                
                // se chave atual é maior que a procurada, significa que
                // a chave procurada não existe na lista ordenada e, assim,
                // termina a pesquisa indicando que não achou. Anterior
                // aponta o anterior ao atual, que foi acessado por
                // último
                
                if (atual.Info.CompareTo(outroProcurado) > 0)
                    fim = true;
                else
                {
                
                    // se não achou a chave procurada nem uma chave > que ela,
                    // então a pesquisa continua, de maneira que o apontador
                    // anterior deve apontar o nó atual e o apontador atual
                    // deve seguir para o nó seguinte

                    anterior = atual;
                    atual = atual.Prox;
                }

            // por fim, caso a pesquisa tenha terminado, o apontador atual
            // aponta o nó onde está a chave procurada, caso ela tenha sido
            // encontrada, ou o nó onde ela deveria estar para manter a
            // ordenação da lista. O apontador anterior aponta o nó anterior
            // ao atual

            return achou; // devolve o valor da variável achou, que indica
        }                 // se a chave procurada foi ou não encontrado

        private void InserirNoMeio(Dado dados)
        {
            NoLista<Dado> novoNo = new NoLista<Dado>(dados, null);

            // ExisteDado() encontrou intervalo de inclusão do novo nó

            anterior.Prox = novoNo; // liga anterior ao novo
            novoNo.Prox = atual;    // e novo no atual
            
            if (anterior == Ultimo) // se incluiu ao final da lista,
                Ultimo = novoNo;    // atualiza o apontador ultimo
            
            quantosNos++; // incrementa número de nós da lista
        }
        public void InserirEmOrdem(Dado dados)
        {
            if (!ExisteDado(dados)) // ExisteDado configura anterior e atual
            {                       // aqui temos certeza de que a chave não existe
                                    // novo nó
                if (EstaVazia)                   // se a lista está vazia, então o
                    InserirAntesDoInicio(dados); // novo nó é o primeiro da lista
                else
                if (anterior == null && atual != null)
                    InserirAntesDoInicio(dados);    // liga novo antes do primeiro
                else
                    InserirNoMeio(dados); // insere entre os nós anterior e atual
            }
        }
        public bool EstaVazia
        {
            get { return Primeiro == null; }
        }

        public NoLista<Dado> Atual
        {
            get
            {
                return atual;
            }
        }

        public NoLista<Dado> Ultimo { get { return ultimo; } set { ultimo = value; } }
        public NoLista<Dado> Primeiro { get { return primeiro; } set { primeiro = value; } }
        

        public void percorrer()
        {
            atual = Primeiro;
            while (atual != null)
            {
                MessageBox.Show(atual.Info.ToString());
                atual = atual.Prox;
            }
        }

        public void Remover(ref NoLista<Dado> anterior, ref NoLista<Dado> atual)
        {
            if (!EstaVazia)
            {
                if (atual == primeiro)
                {
                    primeiro = primeiro.Prox;
                    if (EstaVazia)
                        ultimo = null;
                }
                else
                  if (atual == ultimo)
                {
                    ultimo = anterior;
                    ultimo.Prox = null;
                }
                else
                    anterior.Prox = atual.Prox;

                quantosNos--;
            }
        }
        public bool Remover(Dado info)
        {
            if (ExisteDado(info))  // ExisteDado posiciona anterior e atual
            {
                Remover(ref anterior, ref atual);
                return true;   // conseguimos remover o nó
            }
            else
                return false;
        }
        public void SalvarNoArquivo(string nomeArquivo)
        {
            var arq = new StreamWriter(nomeArquivo);
            atual = Primeiro;
            while (atual != null)
            {
                arq.WriteLine(atual.Info.ToString());
                atual = atual.Prox;
            }
            arq.Close();
        }

        public void listar(ListBox umListBox, string cabecalho)
        {
            umListBox.Items.Clear();
            umListBox.Items.Add(cabecalho);

            atual = Primeiro;
            while (atual != null)
            {
                umListBox.Items.Add(atual.Info.ToString());
                atual = atual.Prox;
            }
        }

        public int Contar()
        {
            int contagemAtual = 0;
            atual = Primeiro;
            while (atual != null)
            {
                contagemAtual++;
                atual = atual.Prox;
            }
            return contagemAtual;
        }

        public ListaSimples<Dado> Juntar(ListaSimples<Dado> outra)
        {
            var result = new ListaSimples<Dado>();

            this.atual = this.Primeiro;         // posicionamos ponteiro atual no início da lista this
            outra.atual = outra.Primeiro;       // posicionamos ponteiro atual no início da lista outra

            while (this.atual != null && outra.atual != null)  // enquanto uma das listas ainda não acabou
            {
                if (this.atual.Info.CompareTo(outra.atual.Info) < 0)
                {
                    result.InserirAposFim(this.atual.Info);  // guardamos o menor elemento
                    this.atual = this.atual.Prox;            // avançamos na lista do menor elemento
                }
                else
                    if (outra.atual.Info.CompareTo(this.atual.Info) < 0)
                {
                    result.InserirAposFim(outra.atual.Info);    // guardamos o menor elemento
                    outra.atual = outra.atual.Prox;             // avançamos na lista do menor elemento
                }
                else    // elementos de chaves iguais
                {
                    result.InserirAposFim(this.atual.Info);  // guardamos um dos elementos iguais
                    this.atual = this.atual.Prox;            // como empatou, avançamos nas duas
                    outra.atual = outra.atual.Prox;          // listas
                }
            }

            // quando uma das listas terminar de ser percorrida, temos que
            // "esgotar" o percurso da lista que sobrou, percorrendo-a até
            // seu final:

            while (this.atual != null)   // a lista this ainda não acabou
            {
                result.InserirAposFim(this.atual.Info);  // guardamos o elemento que sobrou
                this.atual = this.atual.Prox;            // avançamos na lista this
            }

            while (outra.atual != null)   // a lista this ainda não acabou
            {
                result.InserirAposFim(outra.atual.Info);  // guardamos o elemento que sobrou
                outra.atual = outra.atual.Prox;           // avançamos na outra lista 
            }

            return result;
        }

        public void Inverter()
        {
            NoLista<Dado> um, dois, tres;
            um = dois = tres = null;

            if (!EstaVazia)
            {
                um = Primeiro;
                dois = Primeiro.Prox;
                while (dois != null)
                {
                    tres = dois.Prox;
                    dois.Prox = um;
                    um = dois;
                    dois = tres;
                }
                Ultimo = Primeiro;
                Primeiro = um;
                Ultimo.Prox = null;
            }
        }
    }
}
