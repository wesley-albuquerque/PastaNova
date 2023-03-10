using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Grupo6.Logistics.Arquitetura;
using Microsoft.Xrm.Sdk;

namespace Grupo6.Logistics.ProdutoAmbiente2.Plugin
{
    public class ProductManagerEnv2 : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity product = (Entity)this.Context.InputParameters["Target"];

            if(!product.Contains("gp6_integracao") || product.GetAttributeValue<Boolean>("gp6_integracao") == false)
            {              
                throw new InvalidPluginExecutionException ("Essa ação não é permitida para esse ambiente.");
            }
        }
    }
}
