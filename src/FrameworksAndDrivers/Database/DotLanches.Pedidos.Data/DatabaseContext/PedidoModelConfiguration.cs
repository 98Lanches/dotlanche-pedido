using DotLanches.Pedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotLanches.Pedidos.DataMongo.DatabaseContext;

internal class PedidoModelConfiguration: IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x => x.ClienteCpf).IsRequired();
        builder.Property(x => x.Combos).IsRequired();
    }
}