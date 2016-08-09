using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Extention.DOC.Library;
namespace Extention.DOC
{
    public partial class UploadSaveGameData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string username = Request["username"];
                string gameid = Request["gameid"];
                if (username != null && gameid != null)
                {
                    string filename = gameid + "." + username + ".zip";
                    GameMenuConfig.loadConfig();
                    string path = GameMenuConfig.saveDataFolder + filename;

                    HttpPostedFile file = Request.Files[0];
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    file.SaveAs(path);
                    Response.Write("ok");
                }
                else
                {
                    Response.Write("upload error>> no username and gameid");
                }
            }
            catch
            {
                Response.Write("upload error");
            }
            Response.End();
        }
    }
}