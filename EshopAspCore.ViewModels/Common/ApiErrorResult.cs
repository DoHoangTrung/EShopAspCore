﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Common
{
    public class ApiErrorResult<T>: ApiResult<T>
    {
        public List<string> ValidationErrors { get; set; }
        
        public ApiErrorResult()
        {
            IsSuccessed = false;
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult(List<string> validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}