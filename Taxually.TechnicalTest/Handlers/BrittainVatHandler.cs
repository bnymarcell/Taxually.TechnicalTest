using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Handlers
{
    public class BrittainVatHandler : IVatRegistrationHandler
    {
        private readonly IHttpClient _httpClient;

        public BrittainVatHandler(IHttpClient httpClient)
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
