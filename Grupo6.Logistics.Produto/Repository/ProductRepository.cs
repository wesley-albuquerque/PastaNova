using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo6.Logistics.Produto.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Grupo6.Logistics.Produto.Repository
{
    public class ProductRepository
    {
        private IOrganizationService Service { get; set; }
        private IOrganizationService ServicePrincipal { get; set; }
        private ITracingService tracingService { get; set; }
        private string logicalname { get; set; }
        public ProductRepository(IOrganizationService service, ITracingService tracingService, IOrganizationService ServicePrincipal )
        {
            this.tracingService = tracingService;
            this.Service = service;
            this.ServicePrincipal = ServicePrincipal;
            this.logicalname = "product";
        }

        public Guid Create(ProductModel productModel)
        {
            Entity product = new Entity(this.logicalname);
            product["gp6_integracao"] = true;

            product["name"] = productModel.Nome;
            product["productnumber"] = productModel.ProdutoId;

            if(productModel.ValidoAPartir != null)
                product["validfromdate"] = productModel.ValidoAPartir.Value;

            if(productModel.ValidoAte != null)
                product["validtodate"] = productModel.ValidoAte.Value;


            var id = RecuperaID("uomschedule", "name", "name",productModel.GrupoUnidade);
            product["defaultuomscheduleid"] = new EntityReference("uomschedule", id);
            var id2 = RecuperaID("uom", "name", "name", productModel.UnidadePadrao);
            product["defaultuomid"] = new EntityReference("uom", id2);
            product["quantitydecimal"] = productModel.SuporteDecimais;

            if (productModel.CustoAtual != null)
                product["currentcost"] = new Money(productModel.CustoAtual.Value);

            if (productModel.PrecoDeLista != null)
                product["price"] = new Money(productModel.PrecoDeLista.Value);

                Guid productId = Service.Create(product);
            return productId;
        }
        public string GetRecord(Guid id, string logicalName)
        {
            return ServicePrincipal.Retrieve(logicalName, id, new ColumnSet("name")).GetAttributeValue<string>("name");
        }
        public Guid RecuperaID( string logicalname, string column, string condicao, Guid idRecord)
        {
            var name = GetRecord(idRecord, logicalname);
            QueryExpression query = new QueryExpression(logicalname);
            query.Criteria.AddCondition(condicao, ConditionOperator.Equal, name);
            tracingService.Trace("antes de buscar");
            var response = Service.RetrieveMultiple(query);
            tracingService.Trace("Total:"+response.Entities.Count);
            var id = Guid.Empty;
            if(response != null && response.Entities.Any()) {
                id = response.Entities[0].Id;
            }
            return id;
        }
    }
}
