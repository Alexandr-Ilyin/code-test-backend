using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Contracts;

namespace SlothEnterprise.ProductApplication.Implementation.Handlers
{
    public class SelectiveInvoiceDiscountHandler : ISellerApplicationHandler
    {
        private readonly ISelectInvoiceService invoiceService;

        public SelectiveInvoiceDiscountHandler(ISelectInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public int Handle(SellerApplication application)
        {
            return invoiceService.SubmitApplicationFor(application.CompanyData.Number.ToString(), application.SelectiveInvoiceDiscount.InvoiceAmount,
                application.SelectiveInvoiceDiscount.AdvancePercentage);
        }

        public bool CanHandle(SellerApplication application)
        {
            return application.SelectiveInvoiceDiscount != null;
        }
    }
}