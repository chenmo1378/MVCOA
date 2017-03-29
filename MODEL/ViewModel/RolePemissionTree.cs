using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL.ViewModel
{
    /// <summary>
    /// 角色权限树 视图实体
    /// </summary>
    public class RolePemissionTree
    {
        /// <summary>
        /// 某角色的权限
        /// </summary>
        public List<MODEL.Ou_Permission> UserPer { get; set; }
        /// <summary>
        /// 系统中所有权限
        /// </summary>
        public List<MODEL.Ou_Permission> AllPer { get; set; }
        /// <summary>
        /// 所有父权限
        /// </summary>
        public List<MODEL.Ou_Permission> AllParentPer { get; set; }
    }
}
