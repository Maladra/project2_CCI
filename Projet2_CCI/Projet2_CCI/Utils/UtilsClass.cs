using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Utils
{
    static class UtilsClass
    {
        /// <summary>
        /// prend un array de string si au moins un string est vide ou a un espace return false sinon retourne true
        /// </summary>
        public static bool VerifString(params string[] strings)
        => !strings.Any(string.IsNullOrWhiteSpace);



    }
}
