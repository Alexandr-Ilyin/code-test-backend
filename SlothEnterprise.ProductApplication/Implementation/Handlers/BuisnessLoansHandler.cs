using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Contracts;

namespace SlothEnterprise.ProductApplication.Implementation.Handlers
{
    public class BuisnessLoansHandler : ISellerApplicationHandler
    {
        private readonly IBusinessLoansService _businessLoansService;

        public BuisnessLoansHandler(IBusinessLoansService businessLoansService)
        {
            this._businessLoansService = businessLoansService;
        }

        public int Handle(SellerApplication application)
        {
            var loans = application.BusinessLoans;
            var result = _businessLoansService.SubmitApplicationFor(new CompanyDataRequest
            {
                CompanyFounded = application.CompanyData.Founded,
                CompanyNumber = application.CompanyData.Number,
                CompanyName = application.CompanyData.Name,
                DirectorName = application.CompanyData.DirectorName
            }, new LoansRequest
            {
                InterestRatePerAnnum = loans.InterestRatePerAnnum,
                LoanAmount = loans.LoanAmount
            });
            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }

        public bool CanHandle(SellerApplication application)
        {
            return application.BusinessLoans != null;
        }
    }
}