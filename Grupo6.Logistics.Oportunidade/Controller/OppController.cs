using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Grupo6.Logistics.Oportunidade.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Grupo6.Logistics.Oportunidade.Controller
{
    public class OppController
    {
        public Opportunity Oportunidade { get; set; }
        public CodOportunidade CodOportunidade { get; set; }
        public string CodOpp { get; set; }

        public OppController(IOrganizationService serviceClient)
        {
            Oportunidade = new Opportunity(serviceClient);
            CodOportunidade = new CodOportunidade();
        }

        public void GetCod()
        {
            string codOpp = Oportunidade.BuscaMaiorCodOpp();
            if( codOpp == null)
            {
                CodOpp = CodOportunidade.GetCod();
            }
            else
            {
                string[] arrayCodOpp = codOpp.Split('-');
                CodOpp = CodOportunidade.AcrescerCod(int.Parse(arrayCodOpp[1]));
            }

        }

        public void InsereCod(IPluginExecutionContext context)
        {
            Entity oportunidade = context.InputParameters["Target"] as Entity;
            oportunidade.Attributes["gp6_codopp"] = CodOpp;
        }


    }
}
