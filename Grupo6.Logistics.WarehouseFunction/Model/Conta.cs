using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grupo6.Logistics.WarehouseFunction.Model
{
    public class Conta
    {
        //public Entity Account = new Entity("account");
        //public string LogicalName { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string Site { get; set; }
        public string CNPJ_ContaPrimaria { get; set; }
        public string SimboloAcao { get; set; }
        public string DataUltVisita { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string DDD { get; set; }
        public string CPF_Contato { get; set; }
        public int Setor { get; set; }
        public string CNAE { get; set; }
        public int Proriedade { get; set; }
        public string Descricao { get; set; }
        


        public void CriaAttConta()
        {
            Conexao conexao = new Conexao();
            conexao.GetService();
            Regex regexClear = new Regex(@"[^\d]");
            Regex regexCPF = new Regex(@"(\d{3})(\d{3})(\d{3})(\d{2})");
            Regex regexCNPJ = new Regex(@"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})");
            CNPJ = regexClear.Replace(CNPJ, "");
            
            if (CNPJ.Length != 14)
            {
                throw new Exception("CNPJ inválido");
            }
            
            CNPJ = regexCNPJ.Replace(CNPJ, "$1.$2.$3/$4-$5");
            QueryExpression busca = new QueryExpression("account");
            busca.ColumnSet = new ColumnSet("gp6_cnpj");
            busca.Criteria.AddCondition("gp6_cnpj", ConditionOperator.Equal, CNPJ);
            EntityCollection contas = conexao.Service.RetrieveMultiple(busca);

            if (contas.Entities.Count > 0)
            {
                Entity conta = new Entity("account");
                conta.Id = contas.Entities.FirstOrDefault().Id;
                if (!string.IsNullOrEmpty(Nome))
                    conta.Attributes["name"] = Nome;
                if (!string.IsNullOrEmpty(Telefone))
                    conta.Attributes["telephone1"] = Telefone;
                if (!string.IsNullOrEmpty(Fax))
                    conta.Attributes["fax"] = Fax;
                if (!string.IsNullOrEmpty(Site))
                    conta.Attributes["websiteurl"] = Site;
                if (!string.IsNullOrEmpty(CNPJ_ContaPrimaria))
                {
                    CNPJ_ContaPrimaria = regexCNPJ.Replace(CNPJ_ContaPrimaria, "$1.$2.$3/$4-$5");
                    conta.Attributes["parentaccountid"] = new EntityReference("account", GetAccountIdByCNPJ(CNPJ_ContaPrimaria));
                }
                if (!string.IsNullOrEmpty(SimboloAcao))
                    conta.Attributes["tickersymbol"] = SimboloAcao;
                if (!string.IsNullOrEmpty(DataUltVisita))
                    conta.Attributes["gp6_dataultimavisita"] = DataUltVisita;
                if (!string.IsNullOrEmpty(CEP))
                    conta.Attributes["address1_postalcode"] = CEP;
                if (!string.IsNullOrEmpty(Logradouro))
                    conta.Attributes["address1_line1"] = Logradouro;
                if (!string.IsNullOrEmpty(Complemento))
                    conta.Attributes["address1_line2"] = Complemento;
                if (!string.IsNullOrEmpty(Numero))
                    conta.Attributes["address1_line3"] = Numero;
                if (!string.IsNullOrEmpty(Bairro))
                    conta.Attributes["address1_upszone"] = Bairro;
                if (!string.IsNullOrEmpty(Cidade))
                    conta.Attributes["address1_city"] = Cidade;
                if (!string.IsNullOrEmpty(Uf))
                    conta.Attributes["address1_stateorprovince"] = Uf;
                if (string.IsNullOrEmpty(Ibge))
                    conta.Attributes["gp6_ibge"] = Ibge;
                if (!string.IsNullOrEmpty(DDD))
                    conta.Attributes["gp6_ddd"] = DDD;
                if (!string.IsNullOrEmpty(CPF_Contato))
                {
                    CPF_Contato = regexCPF.Replace(CPF_Contato, "$1.$2.$3-$4");
                    conta.Attributes["primarycontactid"] = new EntityReference("contact", GetAccountIdByCNPJ(CPF_Contato));
                }
                if (!string.IsNullOrEmpty(Telefone))
                    conta.Attributes["telephone1"] = Telefone;
                if (!string.IsNullOrEmpty(Telefone))
                    conta.Attributes["telephone1"] = Telefone;

            }
        }

        public Guid GetAccountIdByCNPJ(string cnpj)
        {
            Conexao conexao = new Conexao();
            conexao.GetService();
            QueryExpression busca = new QueryExpression("account");
            busca.ColumnSet = new ColumnSet("gp6_cnpj");
            busca.Criteria.AddCondition("gp6_cnpj", ConditionOperator.Equal, cnpj);
            EntityCollection contas = conexao.Service.RetrieveMultiple(busca);

            return contas.Entities.FirstOrDefault().Id;
        }
        public Guid GetContactIdByCPF(string cpf)
        {
            Conexao conexao = new Conexao();
            conexao.GetService();
            QueryExpression busca = new QueryExpression("contact");
            busca.ColumnSet = new ColumnSet("gp6_cpf");
            busca.Criteria.AddCondition("gp6_cpf", ConditionOperator.Equal, cpf);
            EntityCollection contas = conexao.Service.RetrieveMultiple(busca);

            return contas.Entities.FirstOrDefault().Id;
        }
    }
}
