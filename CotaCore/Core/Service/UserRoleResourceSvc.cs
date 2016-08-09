using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using Cota.Core.Business;
using Cota.Core;
using System.ServiceModel.Activation;
namespace COTACOREWS
{
    public partial class CotaCore : ICotaCore
    {
        public long USERROLERESOURCE_insert(UserRoleResourceBus obj)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return obj.insert();
            }
        }
        public void USERROLERESOURCE_update(UserRoleResourceBus obj)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                obj.update();
            }
        }
        public void USERROLERESOURCE_delete(long ID)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                UserRoleResourceBus obj = (new UserRoleResourceBus()).selectByID<UserRoleResourceBus>(ID);
                if (default(UserRoleResourceBus) == obj)
                {
                    throw new Exception("UserID không tồn tại");
                }
                obj.delete();
            }
        }
        public UserRoleResourceBus USERROLERESOURCE_selectByID(long ID)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserRoleResourceBus()).selectByID<UserRoleResourceBus>(ID);
            }
        }
        public List<UserRoleResourceDetailBus> USERROLERESOURCE_select(long pageNum = 1, long pageSize = 99999)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserRoleResourceDetailBus()).select<UserRoleResourceDetailBus>(pageNum, pageSize, "", null, SortOrder.Descending);
            }
        }
        public long USERROLERESOURCE_selectGetCount()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserRoleResourceDetailBus()).selectGetCount();
            }
        }
        public List<UserRoleResourceDetailBus> USERROLERESOURCE_selectByUserIDRoleIDResID(long UserID = -1, long RoleID = -1, long RessourceID = -1, long pageNum = 1, long pageSize = 20)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserRoleResourceBus()).selectByUserIDRoleIDResID(UserID, RoleID, RessourceID, pageNum, pageSize);
            }
        }
        public long USERROLERESOURCE_selectByUserIDRoleIDResIDGetCount(long inpUserID = -1, long inpRoleID = -1, long inpResourceID = -1)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserRoleResourceBus()).selectByUserIDRoleIDResIDGetCount(inpUserID, inpRoleID, inpResourceID);
            }
        }
    }
}
