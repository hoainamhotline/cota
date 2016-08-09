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
        List<RoleBus> ROLE_select();
        [OperationContract]
        RoleBus ROLE_selectByID(long ID);
    }
}
