// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProdutosComprados.Data;

namespace ProdutosComprados.Migrations
{
    [DbContext(typeof(ProdutosCompradosContext))]
    partial class ProdutosCompradosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProdutosComprados.Models.Produtos", b =>
                {
                    b.Property<int>("ListaDeProdutosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomeProduto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrecoProduto")
                        .HasColumnType("float");

                    b.Property<bool>("ProdutoComprado")
                        .HasColumnType("bit");

                    b.HasKey("ListaDeProdutosId");

                    b.ToTable("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
