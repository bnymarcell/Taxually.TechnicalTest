using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Interfaces
{
    public interface IVatRegistrationHandler
    {
        bool CanHandle(string companyId);

        Task HandleAsync(VatRegistrationRequest request);
    }
}
