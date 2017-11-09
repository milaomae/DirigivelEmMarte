using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirigivelEmMarte
{
    class PilhaVaziaException : Exception
    {
        public PilhaVaziaException(string msg) : base(msg)
        {
            
        }
    }
}
