using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Donnee
{
    class Style
    {
        public int idStyle { get; set; }
        public string nomStyle { get; set; }

        public Style(int idStyle, string nomStyle)
        {
            this.idStyle = idStyle;
            this.nomStyle = nomStyle;
        }
    }
}
