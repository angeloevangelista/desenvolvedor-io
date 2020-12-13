using System;
using System.Linq;
using curso_ef_core.Data.Configurations;
using curso_ef_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace curso_ef_core.Data
{
  public class ApplicationContext : DbContext
  {
    private static readonly ILoggerFactory _logger = LoggerFactory.Create(
      configure => configure.AddConsole()
    );

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder
        .UseLoggerFactory(_logger)
        .EnableSensitiveDataLogging()
        .UseSqlServer(
          "Server=127.0.0.1,1433;Database=curso_ef_core;User Id=sa;Password=DockerMsSql127!",
          predicate => predicate.EnableRetryOnFailure(
            maxRetryCount: 2,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null
          ).MigrationsHistoryTable("curso_ef_core_migrations_history")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

      this.MapearPropriedadesEsquecidas(modelBuilder);
    }

    private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
    {
      foreach (var entity in modelBuilder.Model.GetEntityTypes())
      {
        var properties = entity
          .GetProperties()
          .Where(predicate => predicate.ClrType == typeof(string));

        foreach (var property in properties)
        {
          if (
            string.IsNullOrEmpty(property.GetColumnType())
            && !property.GetMaxLength().HasValue)
          {
            // property.SetMaxLength(100);
            property.SetColumnType("VARCHAR(100)");
          }
        }
      }
    }
  }
}
