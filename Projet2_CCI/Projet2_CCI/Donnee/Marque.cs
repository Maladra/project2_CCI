using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Donnee
{
    class Marque
    {
        public int idMarque { get; set; }
        public string nomMarque { get; set; }

        public Marque(int idMarque, string nomMarque)
        {
            this.idMarque = idMarque;
            this.nomMarque = nomMarque;
        }
    }
}
