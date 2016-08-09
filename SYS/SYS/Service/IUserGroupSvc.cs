using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SYS.Business;
using System.Data.SqlClient;
namespace COREWS
{
    public partial interface ICore
    {
        [OperationContract]
        long USERGROUP_insert(UserGroupBus obj);
        [OperationContract]
        void USERGROUP_update(UserGroupBus obj);
        [OperationContract]
        void USERGROUP_delete(long ID);
        [OperationContract]
        UserGroupBus USERGROUP_selectByID(long ID);
        [OperationContract]
        List<UserGroupBus> USERGROUP_select(long pageNum = 1, long pageSize = 99999);
        [OperationContract]
        long USERGROUP_selectGetCount();
        [OperationContract]
        List<UserGroupBus> USERGROUP_search(long pageNum = 1,long pageSize = 99999,params string[] param);
        [OperationContract]
        long USERGROUP_searchGetCount(params string[] param);
        [OperationContract]
        List<UserGroupBus> USERGROUP_getAllUserGroup(long inpParentID = -1);
        [OperationContract]
        long USERGROUP_SystemGroupID();
        [OperationContract]
        long USERGROUP_NhanVienGroupID();
        [OperationContract]
        long USERGROUP_KhachHangGroupID();
    }
}
