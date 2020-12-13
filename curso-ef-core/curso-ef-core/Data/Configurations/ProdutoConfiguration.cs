using curso_ef_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_ef_core.Data.Configurations
{
  public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
  {
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
      builder.ToTable("produtos");
      builder.HasKey(produto => produto.Id).HasName("id");

      builder.Property(produto => produto.Id).HasColumnName("id");
      builder.Property(produto => produto.Ativo).HasColumnName("ativo").HasDefaultValue(true).IsRequired();
      builder.Property(produto => produto.CodigoBarras).HasColumnName("codigo_barras").HasColumnType("VARCHAR(14)").IsRequired();
      builder.Property(produto => produto.Descricao).HasColumnName("descricao").HasColumnType("VARCHAR(60)").IsRequired();
      builder.Property(produto => produto.Valor).HasColumnName("valor").HasColumnType("DECIMAL(10,2)").IsRequired();
      builder.Property(produto => produto.TipoProduto).HasColumnName("tipo_produto").HasConversion<string>();
    }
  }
}
