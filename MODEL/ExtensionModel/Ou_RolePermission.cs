using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public partial class Ou_RolePermission
    {
        #region 1.0 获取角色权限里的 权限部分 +Ou_Permission GetPermissionPart()
        /// <summary>
        /// 获取角色权限里的 权限部分
        /// </summary>
        /// <returns></returns>
        public Ou_Permission GetPermissionPart()
        {
            return new Ou_Permission()
            {
                pid = this.Ou_Permission.pid,
                pParent = this.Ou_Permission.pParent,
                pName = this.Ou_Permission.pName,
                pAreaName = this.Ou_Permission.pAreaName,
                pControllerName = this.Ou_Permission.pControllerName,
                pActionName = this.Ou_Permission.pActionName,
                pFormMethod = this.Ou_Permission.pFormMethod,
                pOperationType = this.Ou_Permission.pOperationType,
                pIco = this.Ou_Permission.pIco,
                pOrder = this.Ou_Permission.pOrder,
                pIsLink = this.Ou_Permission.pIsLink,
                pLinkUrl = this.Ou_Permission.pLinkUrl,
                pIsShow = this.Ou_Permission.pIsShow,
                pRemark = this.Ou_Permission.pRemark,
                pIsDel = this.Ou_Permission.pIsDel,
                pAddTime = this.Ou_Permission.pAddTime
            };
        } 
        #endregion
    }
}
