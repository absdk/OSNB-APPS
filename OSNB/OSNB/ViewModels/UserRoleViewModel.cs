using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<AssignRoleModel> AssignRoles { get; set; }
    }
}