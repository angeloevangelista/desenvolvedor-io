using curso_ef_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_ef_core.Data.Configurations
{
  public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
  {
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
      builder.ToTable("clientes");
      builder.HasKey(cliente => cliente.Id);

      builder.Property(cliente => cliente.Id).HasColumnName("id");
      builder.Property(cliente => cliente.Telefone).HasColumnName("telefone").HasColumnType("CHAR(11)");
      builder.Property(cliente => cliente.Cidade).HasColumnName("cidade").HasMaxLength(60).IsRequired();
      builder.Property(cliente => cliente.CEP).HasColumnName("cep").HasColumnType("CHAR(8)").IsRequired();
      builder.Property(cliente => cliente.Estado).HasColumnName("estado").HasColumnType("CHAR(2)").IsRequired();
      builder.Property(cliente => cliente.Nome).HasColumnName("nome").HasColumnType("VARCHAR(100)").IsRequired();

      builder.HasIndex(cliente => cliente.Telefone).HasName("index_cliente_telefone");
    }
  }
}
