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
