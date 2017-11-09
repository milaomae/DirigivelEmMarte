﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class Caminho : IComparable<Caminho>
    {
        private int cidadeAtual, cidadeDestino, tempo;
        private double preco, distancia;

        public Caminho(int cidadeAtual, int cidadeDestino, int tempo, double preco, double distancia)
        {
            this.cidadeAtual = cidadeAtual;
            this.cidadeDestino = cidadeDestino;
            this.tempo = tempo;
            this.preco = preco;
            this.distancia = distancia;
        }

        public int CidadeAtual { get => cidadeAtual; set => cidadeAtual = value; }
        public int CidadeDestino { get => cidadeDestino; set => cidadeDestino = value; }
        public int Tempo { get => tempo; set => tempo = value; }
        public double Preco { get => preco; set => preco = value; }
        public double Distancia { get => distancia; set => distancia = value; }

        public int CompareTo(Caminho other)
        {
            throw new NotImplementedException();
        }
    }
}
