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
    public partial class DownloadIcon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string iconName = Request["id"] + ".ico";
            string path = GameMenuConfig.iconFolder + iconName;
            if (!File.Exists(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + "icon.ico";
            }
            Response.Buffer = false; //transmitfile self buffers
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/dat";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + iconName);
            Response.TransmitFile(path); //transmitfile keeps entire file from loading into memory
            Response.End();
        }
    }
}