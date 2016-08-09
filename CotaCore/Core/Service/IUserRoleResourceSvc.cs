using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cota.Core.Business;
using System.Data.SqlClient;
namespace COTACOREWS
{
    public partial interface ICotaCore
    {
        [OperationContract]
        long USERROLERESOURCE_insert(UserRoleResourceBus obj);
        [OperationContract]
        void USERROLERESOURCE_update(UserRoleResourceBus obj);
        [OperationContract]
        void USERROLERESOURCE_delete(long ID);
        [OperationContract]
        UserRoleResourceBus USERROLERESOURCE_selectByID(long ID);
        [OperationContract]
        List<UserRoleResourceDetailBus> USERROLERESOURCE_select(long pageNum = 1, long pageSize = 99999);
        [OperationContract]
        long USERROLERESOURCE_selectGetCount();
        [OperationContract]
        List<UserRoleResourceDetailBus> USERROLERESOURCE_selectByUserIDRoleIDResID(long UserID = -1, long RoleID = -1, long RessourceID = -1, long pageNum = 1, long pageSize = 20);
        [OperationContract]
        long USERROLERESOURCE_selectByUserIDRoleIDResIDGetCount(long inpUserID = -1, long inpRoleID = -1, long inpResourceID = -1);
    }
}
