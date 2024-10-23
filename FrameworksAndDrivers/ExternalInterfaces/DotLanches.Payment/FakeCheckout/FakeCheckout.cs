using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.ExternalInterfaces;

namespace DotLanches.Payment.FakeCheckout
{
    public class FakeCheckout : ICheckout
    {
        public string RequestQrCode(Pagamento pagamento)
        {
            //proccess time - 3s
            Thread.Sleep(3000);
            if (pagamento == null) return String.Empty;
            return "DecodedQRCode";
        }
    }
}
