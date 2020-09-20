using System;

namespace SlothEnterprise.ProductApplication.Applications
{
    public class SellerCompanyData 
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string DirectorName { get; set; }
        public DateTime Founded { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Number)}: {Number}, {nameof(DirectorName)}: {DirectorName}, {nameof(Founded)}: {Founded}";
        }
    }
}