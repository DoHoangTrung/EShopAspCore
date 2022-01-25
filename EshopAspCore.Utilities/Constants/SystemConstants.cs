﻿using System;
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

        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string SuccessMessage = "SuccessMsg";
        }

        public class ProductSettings
        {
            public const int NumberOfFeaturedProducts = 4;

            public const int NumberOfLatestProducts = 6;
        }
    }
}
