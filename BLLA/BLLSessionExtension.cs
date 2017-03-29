
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBLL;

namespace BLLA
{
	public partial class BLLSession:IBLL.IBLLSession
    {
		#region 01 业务接口 IBill_LeaveDAL
		IBill_LeaveBLL iBill_LeaveBLL;
		public IBill_LeaveBLL IBill_LeaveBLL
		{
			get
			{
				if(iBill_LeaveBLL==null)
					iBill_LeaveBLL= new Bill_Leave();
				return iBill_LeaveBLL;
			}
			set
			{
				iBill_LeaveBLL= value;
			}
		}
		#endregion

		#region 02 业务接口 IOu_DepartmentDAL
		IOu_DepartmentBLL iOu_DepartmentBLL;
		public IOu_DepartmentBLL IOu_DepartmentBLL
		{
			get
			{
				if(iOu_DepartmentBLL==null)
					iOu_DepartmentBLL= new Ou_Department();
				return iOu_DepartmentBLL;
			}
			set
			{
				iOu_DepartmentBLL= value;
			}
		}
		#endregion

		#region 03 业务接口 IOu_PermissionDAL
		IOu_PermissionBLL iOu_PermissionBLL;
		public IOu_PermissionBLL IOu_PermissionBLL
		{
			get
			{
				if(iOu_PermissionBLL==null)
					iOu_PermissionBLL= new Ou_Permission();
				return iOu_PermissionBLL;
			}
			set
			{
				iOu_PermissionBLL= value;
			}
		}
		#endregion

		#region 04 业务接口 IOu_RoleDAL
		IOu_RoleBLL iOu_RoleBLL;
		public IOu_RoleBLL IOu_RoleBLL
		{
			get
			{
				if(iOu_RoleBLL==null)
					iOu_RoleBLL= new Ou_Role();
				return iOu_RoleBLL;
			}
			set
			{
				iOu_RoleBLL= value;
			}
		}
		#endregion

		#region 05 业务接口 IOu_RolePermissionDAL
		IOu_RolePermissionBLL iOu_RolePermissionBLL;
		public IOu_RolePermissionBLL IOu_RolePermissionBLL
		{
			get
			{
				if(iOu_RolePermissionBLL==null)
					iOu_RolePermissionBLL= new Ou_RolePermission();
				return iOu_RolePermissionBLL;
			}
			set
			{
				iOu_RolePermissionBLL= value;
			}
		}
		#endregion

		#region 06 业务接口 IOu_UserInfoDAL
		IOu_UserInfoBLL iOu_UserInfoBLL;
		public IOu_UserInfoBLL IOu_UserInfoBLL
		{
			get
			{
				if(iOu_UserInfoBLL==null)
					iOu_UserInfoBLL= new Ou_UserInfo();
				return iOu_UserInfoBLL;
			}
			set
			{
				iOu_UserInfoBLL= value;
			}
		}
		#endregion

		#region 07 业务接口 IOu_UserRoleDAL
		IOu_UserRoleBLL iOu_UserRoleBLL;
		public IOu_UserRoleBLL IOu_UserRoleBLL
		{
			get
			{
				if(iOu_UserRoleBLL==null)
					iOu_UserRoleBLL= new Ou_UserRole();
				return iOu_UserRoleBLL;
			}
			set
			{
				iOu_UserRoleBLL= value;
			}
		}
		#endregion

		#region 08 业务接口 IOu_UserVipPermissionDAL
		IOu_UserVipPermissionBLL iOu_UserVipPermissionBLL;
		public IOu_UserVipPermissionBLL IOu_UserVipPermissionBLL
		{
			get
			{
				if(iOu_UserVipPermissionBLL==null)
					iOu_UserVipPermissionBLL= new Ou_UserVipPermission();
				return iOu_UserVipPermissionBLL;
			}
			set
			{
				iOu_UserVipPermissionBLL= value;
			}
		}
		#endregion

		#region 09 业务接口 IWF_AutoTransactNodeDAL
		IWF_AutoTransactNodeBLL iWF_AutoTransactNodeBLL;
		public IWF_AutoTransactNodeBLL IWF_AutoTransactNodeBLL
		{
			get
			{
				if(iWF_AutoTransactNodeBLL==null)
					iWF_AutoTransactNodeBLL= new WF_AutoTransactNode();
				return iWF_AutoTransactNodeBLL;
			}
			set
			{
				iWF_AutoTransactNodeBLL= value;
			}
		}
		#endregion

		#region 10 业务接口 IWF_BillFlowNodeDAL
		IWF_BillFlowNodeBLL iWF_BillFlowNodeBLL;
		public IWF_BillFlowNodeBLL IWF_BillFlowNodeBLL
		{
			get
			{
				if(iWF_BillFlowNodeBLL==null)
					iWF_BillFlowNodeBLL= new WF_BillFlowNode();
				return iWF_BillFlowNodeBLL;
			}
			set
			{
				iWF_BillFlowNodeBLL= value;
			}
		}
		#endregion

		#region 11 业务接口 IWF_BillFlowNodeRemarkDAL
		IWF_BillFlowNodeRemarkBLL iWF_BillFlowNodeRemarkBLL;
		public IWF_BillFlowNodeRemarkBLL IWF_BillFlowNodeRemarkBLL
		{
			get
			{
				if(iWF_BillFlowNodeRemarkBLL==null)
					iWF_BillFlowNodeRemarkBLL= new WF_BillFlowNodeRemark();
				return iWF_BillFlowNodeRemarkBLL;
			}
			set
			{
				iWF_BillFlowNodeRemarkBLL= value;
			}
		}
		#endregion

		#region 12 业务接口 IWF_BillStateDAL
		IWF_BillStateBLL iWF_BillStateBLL;
		public IWF_BillStateBLL IWF_BillStateBLL
		{
			get
			{
				if(iWF_BillStateBLL==null)
					iWF_BillStateBLL= new WF_BillState();
				return iWF_BillStateBLL;
			}
			set
			{
				iWF_BillStateBLL= value;
			}
		}
		#endregion

		#region 13 业务接口 IWF_NodeDAL
		IWF_NodeBLL iWF_NodeBLL;
		public IWF_NodeBLL IWF_NodeBLL
		{
			get
			{
				if(iWF_NodeBLL==null)
					iWF_NodeBLL= new WF_Node();
				return iWF_NodeBLL;
			}
			set
			{
				iWF_NodeBLL= value;
			}
		}
		#endregion

		#region 14 业务接口 IWF_NodeStateDAL
		IWF_NodeStateBLL iWF_NodeStateBLL;
		public IWF_NodeStateBLL IWF_NodeStateBLL
		{
			get
			{
				if(iWF_NodeStateBLL==null)
					iWF_NodeStateBLL= new WF_NodeState();
				return iWF_NodeStateBLL;
			}
			set
			{
				iWF_NodeStateBLL= value;
			}
		}
		#endregion

		#region 15 业务接口 IWF_WorkFlowDAL
		IWF_WorkFlowBLL iWF_WorkFlowBLL;
		public IWF_WorkFlowBLL IWF_WorkFlowBLL
		{
			get
			{
				if(iWF_WorkFlowBLL==null)
					iWF_WorkFlowBLL= new WF_WorkFlow();
				return iWF_WorkFlowBLL;
			}
			set
			{
				iWF_WorkFlowBLL= value;
			}
		}
		#endregion

		#region 16 业务接口 IWF_WorkFlowNodeDAL
		IWF_WorkFlowNodeBLL iWF_WorkFlowNodeBLL;
		public IWF_WorkFlowNodeBLL IWF_WorkFlowNodeBLL
		{
			get
			{
				if(iWF_WorkFlowNodeBLL==null)
					iWF_WorkFlowNodeBLL= new WF_WorkFlowNode();
				return iWF_WorkFlowNodeBLL;
			}
			set
			{
				iWF_WorkFlowNodeBLL= value;
			}
		}
		#endregion

    }

}