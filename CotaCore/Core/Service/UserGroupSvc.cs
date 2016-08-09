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
namespace CotaCoreWS
{
    public partial class CotaCore : ICotaCore
    {
        public long USERGROUP_insert(UserGroupBus obj)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return obj.insert();
            }
        }

        public void USERGROUP_update(UserGroupBus obj)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                obj.update();
            }
        }

        public void USERGROUP_delete(long ID)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                UserGroupBus obj = (new UserGroupBus()).selectByID<UserGroupBus>(ID);
                if (default(UserGroupBus) == obj)
                {
                    throw new Exception("UserGroupID không tồn tại");
                }
                obj.delete();
            }
        }

        public UserGroupBus USERGROUP_selectByID(long ID)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserGroupBus()).selectByID<UserGroupBus>(ID);
            }
        }

        public List<UserGroupBus> USERGROUP_select(long pageNum = 1, long pageSize = 99999)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserGroupBus()).select<UserGroupBus>(pageNum, pageSize, "", null, SortOrder.Descending);
            }
        }
        public long USERGROUP_selectGetCount()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserGroupBus()).selectGetCount();
            }
        }
        public List<UserGroupBus> USERGROUP_search(long pageNum = 1, long pageSize = 99999,params string[] param)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserGroupBus()).search<UserGroupBus>(pageNum, pageSize, 1,param);
            }
        }
        public long USERGROUP_searchGetCount( params string[] param)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserGroupBus()).searchGetCount(1, param);
            }
        }
        public List<UserGroupBus> USERGROUP_getAllUserGroup(long inpParentID = -1)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new UserGroupBus()).getAllUserGroup(inpParentID);
            }
        }

        public long USERGROUP_SystemGroupID()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return Init.UserGroupConfig.System_GROUP_ID;
            }
        }

        public long USERGROUP_NhanVienGroupID()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return Init.UserGroupConfig.NhanVien_GROUP_ID;
            }
        }

        public long USERGROUP_KhachHangGroupID()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return Init.UserGroupConfig.KhachHang_GROUP_ID;
            }
        }
    }
}
