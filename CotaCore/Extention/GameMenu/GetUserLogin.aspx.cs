using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
namespace Extention.DOC
{
    public partial class GetUserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Write(ComputerName2Account(Request["c"].ToString()));
                Response.End();
                return;
            }
            catch { }
            Response.Clear();
            Response.End();
        }
        private string ComputerName2Account(string ComputerName)
        {
            //ComputerName = "MAY-25";
            string sql = "select systemlogtb.Status, systemlogtb.UserID, usertb.UserName,systemlogtb.SystemLogId  from systemlogtb left join usertb on (systemlogtb.userID = usertb.userID)  where systemlogtb.MachineName = \"{0}\" order by systemlogtb.SystemLogId DESC limit 1;";
            string AccountID = "";
            //get userID
            try
            {
                DataTable tmp = MySQLDatabase.execute(string.Format(sql, ComputerName));
                string tmpAcName = tmp.Rows[0][2].ToString().Trim();
                if (!(tmpAcName.Contains("MAY-") || tmpAcName == "" || tmpAcName == "ADMIN" || tmpAcName == "Quản lý" || tmpAcName == null))
                {
                    AccountID = tmp.Rows[0][1].ToString().Trim();
                }
                checkHackLog(tmp);
            }
            catch { }
            return AccountID;
        }
        private void checkHackLog(DataTable tblLog)
        {
            //1 la sansang
            //2 la mat ket noi
            //3 la dang su dung
            //4 la canh bao
            if (tblLog.Rows[0][0] == "2" || tblLog.Rows[0][0] == "4")
            {
                //Mat ket noi hoac canh bao ma van gui log ve chung to la h

            }
        }
    }
}