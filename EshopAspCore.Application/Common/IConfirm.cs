using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Common
{
    public interface IConfirm
    {
        bool ConfirmOrder(string key, string token);
    }
}
