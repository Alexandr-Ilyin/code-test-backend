using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Implementation.Handlers;

namespace SlothEnterprise.ProductApplication.Implementation
{
    public class ProductApplicationServiceFactory
    {
        public ProductApplicationService CreateProductApplicationService(IBusinessLoansService businessLoansService,
            IConfidentialInvoiceService confidentialInvoiceService,
            ISelectInvoiceService selectInvoiceService)
        {
            return new ProductApplicationService(new ISellerApplicationHandler[]
            {
                new BuisnessLoansHandler(businessLoansService),
                new ConfidentialInvoiceServiceHandler(confidentialInvoiceService),
                new SelectiveInvoiceDiscountHandler(selectInvoiceService),
            });
        }
    }
}