using Microsoft.EntityFrameworkCore;
using curso_ef_core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_ef_core.Data.Configurations
{
  public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
  {
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
      builder.ToTable("pedido_items");
      builder.HasKey(item => item.Id);

      builder.Property(item => item.Id).HasColumnName("id");
      builder.Property(item => item.PedidoId).HasColumnName("pedido_id");
      builder.Property(item => item.ProdutoId).HasColumnName("produto_id");
      builder.Property(item => item.Quantidade).HasColumnName("quantidade").HasDefaultValue(1).IsRequired();
      builder.Property(item => item.Valor).HasColumnName("valor").HasColumnType("DECIMAL(10,2)").IsRequired();
      builder.Property(item => item.Desconto).HasColumnName("desconto").HasColumnType("DECIMAL(10,2)").IsRequired();
    }
  }
}
