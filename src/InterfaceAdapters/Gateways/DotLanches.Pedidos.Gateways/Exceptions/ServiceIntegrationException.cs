namespace DotLanches.Pedidos.Gateways.Exceptions
{
    public class ServiceIntegrationException : Exception
    {
        private const string BaseMessageTemplate = "Could not integrate with service {0}.";
        private const string ExtraMessageTemplate = "Error: {0}";

        public ServiceIntegrationException(string serviceName, string? ErrorMessage = null)
            : base($"{string.Format(BaseMessageTemplate, serviceName)} {(ErrorMessage != null ? string.Format(ExtraMessageTemplate, ErrorMessage) : string.Empty)}")
        {
        }
    }
}