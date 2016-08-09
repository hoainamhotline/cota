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
        public List<RoleBus> ROLE_select()
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new RoleBus()).select<RoleBus>(1, 99999, "", "resourceTypeID", SortOrder.Descending);
            }
        }
        public RoleBus ROLE_selectByID(long ID)
        {
            lock (SYS.Common.AllSyncObj)
            {
                return (new RoleBus()).selectByID<RoleBus>(ID);
            }
        }
    }
}
