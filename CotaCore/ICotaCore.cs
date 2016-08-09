using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cota.Core.Business;
using System.Data.SqlClient;
namespace CotaCoreWS
{
    /// <summary>
    /// Interface cho service - các hàm trong Interface này sẽ được viết thêm tại mỗi module - nằm trong thư mục service 
    /// </summary>
    [ServiceContract]
    public partial interface ICotaCore
    {
        //COMMON_FUNTION**********************************************************************/
        [OperationContract]
        bool COMMON_checkPermission(long RoleID, long ResourceID);
    }
}
