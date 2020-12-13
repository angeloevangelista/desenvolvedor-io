using curso_ef_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_ef_core.Data.Configurations
{
  public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
  {
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
      builder.ToTable("pedidos");
      builder.HasKey(pedido => pedido.Id);

      builder.Property(pedido => pedido.Id).HasColumnName("id");
      builder.Property(pedido => pedido.ClienteId).HasColumnName("cliente_id");
      builder.Property(pedido => pedido.IniciadoEm).HasColumnName("iniciado_em").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd().IsRequired();
      builder.Property(pedido => pedido.FinalizadoEm).HasColumnName("finalizado_em");
      builder.Property(pedido => pedido.Status).HasColumnName("status").HasConversion<string>().IsRequired();
      builder.Property(pedido => pedido.TipoFrete).HasColumnName("tipo_frete").HasConversion<int>().IsRequired();
      builder.Property(pedido => pedido.Observacao).HasColumnName("observacao").HasColumnType("VARCHAR(512)");

      builder
        .HasMany(pedido => pedido.Itens)
        .WithOne(item => item.Pedido)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
