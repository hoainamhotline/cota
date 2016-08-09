using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SYS
{
    class DBConnection
    {
        
        public static SqlConnection open()
        {
            try
            {
                if (_curentConnect != null)
                {
                    return _curentConnect;
                }
                else
                {
                    SqlConnection con = new SqlConnection(Config.sqlConnectString);
                    con.Open();
                    return con;
                }
            }
            catch
            {
                throw new Exception("Lỗi không thể kết nối với CSDL!");
            }
        }
        public static void close(SqlConnection con)
        {
            if (_curentConnect == null)
            {
                con.Close();
            }
        }
        //xu lys transaction
        private static SqlConnection _curentConnect = null;
        public static SqlTransaction CurentTransaction = null;
        public static void startNewTransaction()
        {
            if (CurentTransaction == null)
            {
                _curentConnect = new SqlConnection(Config.sqlConnectString);
                _curentConnect.Open();
                CurentTransaction = _curentConnect.BeginTransaction("default");
            }
            else
            {
                _curentConnect = null;
                CurentTransaction = null;
                throw new Exception("Lỗi DB transaction đã tồn tại!");

            }
        }
        public static void commitTransaction()
        {
            CurentTransaction.Commit();
            CurentTransaction.Dispose();
            _curentConnect.Close();
            _curentConnect = null;
            CurentTransaction = null;
        }
        public static void rollbackTransaction()
        {
            CurentTransaction.Rollback();
            CurentTransaction.Dispose();
            _curentConnect.Close();
            _curentConnect = null;
            CurentTransaction = null;
        }
    }
}
