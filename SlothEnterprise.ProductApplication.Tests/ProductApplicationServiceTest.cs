using System;
using System.Collections.Generic;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Contracts;
using SlothEnterprise.ProductApplication.Implementation;
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

        [Fact]
        public void CreateSelectiveInvoiceDiscountApplication__Handle__AssertIsRedirectedToBusinessLoansService()
        {
            var selectiveInvoiceDiscount = new SelectiveInvoiceDiscount() { InvoiceAmount = 1, AdvancePercentage = 2, Id = 3 };
            var application = new SellerApplication(selectiveInvoiceDiscount, GetCompanyData());
            var selectiveInvoiceService = new Mock<ISelectInvoiceService>();

            selectiveInvoiceService.Setup(svc => svc.SubmitApplicationFor(
                It.IsAny<string>(), 1,2)).Returns(1);

            var applicationService = applicationServiceFactory.CreateProductApplicationService(
                new Mock<IBusinessLoansService>().Object,
                new Mock<IConfidentialInvoiceService>().Object,
                selectiveInvoiceService.Object
            );
            var applicationResult = applicationService.SubmitApplicationFor(application);
            selectiveInvoiceService.VerifyAll();
            Assert.Equal(1, applicationResult);
        }

        [Fact]
        public void CreateBuisnessLoanApplication__Handle__AssertIsRedirectedToBusinessLoansService()
        {
            var invoiceDiscount = new BusinessLoans() { Id = 1, LoanAmount = 2, InterestRatePerAnnum = 3 };
            var application = new SellerApplication(invoiceDiscount, GetCompanyData());

            var buisnessLoanService = new Mock<IBusinessLoansService>();
            buisnessLoanService.Setup(svc => svc.SubmitApplicationFor(
                It.Is<CompanyDataRequest>(r=>
                    r.CompanyFounded.Equals(GetCompanyData().Founded)&&
                    r.CompanyName.Equals(GetCompanyData().Name) &&
                    r.CompanyNumber.Equals(GetCompanyData().Number) &&
                    r.DirectorName.Equals(GetCompanyData().DirectorName) 
                    ), 
                It.Is<LoansRequest>(r=>r.LoanAmount==2 && r.InterestRatePerAnnum==3))).Returns(GetValidApplicationResult());

            var applicationService = applicationServiceFactory.CreateProductApplicationService(
                buisnessLoanService.Object,
                new Mock<IConfidentialInvoiceService>().Object,
                new Mock<ISelectInvoiceService>().Object
            );
            var applicationResult = applicationService.SubmitApplicationFor(application);
            buisnessLoanService.VerifyAll();
            Assert.Equal(1, applicationResult);
        }

        private IApplicationResult GetValidApplicationResult()
        {
            return new TestApplicationResult() { Success = true, ApplicationId = 1 };
        }

        private static SellerCompanyData GetCompanyData()
        {
            return new SellerCompanyData()
            {
                Founded = new DateTime(1990, 1, 1),
                DirectorName = "TestDirectorName",
                Name = "TestName",
                Number = 1000
            };
        }
    }

    internal class TestApplicationResult : IApplicationResult
    {
        public int? ApplicationId { get; set; }
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }
}