using EshopAspCore.Application.Utilities.Confirm;
using EshopAspCore.Data.Enum;
using System;

namespace EshopAspCore.Application.Common
{
    public class Confirm : IConfirm
    {
        public bool ConfirmOrder(string key, string token)
        {
            if (key.Length < 16)
                return false;

            var state = AesOperation.DecryptString(key, token);
            if(state == Enum.GetName(typeof(OrderStatus), OrderStatus.Confirmed))
            {
                return true;
            }

            return false;
        }

    }
}
