using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        private readonly IEnumerable<IVatRegistrationHandler> _handlers;

        public VatRegistrationController(IEnumerable<IVatRegistrationHandler> handlers)
        {
            _handlers = handlers;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterVat([FromBody]VatRegistrationRequest request)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(request.Country));
            if (handler == null)
            {
                return BadRequest("Input was incorrect");
            }

            await handler.HandleAsync(request);
           return Ok();
        }
    }


}
