using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Applications
{

    public interface ISellerApplication
    {
        IProduct Product { get;  }
        ISellerCompanyData CompanyData { get;  }
    }

    public class SellerApplication : ISellerApplication
    {
        public SellerApplication(BusinessLoans businessLoans, ISellerCompanyData companyData)
        {
            BusinessLoans = businessLoans;
            CompanyData = companyData;
        }

        public SellerApplication(ConfidentialInvoiceDiscount confidentialInvoiceDiscount, ISellerCompanyData companyData)
        {
            ConfidentialInvoiceDiscount = confidentialInvoiceDiscount;
            CompanyData = companyData;
        }

        public SellerApplication(SelectiveInvoiceDiscount selectiveInvoiceDiscount, ISellerCompanyData companyData)
        {
            SelectiveInvoiceDiscount = selectiveInvoiceDiscount;
            CompanyData = companyData;
        }

        public IProduct Product
        {
            get { return (IProduct)BusinessLoans ?? (IProduct)ConfidentialInvoiceDiscount ?? SelectiveInvoiceDiscount; }
        }

        public BusinessLoans BusinessLoans { get;  }
        public ConfidentialInvoiceDiscount ConfidentialInvoiceDiscount { get; }
        public SelectiveInvoiceDiscount  SelectiveInvoiceDiscount { get;  }
        public ISellerCompanyData CompanyData { get;  }
    }
}