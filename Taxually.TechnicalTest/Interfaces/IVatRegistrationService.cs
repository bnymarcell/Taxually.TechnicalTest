using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Interfaces
{
    public interface IVatRegistrationService
    {
       Task RegisterAsync(VatRegistrationRequest request);
    }
}
