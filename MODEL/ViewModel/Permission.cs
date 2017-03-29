using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MODEL.ViewModel
{
    public class Permission
    {
        [DisplayName("ID")]
        public int pid { get; set; }

        [DisplayName("父级")]
        public int pParent { get; set; }

        [DisplayName("权限名")]
        [Required]
        public string pName { get; set; }

        [DisplayName("区域名")]
        [Required]
        public string pAreaName { get; set; }

        [DisplayName("控制器名")]
        public string pControllerName { get; set; }

        [DisplayName("方法名")]
        public string pActionName { get; set; }

        [DisplayName("请求方式")]
        public short pFormMethod { get; set; }

        [DisplayName("操作方式")]
        public short pOperationType { get; set; }

        [DisplayName("序号")]
        public int pOrder { get; set; }

        [DisplayName("是否在菜单显示")]
        public bool pIsShow { get; set; }

        [DisplayName("备注")]
        [DataType(DataType.MultilineText)]
        public string pRemark { get; set; }
    }
}
