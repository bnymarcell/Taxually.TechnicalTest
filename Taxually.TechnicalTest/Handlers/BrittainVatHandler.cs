using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Handlers
{
    public class BrittainVatHandler : IVatRegistrationHandler
    {
        private readonly TaxuallyHttpClient _httpClient;

        public BrittainVatHandler(TaxuallyHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public bool CanHandle(string countryCode) => countryCode == "GB";

        public async Task HandleAsync(VatRegistrationRequest request)
        {
            var httpClient = _httpClient;
            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }

    }
}
