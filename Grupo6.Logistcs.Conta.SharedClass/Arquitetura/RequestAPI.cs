using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Grupo6.Logistcs.Conta.SharedClass.Extensions;
using System.Web.UI.WebControls;


namespace Grupo6.Logistcs.Conta.SharedClass.Arquitetura
{
    public class RequestAPI
    {
        public HttpClient Client { get; set; }
        private Guid OrganizationIdPrincipal = new Guid("0f82db9d-bbb6-ed11-9a84-0022483791e2");
        //private Guid OrganizationIdSecundario = new Guid("f90f4d59-39b5-ed11-9a84-0022483791e7");
        public string Ambiente { get; set; }
        public string Token { get; set; }
        public RequestAPI(Guid organizationId)
        {
            Client = new HttpClient();

            if(OrganizationIdPrincipal == organizationId) 
            {
                Ambiente = "secundario";
            }
            else
            {
                Ambiente = "principal";
            }
        }
        public void GetToken()
        {
            
            var request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/d4ab811a-8099-400e-af26-b4452ec3ae1c/oauth2/token");
            request.Headers.Add("Cookie", "fpc=AnAiZYWtN-dMoTJetLGZc2Bq6aNiAQAAAI_bl9sOAAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("client_id", "47d414b0-dc10-4025-98e4-2e63336ce1e9"));
            collection.Add(new KeyValuePair<string, string>("client_secret", "Nd~8Q~_eyoUkcgZcnbkeDc4DTPZ_repzoboBzb5f"));
            collection.Add(new KeyValuePair<string, string>("resource", $"https://logistics{Ambiente}.crm2.dynamics.com/"));
            collection.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = Client.SendAsync(request).Result;
            var httpResponse = response.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                var resposta = response.Content.ReadAsStringAsync().Result;
                RequestToken json = JsonConvert.DeserializeObject<RequestToken>(resposta);
                
                
                Token = json.TokenType + " " + json.AccessToken;

            }
            else
            {
                throw new Exception("Erro ao obter o token de acesso");
            }

            
        }

        public string BuscarMaiorCodOppNaApi()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://logistics{Ambiente}.crm2.dynamics.com/api/data/v9.2/opportunities?$select=gp6_codopp&$orderby=gp6_codopp desc&$top=1");
            request.Headers.Add("OData-MaxVersion", "4.0");
            request.Headers.Add("OData-Version", "4.0");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", Token);
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            

            if (response.IsSuccessStatusCode)
            {
                 var resposta = response.Content.ReadAsStringAsync().Result;
                RequestOpp json = JsonConvert.DeserializeObject<RequestOpp>(resposta);
                return json.Value[0].Gp6Codopp;
            }
            else
            {
                throw new Exception("Erro ao consultar API");
            }

            
        }

    }
}
