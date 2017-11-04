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

        private int[][] pocisoes;
        private Caminho caminho;
        //private ListaSimples<Caminho> listaSimplesCaminho;
        public Form1()
        {
            InitializeComponent();
            //listaSimplesCaminho = new ListaSimples<Caminho>();
        }

        private void btn_ler_caminhos(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader arquivo = new StreamReader(fileDialog.FileName);
                String linha = arquivo.ReadLine();

                while ((linha = arquivo.ReadLine()) != null)
                {

                    caminho = new Caminho(Convert.ToInt32(linha.Substring(0, 2).Trim()),
                    Convert.ToInt32(linha.Substring(2, 3).Trim()),
                    Convert.ToInt32(linha.Substring(5, 6).Trim()),
                    Convert.ToDouble(linha.Substring(11, 4).Trim()),
                    Convert.ToDouble(linha.Substring(15).Trim()));

                    //pilhaListaCaminho.inserirAposFim(caminho);

                }

            }
        }
    }
}
