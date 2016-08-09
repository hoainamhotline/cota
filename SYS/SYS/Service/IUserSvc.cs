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
        //USER_FUNCTION**************************************************************************/
        #region USER_FUNCTION
        [OperationContract]
        UserBus USER_login(string inpAccount, string inpPassword);
        [OperationContract]
        long USER_insert(UserBus obj);
        [OperationContract]
        void USER_update(UserBus obj, string oldPass = "");
        [OperationContract]
        void USER_delete(long ID);
        [OperationContract]
        UserBus USER_selectByID(long ID);
        [OperationContract]
        List<UserBus> USER_select(long pageNum = 1, long pageSize = 99999);
        [OperationContract]
        long USER_selectGetCount();
        [OperationContract]
        List<UserBus> USER_search(long inpParentID , long pageNum, long pageSize, string truong, string data);
        [OperationContract]
        long USER_searchGetCount(long inpParentID, string truong, string data);
        [OperationContract]
        List<UserBus> USER_getAllUser(long inpParentID = -1, long pageNum = 1, long pageSize = 20);
        [OperationContract]
        long USER_getAllUserGetCount(long inpParentID = -1);
        [OperationContract]
        List<UserBus> USER_getAllUserInAllLevel(long pageNum = 1, long pageSize = 20);
        [OperationContract]
        long USER_getAllUserInAllLevelGetCount();
        //===============================================
        [OperationContract]
        UserBus USER_getLogedInUser();
        [OperationContract]
        void USER_logout();
        [OperationContract]
        SYS.Init.UserConfigClass USER_Config();
        [OperationContract]
        bool isLogin();
        #endregion
    }
}
