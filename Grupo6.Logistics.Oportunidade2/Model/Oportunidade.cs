using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Oportunidade2.Model
{
    public class Oportunidade
    {
        public IOrganizationService Service { get; set; }
        public Oportunidade(IOrganizationService service)
        {
            Service = service;
        }


        public string GetNameByUserId(Guid userId)
        {
            QueryExpression busca = new QueryExpression("systemuser");
            busca.ColumnSet.AddColumn("domainname");
            busca.Criteria.AddCondition("systemuserid", ConditionOperator.Equal, userId);
            EntityCollection usuarios = Service.RetrieveMultiple(busca);
            return usuarios.Entities.FirstOrDefault().Attributes["domainname"].ToString();
        }
        public Guid GetUserIdByName(string NomeUsuario)
        {
            QueryExpression busca = new QueryExpression("systemuser");
            busca.ColumnSet.AddColumn("systemuserid");
            busca.Criteria.AddCondition("domainname", ConditionOperator.Equal, NomeUsuario);
            EntityCollection usuarios = Service.RetrieveMultiple(busca);
            return usuarios.Entities.FirstOrDefault().Id;
        }

        public string GetCodeSymbolById(Guid moedaId)
        {
            QueryExpression busca = new QueryExpression("transactioncurrency");
            busca.ColumnSet.AddColumn("isocurrencycode");
            busca.Criteria.AddCondition("transactioncurrencyid", ConditionOperator.Equal, moedaId);
            EntityCollection moedas = Service.RetrieveMultiple(busca);
            return moedas.Entities.FirstOrDefault().Attributes["isocurrencycode"].ToString();
        }
        public Guid GetCurrencyIdByName(string moedaCodigo)
        {
            QueryExpression busca = new QueryExpression("transactioncurrency");
            busca.Criteria.AddCondition("isocurrencycode", ConditionOperator.Equal, moedaCodigo);
            EntityCollection moedas = Service.RetrieveMultiple(busca);
            return moedas.Entities.FirstOrDefault().Id;
        }

        public string GetCPFContacById(Guid contactId)
        {
            QueryExpression busca = new QueryExpression("contact");
            busca.ColumnSet.AddColumn("gp6_cpf");
            busca.Criteria.AddCondition("contactid", ConditionOperator.Equal, contactId);
            EntityCollection contatos = Service.RetrieveMultiple(busca);
            return contatos.Entities.FirstOrDefault().Attributes["gp6_cpf"].ToString();
        }
        public Guid GetContactIdByCpf(string cpfContato)
        {
            QueryExpression busca = new QueryExpression("contact");
            busca.Criteria.AddCondition("gp6_cpf", ConditionOperator.Equal, cpfContato);
            EntityCollection contatos = Service.RetrieveMultiple(busca);
            return contatos.Entities.FirstOrDefault().Id;
        }

        public string GetCnpjAccountById(Guid accountId)
        {
            QueryExpression busca = new QueryExpression("account");
            busca.ColumnSet.AddColumn("gp6_cnpj");
            busca.Criteria.AddCondition("accountid", ConditionOperator.Equal, accountId);
            EntityCollection contas = Service.RetrieveMultiple(busca);
            return contas.Entities.FirstOrDefault().Attributes["gp6_cnpj"].ToString();
        }
        public Guid GetAccountIdByCnpj(string Cnpj)
        {
            QueryExpression busca = new QueryExpression("account");
            busca.Criteria.AddCondition("gp6_cnpj", ConditionOperator.Equal, Cnpj);
            EntityCollection contas = Service.RetrieveMultiple(busca);
            return contas.Entities.FirstOrDefault().Id;
        }
    }
}
