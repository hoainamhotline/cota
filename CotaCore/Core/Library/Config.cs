using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cota.Core
{
    public class Config
    {
        public static string sqlConnectString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;
            }
        }
        /// <summary>
        /// khi chạy trong chế độ debug chúng sẽ có các hàm chạy phục vụ cho debug
        /// Khi ko debug thì đặt bằng fasle
        /// </summary>
        public static bool isDebug = false;

        public static string sqlConfig = " SET DATEFORMAT dmy; ";

        public static string docFolder
        {
            get { return "\\docData\\"; }
        }
    }
}
