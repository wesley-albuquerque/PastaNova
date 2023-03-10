using Grupo6.Logistcs.Arquitetura;
using Grupo6.Logistics.Arquitetura;
using Grupo6.Logistics.Oportunidade2.Controller;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Oportunidade2
{
    public class OpportunityManager2 : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportuniy = Context.InputParameters["Target"] as Entity;
            IOrganizationService ambient2 = PluginConnection.GetService();
            opportuniy.Attributes["gp6_integracao"] = true;
            
            
            OppController2 oppControllerPrincipal = new OppController2(Service);
            OppController2 oppControllersecundario = new OppController2(ambient2);
            
            string nomeUsuario = oppControllerPrincipal.GetNameByUserId(((EntityReference)opportuniy.Attributes["ownerid"]).Id);
            Guid userId = oppControllersecundario.GetUserIdByName(nomeUsuario);

            string codMoeda = oppControllerPrincipal.GetCodeSymbolById(((EntityReference)opportuniy.Attributes["transactioncurrencyid"]).Id);
            Guid moedaId = oppControllersecundario.GetCurrencyIdByName(codMoeda);

            //if (opportuniy.Contains("parentcontactid")) 
            //{
            //    string cpfContato = oppControllerPrincipal.GetCPFContacById(((EntityReference)opportuniy.Attributes["parentcontactid"]).Id);
            //    Guid contactId = oppControllersecundario.GetContactIdByCpf(cpfContato);
            //    opportuniy.Attributes["parentcontactid"] = new EntityReference("contact", contactId);
            //}

            if (opportuniy.Contains("parentaccountid"))
            {
                string cnpj = oppControllerPrincipal.GetCnpjAccountById(((EntityReference)opportuniy.Attributes["parentaccountid"]).Id);
                Guid accounId = oppControllersecundario.GetAccountIdByCnpj(cnpj);
                opportuniy.Attributes["parentaccountid"] = new EntityReference("account", accounId);
            }




            //opportuniy.Attributes["parentcontactid"] = new EntityReference("contact", new Guid("a5a9d051-35bf-ed11-83ff-002248dfe152"));

            opportuniy.Attributes["ownerid"] = new EntityReference("systemuser", userId);
            opportuniy.Attributes["transactioncurrencyid"] = new EntityReference("transactioncurrency", moedaId);
            
            ambient2.Create(opportuniy);

        }
    }
}
