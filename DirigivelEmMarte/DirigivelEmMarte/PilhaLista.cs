using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class PilhaLista<Dado> : ListaSimples<Dado>, IStack<Dado>
                             where Dado : IComparable<Dado>
    {
        public PilhaLista() : base()
        {

        }

        public Dado Desempilhar()
        {
            if (EstaVazia())
            throw new PilhaVaziaException("pilha vazia!");

            Dado valor = base.Primeiro.Info;

            NoLista<Dado> pri = base.Primeiro;
            NoLista<Dado> ant = null;
            base.Remover(ref ant, ref pri);
            return valor;
        }

            public void Empilhar(Dado elemento)
            {
                base.InserirAntesDoInicio
                      (
                        new NoLista<Dado>(elemento, null)
                      );
            }

            new public bool EstaVazia()
            {
                return base.EstaVazia;
            }

            public Dado oTopo()
            {
                if (EstaVazia())
                    throw new PilhaVaziaException("pilha vazia!");

                return base.Primeiro.Info;
            }

            public int Tamanho()
            {
                return base.QuantosNos;
            }

                        
        }
}
