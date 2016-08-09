using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace Extention.DOC.Library
{
    public class GameMenuConfig
    {
        public static string startupPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string databaseFile = "";//db dùng chng với gcafe pro
        public static string owndatabaseFile = "";//db riêng của chương trình
        public static string sqlQuery = "";
        public static string iconFolder = "";
        public static string saveDataFolder = "";
        public static string publicDomain = "";
        public static string hdvietacc = "";
        public static string pubacc = "";
        public static string ssphimacc = "";
        public static int TienThuongThangLOL = 0;
        public static string CSMIP = "192.168.1.200";
        public static string[] wandomaim = null;//Chứa các đường linh để ping kiểm tra tình trạng internet
        public static string LOLNhat = "";
        public static string LOLNhi = "";
        public static string LOLBa = "";
        public static string LOLBon = "";
        public static string LOLKhuyenKhich = "";
        public static string LOLGiaiDut = "";
        public static void loadConfig()
        {
            if (databaseFile == "")
            {
                try
                {
                    StreamReader sr = new StreamReader(startupPath + "Extention\\GameMenu\\config.dat");
                    databaseFile = sr.ReadLine();
                    owndatabaseFile = sr.ReadLine();
                    sqlQuery = sr.ReadLine();
                    iconFolder = sr.ReadLine();
                    saveDataFolder = sr.ReadLine();
                    publicDomain = sr.ReadLine();
                    hdvietacc = sr.ReadLine();
                    pubacc = sr.ReadLine();
                    ssphimacc = sr.ReadLine();
                    TienThuongThangLOL = Convert.ToInt32(sr.ReadLine());
                    CSMIP = sr.ReadLine();
                    wandomaim = sr.ReadLine().Split(';');
                    LOLNhat = sr.ReadLine();
                    LOLNhi = sr.ReadLine();
                    LOLBa = sr.ReadLine();
                    LOLBon = sr.ReadLine();
                    LOLKhuyenKhich = sr.ReadLine();
                    LOLGiaiDut = sr.ReadLine();
                    sr.Close(); sr.Dispose();
                }
                catch
                {
                    throw new Exception("Không đọc được cấu hình từ file config.dat");
                }
            }
        }
    }
}