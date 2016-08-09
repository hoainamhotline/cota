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
        public List<LogBus> LOG_selectByUserID(long inpUserID, long pageNum = 1, long pageSize = 20)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new LogBus()).selectByUserID(inpUserID, pageNum, pageSize);
            }
        }

        public long LOG_selectByUserIDGetCount(long inpUserID)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new LogBus()).selectByUserIDGetCount(inpUserID);
            }
        }

        public List<LogBus> LOG_select(long pageNum = 1, long pageSize = 20)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new LogBus()).select<LogBus>(pageNum, pageSize);
            }
        }

        public long LOG_selectGetCount()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new LogBus()).selectGetCount();
            }
        }

    }
}
