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
        List<LogBus> LOG_selectByUserID(long inpUserID, long pageNum = 1, long pageSize = 20);
        [OperationContract]
        long LOG_selectByUserIDGetCount(long inpUserID);
        [OperationContract]
        List<LogBus> LOG_select(long pageNum = 1, long pageSize = 20);
        [OperationContract]
        long LOG_selectGetCount();
    }
}
