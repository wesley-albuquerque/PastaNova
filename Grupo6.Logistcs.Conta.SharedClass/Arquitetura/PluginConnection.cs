using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Grupo6.Logistcs.Arquitetura
{
    public class PluginConnection
    {
        public static IOrganizationService GetService()
        {
            string connectionString =
                "AuthType=OAuth;" +
                "Username=jessica@tccgrupo6dynacoop.onmicrosoft.com;" +
                "Password=Tcc@2023;" +
                "Url=https://logisticssecundario.crm2.dynamics.com/;" +
                "AppId=47d414b0-dc10-4025-98e4-2e63336ce1e9;" +
                "RedirectUri=app://47e0e0b2-6bb5-ed11-a37f-000d3a888d06;";


            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);


            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}
