using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Extention.DOC.Library;
namespace Extention.DOC
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GameMenuConfig.loadConfig();
            if (Request["id"] != null)
            {
                string sql = GameMenuConfig.sqlQuery + " AND ID = " +Request["id"];
                SQLiteDatabase db = new SQLiteDatabase(GameMenuConfig.databaseFile);
                DataTable tblobj = db.GetDataTable(sql);
                if (tblobj.Rows.Count > 0)
                {
                    name.InnerHtml = "Giới Thiệu Game: " + tblobj.Rows[0]["Name"].ToString();
                    if (tblobj.Rows[0]["Description"].ToString().Trim() != "")
                    {
                        Description.InnerHtml = HttpUtility.HtmlDecode(tblobj.Rows[0]["Description"].ToString());
                    }
                    else
                    {
                        //để mặc định
                    }
                    comment.Attributes["data-href"] = "http://" + GameMenuConfig.publicDomain + "/Default.aspx?id=" + Request["id"];
                }
                else
                {
                    form1.Visible = false;
                }
            }
            else
            {
                form1.Visible = false;
            }

        }
    }
}