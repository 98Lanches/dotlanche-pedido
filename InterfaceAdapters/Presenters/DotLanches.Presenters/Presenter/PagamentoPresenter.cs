using DotLanches.Presenters.Dtos;

namespace DotLanches.Presenters.Mappers
{
    public static class PagamentoPresenter
    {
        public static PagamentoViewModel GetPagamentoViewModel(bool? isAccepted, DateTime? registeredAt, int? queueKey = null)
        {
            var viewModel = new PagamentoViewModel()
            {
                IsAccepted = isAccepted,
                RegisteredAt = registeredAt,
                QueueKey = queueKey
            };

            return viewModel;
        }
    }
}