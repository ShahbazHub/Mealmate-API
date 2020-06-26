using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class RoleAssignListViewModel
    {
        public int AppRoleId { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }
}
