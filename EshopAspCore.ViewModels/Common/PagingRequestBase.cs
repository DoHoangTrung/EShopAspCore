﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Common
{
    public class PagingRequestBase :RequestBase
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
