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
using System.Reflection;
namespace CotaCoreWS
{
    /// Class cho service - các hàm trong class này sẽ được viết thêm tại mỗi module - nằm trong thư mục service 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public partial class CotaCore : ICotaCore
    {
        public CotaCore()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                Init.Run();//Khởi tạo 
            }
            
        }
        //COMMON_FUNCTION*******************************************************************/
        public bool COMMON_checkPermission(long RoleID, long ResourceID)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                UserRoleResourceDetailBus a = new UserRoleResourceDetailBus();

                return Cota.Core.Permission.check(RoleID, ResourceID);
            }
            
        }

    }
}
