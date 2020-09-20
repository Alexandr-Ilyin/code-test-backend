using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Internals
{
    public class ConfidentialInvoiceServiceHandler : ISellerApplicationHandler
    {
        private IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceServiceHandler(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
        }

        public int Handle(SellerApplication application)
        {
            var invoiceDiscount = application.ConfidentialInvoiceDiscount;
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                new CompanyDataRequest
                {
                    CompanyFounded = application.CompanyData.Founded,
                    CompanyNumber = application.CompanyData.Number,
                    CompanyName = application.CompanyData.Name,
                    DirectorName = application.CompanyData.DirectorName
                }, invoiceDiscount.TotalLedgerNetworth, invoiceDiscount.AdvancePercentage,
                invoiceDiscount.VatRate);
            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }

        public bool CanHandle(SellerApplication application)
        {
            return application.ConfidentialInvoiceDiscount != null;
        }
    }
}