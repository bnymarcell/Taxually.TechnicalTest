using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Handlers
{
    public class BrittainVatHandler : IVatRegistrationHandler
    {
        public bool CanHandle(string countryCode) => countryCode == "GB";

        public async Task HandleAsync(VatRegistrationRequest request)
        {
            var httpClient = new TaxuallyHttpClient();
            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }

    }
}
