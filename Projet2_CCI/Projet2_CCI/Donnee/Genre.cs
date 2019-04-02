using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Donnee
{
    class Genre
    {
        public int idGenre { get; set; }
        public string nomGenre { get; set; }

        public Genre (int idGenre, string nomGenre)
        {
            this.idGenre = idGenre;
            this.nomGenre = nomGenre;
        }
    }
}
