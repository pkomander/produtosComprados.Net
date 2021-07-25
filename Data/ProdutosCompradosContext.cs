using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProdutosComprados.Models;

namespace ProdutosComprados.Data
{
    public class ProdutosCompradosContext : DbContext
    {
        public ProdutosCompradosContext (DbContextOptions<ProdutosCompradosContext> options)
            : base(options)
        {
        }

        public DbSet<ProdutosComprados.Models.Produtos> Produtos { get; set; }
    }
}
