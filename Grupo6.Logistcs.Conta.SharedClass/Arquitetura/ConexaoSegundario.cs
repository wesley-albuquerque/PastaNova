using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Grupo6.Logistcs.Conta.SharedClass.Arquitetura
{
    public class ConexaoSegundario
    {
        public CrmServiceClient GetService()
        {

            string url = "logisticssecundario";
            string clientId = "47d414b0-dc10-4025-98e4-2e63336ce1e9";
            string clientSecret = "Nd~8Q~_eyoUkcgZcnbkeDc4DTPZ_repzoboBzb5f";
            string stringConnection = $@"AuthType=ClientSecret;
                                      url=https://{url}.crm2.dynamics.com/;
                                      ClientId={clientId};
                                      ClientSecret={clientSecret};";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CrmServiceClient serviceClient = new CrmServiceClient(stringConnection);
            

            return serviceClient;
        }
    }
}
