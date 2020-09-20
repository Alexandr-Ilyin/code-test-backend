using System.Collections.Generic;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Contracts;
using SlothEnterprise.ProductApplication.Implementation;
using SlothEnterprise.ProductApplication.Implementation.Handlers;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationServiceTest
    {
        readonly ProductApplicationServiceFactory applicationServiceFactory = new ProductApplicationServiceFactory();

        [Fact]
        public void CreateDiscountRequestApplication__Handle__AssertIsRedirectedToConfidentialInvoiceService()
        {
            var invoiceDiscount = new ConfidentialInvoiceDiscount() { Id = 1, AdvancePercentage = 2, TotalLedgerNetworth = 3, VatRate = 4 };
            var application = new SellerApplication(invoiceDiscount, GetCompanyData());
            var confidentialInvoiceService = new Mock<IConfidentialInvoiceService>();

            confidentialInvoiceService.Setup(svc => svc.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), 2, 4)).Returns(GetValidApplicationResult());
            
            var applicationService = applicationServiceFactory.CreateProductApplicationService(
                new Mock<IBusinessLoansService>().Object,
                confidentialInvoiceService.Object,
                new Mock<ISelectInvoiceService>().Object
            );
            var applicationResult = applicationService.SubmitApplicationFor(application);
            confidentialInvoiceService.VerifyAll();
            Assert.Equal(1, applicationResult);
        }

        private IApplicationResult GetValidApplicationResult()
        {
            return new TestApplicationResult() { Success = true, ApplicationId = 1};
        }

        private static SellerCompanyData GetCompanyData()
        {
            return new SellerCompanyData();
        }
    }

    internal class TestApplicationResult : IApplicationResult
    {
        public int? ApplicationId { get; set; }
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }
}