using Extention.DOC.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Extention.DOC.Admin
{
    //CREATE TABLE IF NOT EXISTS [tbl_HPGameMenu] ([PkgId] INTEGER,[SavePath] VARCHAR(266) NULL,[Description] TEXT NULL);
    public partial class GameSaveEditer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Tạm bỏ phần login lúc mới chuyển sang project mới
            //if (!Login.checkLogin())
            //{
            //    Response.Redirect("~/Admin/Login.aspx?rurl=GameSaveEditer.aspx");
            //}
            if (!IsPostBack)
            {
                updateData();
            }
            
            
        }
        void updateData()
        {
            GameMenuConfig.loadConfig();
            DataTable tblobj = connectDBAndGetData(GameMenuConfig.databaseFile, GameMenuConfig.sqlQuery);
            foreach (DataRow rw in tblobj.Rows)
            {
                rw["Description"] = HttpUtility.HtmlDecode(rw["Description"].ToString());
            }
            GridView1.DataSource = tblobj;
            GridView1.DataBind();
        }
        private DataTable connectDBAndGetData(string dbFile, string sql)
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase(dbFile);
                return db.GetDataTable(sql);
            }
            catch
            {
                throw new Exception("Gặp lỗi trong khi conect với db và truy vấn dữu liệu");
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            pnlEdit.Visible = true;
            lblid.Text = GridView1.Rows[e.NewEditIndex].Cells[1].Text.ToString().Trim();
            txtName.Text = GridView1.Rows[e.NewEditIndex].Cells[2].Text.ToString().Trim();
            txtGroupName.Text = GridView1.Rows[e.NewEditIndex].Cells[3].Text.ToString().Trim();
            txtMainExe.Text = GridView1.Rows[e.NewEditIndex].Cells[4].Text.ToString().Trim();
            txtLocalPath.Text = GridView1.Rows[e.NewEditIndex].Cells[5].Text.ToString().Trim();
            txtLocalArchivePath.Text = GridView1.Rows[e.NewEditIndex].Cells[6].Text.ToString().Trim();
            txtProcessName.Text = GridView1.Rows[e.NewEditIndex].Cells[7].Text.ToString().Trim();
            CKEditor1.Text = GridView1.Rows[e.NewEditIndex].Cells[8].Text.ToString().Trim();
            e.Cancel = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string sql = "UPDATE Package SET Name = \"{0}\",groupName =  \"{1}\",MainExe =  \"{2}\",LocalPath =  \"{3}\",LocalArchivePath =  \"{4}\", processName =  \"{5}\", Description = \"{6}\" WHERE ID = {7}";
            sql = string.Format(sql, 
                    txtName.Text.Trim(),
                    txtGroupName.Text.Trim(),
                    txtMainExe.Text.Trim(),
                    txtLocalPath.Text.Trim(),
                    txtLocalArchivePath.Text.Trim(),
                    txtProcessName.Text.Trim(),
                    HttpUtility.HtmlEncode(CKEditor1.Text), lblid.Text);
            
            connectDBAndGetData(GameMenuConfig.databaseFile, sql);
            Response.Write("Update thành công");
            updateData();
            pnlEdit.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Package (\"Name\",\"groupName\",\"MainExe\",\"LocalPath\",\"LocalArchivePath\",\"processName\",\"Description\") VALUES (\"" + txtName.Text.Trim() + "\",\"" + txtGroupName.Text.Trim() + "\",\"" + txtMainExe.Text.Trim() + "\",\"" + txtLocalPath.Text.Trim() + "\",\"" + txtLocalArchivePath.Text.Trim() + "\",\"" + txtProcessName.Text.Trim() + "\",\"" + HttpUtility.HtmlEncode(CKEditor1.Text) + "\")";
            connectDBAndGetData(GameMenuConfig.databaseFile, sql);
            Response.Write("Thêm mới thành công");
            updateData();
            pnlEdit.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM Package WHERE ID= " + lblid.Text;
            connectDBAndGetData(GameMenuConfig.databaseFile, sql);
            Response.Write("Xóa thành công");
            updateData();
            pnlEdit.Visible = false;
        }

        
    }
}