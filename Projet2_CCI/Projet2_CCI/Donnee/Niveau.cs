using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Donnee
{
    class Niveau
    {
        public int idNiveau { get; set; }
        public string nomNiveau { get; set; }

        public Niveau (int idNiveau, string nomNiveau)
        {
            this.idNiveau = idNiveau;
            this.nomNiveau = nomNiveau;
        }
    }
}
