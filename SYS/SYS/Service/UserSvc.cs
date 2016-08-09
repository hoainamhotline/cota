using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using SYS.Business;
using SYS;
using System.ServiceModel.Activation;
namespace COREWS
{
    public partial class Core : ICore
    {

        public UserBus USER_login(string inpAccount, string inpPassword)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return UserBus.login(inpAccount, inpPassword);
            }
            
        }
        public UserBus USER_getLogedInUser()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).selectByID<UserBus>(Init.UserConfig.LOGEDIN_ID);
            }
        }
        public void USER_logout()
        {
            lock (SYS.Common.AllSyncObj)
            {
                Init.UserConfig.LOGEDIN_ID = 0;
            }
        }
        public long USER_insert(UserBus obj)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return obj.insert();
            }
        }
        public void USER_update(UserBus obj, string oldPass = "")
        {
            lock (SYS.Common.AllSyncObj)
            {
                obj.update(oldPass);
            }
        }
        public void USER_delete(long ID)
        {
            lock (SYS.Common.AllSyncObj)
            {
                UserBus obj = (new UserBus()).selectByID<UserBus>(ID);
                if (default(UserBus) == obj)
                {
                    throw new Exception("UserID không tồn tại");
                }
                obj.delete();
            }
        }
        public UserBus USER_selectByID(long ID)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).selectByID<UserBus>(ID);
            }
        }
        public List<UserBus> USER_select(long pageNum = 1, long pageSize = 99999)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).select<UserBus>(pageNum, pageSize, "", null, SortOrder.Descending);
            }
        }
        public long USER_selectGetCount()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).selectGetCount();
            }
        }
        public List<UserBus> USER_search(long inpParentID, long pageNum, long pageSize, string truong, string data)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).search(inpParentID,pageNum, pageSize,truong, data,2);
            }
        }
        public long USER_searchGetCount(long inpParentID, string truong, string data)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).searchGetCount(inpParentID,truong, data,2);
            }
        }
        public List<UserBus> USER_getAllUser(long inpParentID = -1, long pageNum = 1, long pageSize = 20)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).getAllUser(inpParentID, pageNum, pageSize);
            }
        }
        public long USER_getAllUserGetCount(long inpParentID = -1)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new UserBus()).getAllUserGetCount(inpParentID);
            }
        }

        public List<UserBus> USER_getAllUserInAllLevel(long pageNum = 1, long pageSize = 20)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return new UserBus().getAllUserInAllLevel(pageNum, pageSize);
            }
        }

        public long USER_getAllUserInAllLevelGetCount()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return new UserBus().getAllUserInAllLevelGetCount();
            }
        }
        public Init.UserConfigClass USER_Config()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return Init.UserConfig;
            }
        }


        public bool isLogin()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return UserBus.isLogin();
            }
        }
    }
}
