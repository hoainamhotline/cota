using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cota.Core.Business;
using System.Data.SqlClient;
namespace Cota.Core
{
    public class Log
    {
        public static void add(string msg)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [CGP].[dbo].[SYS_Log] ([userID]  ,[logConten] ,[createdDate])  VALUES (@userID,@logConten,@createdDate)");
            cmd.Parameters.AddWithValue("@userID",Init.UserConfig.LOGEDIN_ID);
            cmd.Parameters.AddWithValue("@logConten",msg);
            cmd.Parameters.AddWithValue("@createdDate",DateTime.Now);
            Common.sqlCmd(cmd);
        }
    }
}