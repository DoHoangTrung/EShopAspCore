using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class CartUpdateRequest
    {
        public List<CartUpdateItem> Items { get; set; }
    }
}
