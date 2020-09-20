using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Internals
{
    public interface ISellerApplicationHandler
    {
        int Handle(SellerApplication application);
        bool CanHandle(SellerApplication application);
    }
}