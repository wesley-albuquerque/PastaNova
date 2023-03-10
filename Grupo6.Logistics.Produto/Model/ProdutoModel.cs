using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Grupo6.Logistics.Produto.Model
{
    public class ProductModel
    {
        public string Nome { get; set; }
        public string ProdutoId { get; set; }
        public DateTime? ValidoAPartir { get; set; }
        public DateTime? ValidoAte { get; set; }
        public Guid GrupoUnidade { get; set; }
        public Guid UnidadePadrao { get; set; }
        public int SuporteDecimais { get; set; }
        public decimal? CustoAtual { get; set; }
        public decimal? PrecoDeLista { get; set; }
    }
}
