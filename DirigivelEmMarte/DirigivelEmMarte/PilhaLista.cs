using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class PilhaLista<Dado> : ListaSimples<Dado>, IStack<Dado>
    {
        public Dado Desempilhar()
        {
            throw new NotImplementedException();
        }

        public void Empilhar(Dado dado)
        {
            throw new NotImplementedException();
        }

        public bool EstaVazia()
        {
            return base.EstaVazia;
        }

        public Dado oTopo()
        {
            return base.Ultimo.Info;
        }

        public int Tamanho()
        {
            return base.QuantosNos + 1;
        }
    }
}
