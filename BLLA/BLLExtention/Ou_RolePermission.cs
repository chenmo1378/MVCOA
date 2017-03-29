using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLLA
{
    public partial class Ou_RolePermission:IBLL.IOu_RolePermissionBLL
    {
        #region 1.0 根据角色 查询 其权限集合 +List<MODEL.Ou_Permission> GetPermissionByRoleId(int roleId)
        /// <summary>
        /// 根据角色 查询 其权限集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<MODEL.Ou_Permission> GetPermissionByRoleId(int roleId)
        {
            //查询出 角色id 和参数相等，并且 排除掉 权限1
            return this.GetListBy(rp => rp.rpRId == roleId && rp.rpPId != 1).Select(rp => rp.GetPermissionPart()).ToList();
        } 
        #endregion
    }
}
