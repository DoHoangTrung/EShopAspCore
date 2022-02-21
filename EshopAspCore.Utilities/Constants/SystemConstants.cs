using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "EshopAspCoreDatabase";
        public const string BaseApiUrlString = "BaseApiUrl";
        public const string NA = "N/A";
        public const string CartSession = "CartSession";

        public const int NumberOfFeaturedProducts = 4;
        public const int NumberOfLatestProducts = 6;

        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string SuccessMessage = "SuccessMsg";
            public const string ErrorMessage = "ErrorMsg";
            public const string HttpClientWithSSLUntrusted = "HttpClientWithSSLUntrusted";

        }

        public static class SelectionSortOrder
        {
            public const string PriceLowestFirst = "Price lowest first";
            public const string ProductNameAZ = " Product name A - Z";
            public const string ProductNameZA = "Product name Z - A";
            public const string ProductStocke = "Product Stoke";
        }

        public static class AppRole
        {
            public const string User = "user";
            public const string Admin = "admin";
        }
    }
}
