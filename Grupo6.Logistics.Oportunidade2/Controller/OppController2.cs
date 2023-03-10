using Grupo6.Logistics.Oportunidade2.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Oportunidade2.Controller
{
    public class OppController2
    {

        public Oportunidade Oportunidade { get; set; }

        public OppController2(IOrganizationService serviceClient)
        {
            Oportunidade = new Oportunidade(serviceClient);

        }

        public string GetNameByUserId(Guid userId)
        {
            return Oportunidade.GetNameByUserId(userId);
        }

        public Guid GetUserIdByName(string NomeUsuario)
        {
            return Oportunidade.GetUserIdByName(NomeUsuario);
        }

        public string GetCodeSymbolById(Guid moedaId) 
        {
            return Oportunidade.GetCodeSymbolById(moedaId);
        }

        public Guid GetCurrencyIdByName(string codMoeda)
        {
            return Oportunidade.GetCurrencyIdByName(codMoeda);
        }
        public string GetCPFContacById(Guid contactId)
        {
            return Oportunidade.GetCPFContacById(contactId);
        }

        public Guid GetContactIdByCpf(string cpfContato)
        {
            return Oportunidade.GetContactIdByCpf(cpfContato);
        }

        public string GetCnpjAccountById(Guid accounId)
        {
            return Oportunidade.GetCnpjAccountById(accounId);
        }

        public Guid GetAccountIdByCnpj(string cnpj)
        {
            return Oportunidade.GetAccountIdByCnpj(cnpj);
        }
    }
}
