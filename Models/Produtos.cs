using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosComprados.Models
{
    public class Produtos
    {
        [Key]
        public int ListaDeProdutosId { get; set; }
        public string NomeProduto { get; set; }
        public double PrecoProduto { get; set; }
        //public double? TotalProduto { get; set; }
        public bool ProdutoComprado { get; set; }
    }
}
