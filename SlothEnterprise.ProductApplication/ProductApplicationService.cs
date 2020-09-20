using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Internals;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ISellerApplicationHandler[] handlers;

        public ProductApplicationService(ISellerApplicationHandler[] handlers)
        {
            this.handlers = handlers;
        }

        public int SubmitApplicationFor(SellerApplication application)
        {
            foreach (var applicationHandler in handlers)
            {
                if (applicationHandler.CanHandle(application))
                    applicationHandler.Handle(application);
            }
            throw new ApplicationException($"Application is not supported:{application}");
        }
    }
}
