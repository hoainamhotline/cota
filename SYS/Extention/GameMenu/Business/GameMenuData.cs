using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameMenuLib;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Net;
using Extention.GameMenu;
using System.ServiceModel;
using System.Web.Hosting;
using System.Threading;
using Extention.DOC.Dataclass;
using Extention.DOC.Library;
namespace Extention.GameMenu.Module.Bussiness
{
    public class GameMenuData
    {
        /// <summary>
        /// nếu server dang thuc hien load data thi tra ve rong de client se load lai sau
        /// </summary>
        /// <returns></returns>
        public List<GameInfo> getGameData()
        {
            updateData();
            return GameData;
        }
        #region Data
        private static List<GameInfo> GameData = new List<GameInfo>();
        private static bool LoadingData = false;
        private static DateTime _lastUpdateData = DateTime.Now.AddMinutes(-10);
        void updateData()
        {
            //update nếu dữ liệu đã được update trước đó quá 1 phút
            if (DateTime.Now > _lastUpdateData.AddMinutes(5))
            {
                try
                {
                    loadData();
                }
                catch (Exception err)
                {
                    Log("ERROR: lỗi trong quá trình load data>> Detail: " + err.Message);
                }
            }
        }
        
        void loadData()
        {
            LoadingData = true;
            DataTable tbl = connectDBAndGetData(GameMenuConfig.databaseFile, GameMenuConfig.sqlQuery);
            //bắt đầu đọc từ bảng và đưa vào list
            List<GameInfo> GameDataTmp = new List<GameInfo>();
            string rqUrl = OperationContext.Current.RequestContext.RequestMessage.Headers.To.ToString();
            rqUrl = rqUrl.Substring(0, rqUrl.LastIndexOf('/')) + "/";
            Size iconSize = new Size(48,48);
            Bitmap SaveIcon = new Bitmap(HostingEnvironment.ApplicationPhysicalPath + "Extention/GameMenu/saveIcon.png");
            Bitmap GameIcon;
            Bitmap FinishGameIcon;
            Graphics gfx;
            foreach (DataRow row in tbl.Rows)
            {
                //"40","Star Sword","Games Offline","StarSword.exe","E:\Games Offline\Star Sword\","userData\*.*",     LolClient.exe
                // ID   Name         groupName       MainExe         LocalPath                      LocalArchivePath   processName
                // 0    1            2               3               4                              5                  6
                try
                {
                    GameInfo tmp = new GameInfo();
                    tmp.ID = row[0].ToString();
                    tmp.name = row[1].ToString();
                    tmp.runFIle = row[3].ToString();
                    tmp.groupName = row[2].ToString();
                    tmp.folderPath = row[4].ToString();
                    tmp.localArchivePath = row[5].ToString();
                    tmp.processName = row[6].ToString();
                    tmp.iconUrl = rqUrl + "DownloadIcon.aspx?id=" + tmp.ID;
                    if (File.Exists(GameMenuConfig.iconFolder + tmp.ID.ToString() + ".ico"))
                    {
                        GameIcon = (new Icon(GameMenuConfig.iconFolder + tmp.ID.ToString() + ".ico", iconSize)).ToBitmap();
                    }
                    else
                    {
                        GameIcon = (new Icon(HostingEnvironment.ApplicationPhysicalPath + "/icon.ico", iconSize)).ToBitmap();
                    }
                    //xử lý thêm biểu tưởng gama save cho những game có thể save, và thêm biểu tượng game load cho những game có thể load
                    if (tmp.localArchivePath.Trim() != "")
                    {
                        FinishGameIcon = new Bitmap(48, 48);
                        gfx = Graphics.FromImage(FinishGameIcon);
                        gfx.DrawImage(GameIcon, 0, 0, 48, 48);
                        gfx.DrawImage(SaveIcon, 34, 34, 14, 14);
                        tmp.iconImage = FinishGameIcon as Image;
                    }
                    else
                    {
                        tmp.iconImage = GameIcon as Image;
                    }
                    //add vao list
                    GameDataTmp.Add(tmp);
                }
                catch
                {
                    Log("Đọc file icon gặp lỗi filename: " + row[0] + ".ico");
                }
            }
            xuLySapXep(GameDataTmp);
            GameData = GameDataTmp;
            //ghi lại thời gian cập nhập dữ liệu
            _lastUpdateData = DateTime.Now;
            LoadingData = false;
        }
        void xuLySapXep(List<GameInfo> GameDataTmp)
        {
            foreach (GameInfo tmp in GameDataTmp)
            {
                if (tmp.groupName == "Online Games")
                {
                    tmp.groupName = "Games Online";
                }
                if (tmp.groupName != "Games Online" && tmp.groupName != "Games Offline")
                {
                    tmp.groupName = "Ứng Dụng";
                }
            }
            for (int i = 0; i < GameDataTmp.Count; ++i)
            {
                if (GameDataTmp[i].groupName == "Ứng Dụng")
                {
                    GameInfo tmp = GameDataTmp[i];
                    GameDataTmp.RemoveAt(i);
                    GameDataTmp.Insert(0, tmp);
                    break;
                }
            }
            for (int i = 0; i < GameDataTmp.Count; ++i)
            {
                if (GameDataTmp[i].groupName == "Games Offline")
                {
                    GameInfo tmp = GameDataTmp[i];
                    GameDataTmp.RemoveAt(i);
                    GameDataTmp.Insert(0, tmp);
                    break;
                }
            }
            for (int i = 0; i < GameDataTmp.Count; ++i)
            {
                if (GameDataTmp[i].groupName == "Games Online")
                {
                    GameInfo tmp = GameDataTmp[i];
                    GameDataTmp.RemoveAt(i);
                    GameDataTmp.Insert(0, tmp);
                    break;
                }
            }

        }
        public DataTable connectDBAndGetData(string dbFile, string sql)
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
        #endregion

        #region Log
        public static void Log(string msg)
        {
            msg = DateTime.Now.ToString() + ": " + msg;
            StreamWriter sr = new StreamWriter(GameMenuConfig.startupPath + "\\Extention\\GameMenu\\TestData\\Log\\" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".txt", true);
            sr.WriteLine(msg);
            sr.Close();
        }
        #endregion
    }
}