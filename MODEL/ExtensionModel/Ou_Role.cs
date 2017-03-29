using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public partial class Ou_Role
    {
        public Ou_Role ToPOCO()
        {
            Ou_Role poco = new Ou_Role()
            {
                rId=this.rId,
                rName=this.rName,
                rDepId=this.rDepId,
                rRemark=this.rRemark,
                rIsShow=this.rIsShow,
                rIsDel=this.rIsDel,
                rAddTime=this.rAddTime,
                Ou_Department=this.Ou_Department.ToPOCO()
            };
            return poco;
        }
    }
}
