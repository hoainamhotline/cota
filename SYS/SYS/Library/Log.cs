using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS.Business;
using System.Data.SqlClient;
namespace SYS
{
    public class Log
    {
        public static void add(string msg)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [Core].[dbo].[SYS_Log] ([userID]  ,[logConten] ,[createdDate])  VALUES (@userID,@logConten,@createdDate)");
            cmd.Parameters.AddWithValue("@userID",Init.UserConfig.LOGEDIN_ID);
            cmd.Parameters.AddWithValue("@logConten",msg);
            cmd.Parameters.AddWithValue("@createdDate",DateTime.Now);
            Common.sqlCmd(cmd);
        }
    }
}