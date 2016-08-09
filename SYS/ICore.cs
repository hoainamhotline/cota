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
    /// <summary>
    /// Interface cho service - các hàm trong Interface này sẽ được viết thêm tại mỗi module - nằm trong thư mục service 
    /// </summary>
    [ServiceContract]
    public partial interface ICore
    {
        //COMMON_FUNTION**********************************************************************/
        [OperationContract]
        bool COMMON_checkPermission(long RoleID, long ResourceID);
    }
}
