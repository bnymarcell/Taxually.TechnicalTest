using System.Text;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Handlers
{
    public class FranceVatHandler : IVatRegistrationHandler
    {
        private readonly IQueueClient _queueClient;
       
        public bool CanHandle(string countryCode) => countryCode == "FR";

        public FranceVatHandler(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task HandleAsync(VatRegistrationRequest request)
        {
            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            // Queue file to be processed
            await _queueClient.EnqueueAsync("vat-registration-csv", csv);
        }
        
    }
}
