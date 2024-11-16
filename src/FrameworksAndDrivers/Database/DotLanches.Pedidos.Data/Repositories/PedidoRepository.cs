using DotLanches.Pedidos.DataMongo.Exceptions;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DotLanches.Pedidos.DataMongo.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<Pedido> _pedidosCollection;

        public PedidoRepository(IMongoDatabase database)
        {
            _pedidosCollection = database.GetCollection<Pedido>("Pedidos");
        }

        public async Task Add(Pedido pedido)
        {
            await _pedidosCollection.InsertOneAsync(pedido);
        }

        public async Task<Pedido?> GetById(Guid id)
        {
            return await _pedidosCollection
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Pedido> Update(Pedido pedido)
        {
            var filter = Builders<Pedido>.Filter.Eq(p => p.Id, pedido.Id);
            var result = await _pedidosCollection.ReplaceOneAsync(filter, pedido);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return pedido;
        }
    }
}
