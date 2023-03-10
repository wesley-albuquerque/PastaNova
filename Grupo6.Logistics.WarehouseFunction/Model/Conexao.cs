using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.WarehouseFunction.Model
{
    public class Conexao
    {
        public ServiceClient Service { get; set; }

        public void GetService()
        {
            string url = "logisticsprincipal";
            string clientId = "47d414b0-dc10-4025-98e4-2e63336ce1e9";
            string clientSecret = "Nd~8Q~_eyoUkcgZcnbkeDc4DTPZ_repzoboBzb5f";
            string stringConnection = $@"AuthType=ClientSecret;
                                      url=https://{url}.crm2.dynamics.com/;
                                      ClientId={clientId};
                                      ClientSecret={clientSecret};";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServiceClient serviceClient = new ServiceClient(stringConnection);

            Service = serviceClient;
        }
    }
}
