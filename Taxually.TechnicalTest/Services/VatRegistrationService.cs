using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Services
{
    public class VatRegistrationService : IVatRegistrationService
    {
        private readonly IEnumerable<IVatRegistrationHandler> _handlers;

        public VatRegistrationService(IEnumerable<IVatRegistrationHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task RegisterAsync(VatRegistrationRequest request)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(request.Country));
            if (handler == null)
            {
                throw new InvalidOperationException("No handler found for specific country");
            }

            await handler.HandleAsync(request);
        }
    }
}
