using DotLanches.Pedido.DataMongo.Exceptions;
using DotLanches.Pedido.Domain.Entities;
using DotLanches.Pedido.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DotLanches.Pedido.DataMongo.Repositories
{
    internal class PedidoRepository : IPedidoRepository
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

        public async Task<IEnumerable<Pedido>> GetPedidosQueue()
        {
            var queueStatusIds = new[] { EStatus.Pronto, EStatus.EmPreparo, EStatus.Recebido };

            var filter = Builders<Pedido>.Filter.In(p => p.Status, queueStatusIds);
            var sort = Builders<Pedido>.Sort.Descending(p => p.Status).Ascending(p => p.CreatedAt);

            return await _pedidosCollection
                .Find(filter)
                .Sort(sort)
                .ToListAsync();
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

        public async Task<Pedido> UpdateStatus(Pedido pedido)
        {
            var filter = Builders<Pedido>.Filter.Eq(p => p.Id, pedido.Id);
            var update = Builders<Pedido>.Update.Set(p => p.Status, pedido.Status);

            var result = await _pedidosCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return pedido;
        }
    }
}
