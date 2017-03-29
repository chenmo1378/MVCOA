using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IOu_RolePermissionBLL
    {
        List<MODEL.Ou_Permission> GetPermissionByRoleId(int roleId);
    }
}
