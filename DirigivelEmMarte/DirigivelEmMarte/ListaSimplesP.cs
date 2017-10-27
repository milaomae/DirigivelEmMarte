using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class ListaSimplesP<Dado> 
    {
        NoListaP<Dado> primeiro, ultimo, atual, anterior;
        int quantosNos;

        public int QuantosNos
        {
            get
            {
                return quantosNos;
            }

            set
            {
                if (value >= 0)
                    quantosNos = value;
                else
                    throw new Exception("Quantidade de nós não pode ser negativa");
            }
        }

        internal NoListaP<Dado> Anterior
        {
            get
            {
                return anterior;
            }

            set
            {
                anterior = value;
            }
        }

        internal NoListaP<Dado> Atual
        {
            get
            {
                return atual;
            }

            set
            {
                atual = value;
            }
        }

        internal NoListaP<Dado> Primeiro
        {
            get
            {
                return primeiro;
            }

            set
            {
                primeiro = value;
            }
        }

        internal NoListaP<Dado> Ultimo
        {
            get
            {
                return ultimo;
            }

            set
            {
                ultimo = value;
            }
        }

        public void InserirAntesDoInicio(NoListaP<Dado> novoNo)
        {
            novoNo.Prox = primeiro;
            primeiro = novoNo;
            if (EstaVazia)
            {
                ultimo = primeiro;
            }
            
        }

        public bool EstaVazia
        {
            get { return primeiro == null; }
        }


    }
}
