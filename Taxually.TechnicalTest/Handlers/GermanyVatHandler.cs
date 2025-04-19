using System.Xml.Serialization;
using Taxually.TechnicalTest.Helpers;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Handlers
{
    public class GermanyVatHandler : IVatRegistrationHandler
    {
        private IQueueClient _queueClient;
        private XmlHelper _xmlHelper;
        public bool CanHandle(string countryCode) => countryCode == "DE";

        public GermanyVatHandler(IQueueClient queueClient, XmlHelper xmlHelper)
        {
            _queueClient = queueClient;
            _xmlHelper = xmlHelper;
        }

        public async Task HandleAsync(VatRegistrationRequest request)
        {
            using (var stringwriter = new StringWriter())
            {
                var xml = _xmlHelper.SerializeToXml(request);
                // Queue xml doc to be processed
                await _queueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }
    }
}
