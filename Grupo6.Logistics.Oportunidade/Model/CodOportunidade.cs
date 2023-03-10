using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Grupo6.Logistics.Oportunidade.Model
{
    public class CodOportunidade
    {
        public string Termo1 = "OPP-";
        public int Termo2 = 12365;
        public string Termo3 = "-A1A2";
        public object CodCompleto { get; set;}


        public string GetCod()
        {
            CodCompleto = Termo1 + Termo2.ToString() + Termo3;
            return CodCompleto.ToString();
        }

        public string AcrescerCod(int termo2)
        {
            termo2 += 1;
            CodCompleto = Termo1 + termo2 + Termo3;

            return CodCompleto.ToString();
        }
    }
}
