using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    interface IStack<Dado>
    {
        void Empilhar(Dado dado);

        Dado oTopo();

        Dado Desempilhar();

        int Tamanho();

        bool EstaVazia();
        
    }
}
