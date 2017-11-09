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
            if (base.EstaVazia)
                throw new PilhaVaziaException("Underflow: pilha esvaziou.");

            Dado retornado = Primeiro.Info;
            Remover(null, Primeiro);
            return retornado;
        }

        public void Empilhar(Dado dado)
        {
            InserirAposFim(dado);
        }

        public new bool EstaVazia()
        {
            return base.EstaVazia;
        }

        public Dado oTopo()
        {
            if (base.EstaVazia)
                throw new PilhaVaziaException("Underflow: pilha esvaziou.");

            return Primeiro.Info;
        }

        public int Tamanho()
        {
            return base.QuantosNos + 1;
        }
    }
}
