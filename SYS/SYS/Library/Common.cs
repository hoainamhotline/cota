using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.IO;
namespace SYS
{
    public class Common
    {
        /// <summary>
        /// Đối tượng đồng bộ luồng giữa các session của server
        /// </summary>
        public static object ServerSyncObj = new object();
        /// <summary>
        /// Đối tượng đồng bộ luồng giữa các sesion của client
        /// </summary>
        public static object ClientSyncObj = new object();
        /// <summary>
        /// Đối tượng đồng bộ luồng giữa các sesion của toàn bộ hệ thống
        /// </summary>
        public static object AllSyncObj = new object();
        public static string RandomString(int size = 32, bool lowerCase = true)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string rootPath
        {
            get { return (new System.Web.UI.Page()).Server.MapPath("\\");}
        }
        /// <summary>
        /// Tạo file từ đối tượng DocumentBus
        /// </summary>
        /// <param name="data">mảng byte chứa nội dung file</param>
        /// <param name="fileName">tên file bao gồm định dạng</param>
        /// /// <param name="fileName">đường dẫn lưu file</param>
        /// <returns></returns>
        public static bool CreateFile(byte[] data, string fileName, string path)
        {
            FileStream fs = new FileStream(path + fileName, FileMode.Create);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            return true;
        }

        public static byte[] FileToBytes(string path)
        {
            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data;
            int intLength = Convert.ToInt32(f.Length);
            data = new byte[intLength];
            f.Read(data, 0, intLength);
            f.Close();
            return data;
        }
        /// <summary>
        /// Từ text tạo ra command và gọi đến sqlCmd
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable sqlText(string sql)
        {
            sql = Config.sqlConfig + sql;
            SqlCommand commd = new SqlCommand();
            commd.CommandType = CommandType.Text;
            commd.CommandText = sql;
            return sqlCmd(commd);
        }
        /// <summary>
        /// Toàn bộ các kết nối đến DB sẽ gọi đến đây 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static DataTable sqlCmd(SqlCommand cmd)
        {
            cmd.CommandText = Config.sqlConfig + cmd.CommandText;
            SqlConnection con = DBConnection.open();
            cmd.Connection = con;
            if (DBConnection.CurentTransaction != null)
            {
                cmd.Transaction = DBConnection.CurentTransaction;
            }
            SqlDataAdapter SqlDataAdapter = new SqlDataAdapter();
            DataSet dsData = new DataSet();
            SqlDataAdapter.SelectCommand = cmd;
            SqlDataAdapter.Fill(dsData, "tblResult");
            DBConnection.close(con);
            return dsData.Tables["tblResult"];
        }
        public static string MD5Encode(byte[] input)
        {
            //Declarations
            Byte[] originalBytes = input;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }
        public static string MD5Encode(string inpPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(inpPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-","");
        }
        public static bool isEmailString(string inpEmail)
        {
            if (inpEmail == null)
            {
                return false;
            }
            try
            {
                MailAddress obj = new MailAddress(inpEmail);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
        public static void updateFamilyList4Group(string tblName, long ID)
        {
            string sql = string.Format("SELECT dbo.SYS_Resources.ID as ID,dbo.SYS_Resources.parentID as parentID ,{0}.familyList as familyList, 0 as edited  FROM {0} INNER JOIN dbo.SYS_Resources ON ({0}.ID = dbo.SYS_Resources.ID)",tblName);
            tbl = Common.sqlText(sql);
            _updateFamilyList4Group( ID);
            //Update các bản ghi đã sửa;
            foreach (DataRow rw in tbl.Rows)
            {
                if (rw["edited"].ToString() == "1")
                {
                    //update ban ghi nayf
                    Common.sqlText("Update " + tblName + " SET familyList = '" + rw["familyList"].ToString() + "' WHERE ID = " + rw["ID"].ToString());
                }
            }

        }
        private static DataTable tbl;//tbl danh cho _updateFamilyList4Group 
        private static void _updateFamilyList4Group(long ID)
        {
            //tim den ban ghi co ID == ID 
            for (int i = 0; i < tbl.Rows.Count; ++i)
            {
                if (tbl.Rows[i]["ID"].ToString() == ID.ToString())
                {
                    if (tbl.Rows[i]["parentID"] == DBNull.Value)
                    {//Nếu bản ghi có ParentID == null thì sẽ set familyList = ,ID, + cho edited = 1
                        tbl.Rows[i]["familyList"] = "," + ID.ToString() + ",";
                        tbl.Rows[i]["edited"] = 1;
                    }
                    else
                    {//Nếu không thì tìm tiếp bản ghi là parent của bản ghi này 
                        for (int k = 0; k < tbl.Rows.Count ;++k )
                        {
                            if (tbl.Rows[k]["ID"].ToString() == tbl.Rows[i]["parentID"].ToString())
                            {//Sửa familyList của bản ghi này bằng ,ID + familyList của bản ghi parent  + cho edited = 1
                                tbl.Rows[i]["familyList"] = "," + ID.ToString() + tbl.Rows[k]["familyList"].ToString();
                                tbl.Rows[i]["edited"] = 1;
                                break;
                            }
                        }
                    }
                    
                    //Tìm toàn bộ các bản ghi có parent là bản ghi hiện hành và gọi đệ quy
                    for (int m = 0; m < tbl.Rows.Count; ++m)
                    {
                        if (tbl.Rows[m]["parentID"].ToString() == ID.ToString())
                        {
                            _updateFamilyList4Group(Convert.ToInt64(tbl.Rows[m]["ID"]));
                        }
                    }
                    return;
                }
            }
        }
    }
}