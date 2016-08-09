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
    public partial class DownloadSaveGameData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request["username"];
            string gameid = Request["gameid"];
            string filename = gameid + "." + username + ".zip";
            GameMenuConfig.loadConfig();
            string path = GameMenuConfig.saveDataFolder + filename;
            if (File.Exists(path))
            {
                Response.Buffer = false; //transmitfile self buffers
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/dat";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(path); //transmitfile keeps entire file from loading into memory
                Response.End();
            }
        }
    }
}