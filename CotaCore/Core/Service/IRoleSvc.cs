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
        List<RoleBus> ROLE_select();
        [OperationContract]
        RoleBus ROLE_selectByID(long ID);
    }
}
