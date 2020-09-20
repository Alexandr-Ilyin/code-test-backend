namespace SlothEnterprise.ProductApplication.Contracts
{
    public class SellerApplication
    {
        public SellerApplication(BusinessLoans businessLoans, SellerCompanyData companyData)
        {
            BusinessLoans = businessLoans;
            CompanyData = companyData;
        }

        public SellerApplication(ConfidentialInvoiceDiscount confidentialInvoiceDiscount, SellerCompanyData companyData)
        {
            ConfidentialInvoiceDiscount = confidentialInvoiceDiscount;
            CompanyData = companyData;
        }

        public SellerApplication(SelectiveInvoiceDiscount selectiveInvoiceDiscount, SellerCompanyData companyData)
        {
            SelectiveInvoiceDiscount = selectiveInvoiceDiscount;
            CompanyData = companyData;
        }

        public IProduct Product
        {
            get { return (IProduct)BusinessLoans ?? (IProduct)ConfidentialInvoiceDiscount ?? SelectiveInvoiceDiscount; }
        }

        public BusinessLoans BusinessLoans { get; }
        public ConfidentialInvoiceDiscount ConfidentialInvoiceDiscount { get; }
        public SelectiveInvoiceDiscount SelectiveInvoiceDiscount { get; }
        public SellerCompanyData CompanyData { get; }

        public override string ToString()
        {
            return $"{nameof(Product)}: {Product}, {nameof(CompanyData)}: {CompanyData}";
        }
    }
}