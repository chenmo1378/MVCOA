using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public partial class Ou_Department
    {
        public Ou_Department ToPOCO()
        {
            var o = new Ou_Department()
            {
                depId = this.depId,
                depName = this.depName,
                depPid = this.depPid,
                depAddTime = this.depAddTime,
                depIsDel = this.depIsDel,
                depRemark = this.depRemark
                //Ou_DepartmentParent = this.Ou_DepartmentParent==null ? null : this.Ou_DepartmentParent
            };
            o.Ou_DepartmentParent = this.depId != 0 ? this.Ou_DepartmentParent.ToPOCO() : null;
            return o;
        }
    }
}
