using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.System.Roles
{
    public class RoleAssignRequest
    {
        public Guid UserId { get; set; }
        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
    }
}
