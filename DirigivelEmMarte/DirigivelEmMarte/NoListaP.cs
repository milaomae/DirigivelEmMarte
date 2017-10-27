using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class NoListaP<Dado>
    {
        Dado info;
        NoListaP<Dado> prox;

        public NoListaP(Dado info, NoListaP<Dado> prox)
        {
            Info = info;
            Prox = prox;
        }
        public Dado Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }

        internal NoListaP<Dado> Prox
        {
            get
            {
                return prox;
            }

            set
            {
                prox = value;
            }
        }
    }
}
