//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill_Leave
    {
        public int Leave { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> BillStateID { get; set; }
        public Nullable<int> WorkFlowID { get; set; }
    
        public virtual WF_BillState WF_BillState { get; set; }
        public virtual WF_WorkFlow WF_WorkFlow { get; set; }
    }
}
