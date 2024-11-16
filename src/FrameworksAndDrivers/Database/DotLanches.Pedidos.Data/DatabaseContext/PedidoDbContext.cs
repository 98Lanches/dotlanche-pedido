using DotLanches.Pedidos.DataMongo.DatabaseContext;
using DotLanches.Pedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotLanches.Pedidos.DataSql.DatabaseContext;

public class PedidoDbContext : DbContext
{
    public DbSet<Pedido> Pedido { get; set; }

    public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
        :base(options)
    {
            
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PedidoModelConfiguration().Configure(modelBuilder.Entity<Pedido>());
    }
}