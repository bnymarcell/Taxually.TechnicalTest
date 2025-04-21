using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Models.Requests;
using Taxually.TechnicalTest.Services;
using Xunit;

namespace Taxually.TechnicalTest.Tests
{
    public class VatRegistrationServiceTest
    {
        [Fact]
        public async Task RegisterAsync_ShouldCallCorrectHandler()
        {
            var mockHandler = new Mock<IVatRegistrationHandler>();
            mockHandler.Setup(h => h.CanHandle("GB")).Returns(true);
            mockHandler.Setup(h => h.HandleAsync(It.IsAny<VatRegistrationRequest>())).Returns(Task.CompletedTask);

            var service = new VatRegistrationService(new List<IVatRegistrationHandler> { mockHandler.Object });

            var request = new VatRegistrationRequest { Country = "GB" };

            await service.RegisterAsync(request);

            mockHandler.Verify(h => h.HandleAsync(request), Times.Once);

        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowNoHandlerFound()
        {
            var mockHandler = new Mock<IVatRegistrationHandler>();
            mockHandler.Setup(h => h.CanHandle("FR")).Returns(false);

            var service = new VatRegistrationService(new List<IVatRegistrationHandler>() { mockHandler.Object });
            var request = new VatRegistrationRequest { Country = "FR" };
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAsync(request));
        }

        [Fact]
        public async Task RegisterAsync_ShouldCallOnlyMatchinHandler()
        {
            var mockFrance = new Mock<IVatRegistrationHandler>();
            var mockBrittain = new Mock<IVatRegistrationHandler>();

            mockFrance.Setup(h => h.CanHandle("FR")).Returns(true);
            mockFrance.Setup(h => h.HandleAsync(It.IsAny<VatRegistrationRequest>())).Returns(Task.CompletedTask);

            mockBrittain.Setup(h => h.CanHandle("FR")).Returns(false);

            var service = new VatRegistrationService(new[] { mockFrance.Object, mockBrittain.Object });

            var request = new VatRegistrationRequest { Country = "FR" };

            await service.RegisterAsync(request);

            mockFrance.Verify(h => h.HandleAsync(request), Times.Once);

            mockBrittain.Verify(h => h.HandleAsync(It.IsAny<VatRegistrationRequest>()), Times.Never);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowWhenRequestIsNull()
        {
            var service = new VatRegistrationService(new List<IVatRegistrationHandler>());

            await Assert.ThrowsAsync <InvalidOperationException>(() => service.RegisterAsync(null));
        }
    }
}
