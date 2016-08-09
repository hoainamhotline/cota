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
        public List<RoleBus> ROLE_select()
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new RoleBus()).select<RoleBus>(1, 99999, "", "resourceTypeID", SortOrder.Descending);
            }
        }
        public RoleBus ROLE_selectByID(long ID)
        {
            lock (Cota.Core.Common.AllSyncObj)
            {
                return (new RoleBus()).selectByID<RoleBus>(ID);
            }
        }
    }
}
