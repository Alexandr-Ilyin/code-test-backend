using SlothEnterprise.ProductApplication.Contracts;

namespace SlothEnterprise.ProductApplication.Implementation.Handlers
{
    public interface ISellerApplicationHandler
    {
        int Handle(SellerApplication application);
        bool CanHandle(SellerApplication application);
    }
}