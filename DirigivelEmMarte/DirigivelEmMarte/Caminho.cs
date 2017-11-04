using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class Caminho
    {
        private int cidadeAtual, cidadeDistino, tempo;
        private double preco, distancia;

        public Caminho(int cidadeAtual, int cidadeDistino, int tempo, double preco, double distancia)
        {
            this.cidadeAtual = cidadeAtual;
            this.cidadeDistino = cidadeDistino;
            this.tempo = tempo;
            this.preco = preco;
            this.distancia = distancia;
        }

        public int CidadeAtual { get => cidadeAtual; set => cidadeAtual = value; }
        public int CidadeDistino { get => cidadeDistino; set => cidadeDistino = value; }
        public int Tempo { get => tempo; set => tempo = value; }
        public double Preco { get => preco; set => preco = value; }
        public double Distancia { get => distancia; set => distancia = value; }
    }
}
