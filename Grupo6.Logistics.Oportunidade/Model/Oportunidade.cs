using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Oportunidade.Model
{
    public class Opportunity
    {
        public Entity Oportunidade = new Entity("opportunity");
        public string LogicalName = "opportunity";
        public IOrganizationService Service { get; set; }
        public Opportunity(IOrganizationService service) 
        {
            Service = service;
        }

        public bool ValidarCodOpp(string codOpp)
        {
            QueryExpression busca = new QueryExpression(LogicalName);
            busca.Criteria.AddCondition("gp6_codopp", ConditionOperator.Equal, codOpp);
            EntityCollection oportunidades = Service.RetrieveMultiple(busca);
            Entity responseOpp = oportunidades.Entities.FirstOrDefault();

            if (responseOpp == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string BuscaMaiorCodOpp()
        {
            QueryExpression busca = new QueryExpression(LogicalName);
            busca.ColumnSet.AddColumn("gp6_codopp");
            //busca.TopCount = 1;
            busca.AddOrder("gp6_codopp", OrderType.Descending);
            EntityCollection oportunidades = Service.RetrieveMultiple(busca);
            Oportunidade = oportunidades.Entities.FirstOrDefault();
            

            return Oportunidade.Contains("gp6_codopp") ? Oportunidade.Attributes["gp6_codopp"].ToString() : null;

        }

       
    

    }
}
