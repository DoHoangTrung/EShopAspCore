using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Common
{
    public interface IEmailService
    {
        Task<bool> SendAsyn(MailContent mailContent);
    }
}
