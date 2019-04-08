using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Donnee
{
    class Marque
    {
        public string idMarque { get; set; }
        public string nomMarque { get; set; }

        public Marque(string idMarque, string nomMarque)
        {
            this.idMarque = idMarque;
            this.nomMarque = nomMarque;
        }

        public override string ToString()
        {
            return nomMarque;
        }
    }
}
