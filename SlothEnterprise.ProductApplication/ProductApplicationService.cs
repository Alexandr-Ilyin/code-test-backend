using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Contracts;
using SlothEnterprise.ProductApplication.Implementation;
using SlothEnterprise.ProductApplication.Implementation.Handlers;

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
                    return applicationHandler.Handle(application);
            }
            throw new ApplicationException($"Application is not supported:{application}");
        }
    }
}
