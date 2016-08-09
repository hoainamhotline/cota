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
using System.Reflection;
namespace COREWS
{
    /// Class cho service - các hàm trong class này sẽ được viết thêm tại mỗi module - nằm trong thư mục service 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public partial class Core : ICore
    {
        public Core()
        {
            lock (SYS.Common.AllSyncObj)
            {
                Init.Run();//Khởi tạo 
            }
            
        }
        //COMMON_FUNCTION*******************************************************************/
        public bool COMMON_checkPermission(long RoleID, long ResourceID)
        {
            lock (SYS.Common.AllSyncObj)
            {
                UserRoleResourceDetailBus a = new UserRoleResourceDetailBus();

                return SYS.Permission.check(RoleID, ResourceID);
            }
            
        }

    }
}
