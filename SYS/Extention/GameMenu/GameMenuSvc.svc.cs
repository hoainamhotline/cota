using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GameMenuLib;
using System.Data;
using System.Collections;
using System.IO;
using Extention.DOC.Dataclass;
using Extention.DOC.Library;
using System.ServiceModel.Activation;
using Extention.GameMenu.Module;
namespace Extention.DOC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GameMenuSvc" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GameMenuSvc.svc or GameMenuSvc.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class GameMenuSvc : IGameMenuSvc
    {
        public GameMenuSvc()
        {
            GameMenuConfig.loadConfig();
        }
        public List<GameInfo> GetMenuData()
        {
            return (new Extention.GameMenu.Module.Bussiness.GameMenuData()).getGameData();
        }

        public string GetNotifi(string computerName)
        {
            //chay thường xuyên CongT();
            clearUpData();
            CongT();
            string Account = ComputerName2Account(computerName);
            string sql = "SELECT * FROM Notifi WHERE (Account='{0}' OR Account='ALL') AND Status = 0  ORDER BY ID ASC LIMIT 1";
            DataTable tblobj = SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, string.Format(sql, Account));
            if (tblobj.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                sql = "UPDATE Notifi set Status = 1 WHERE ID= " + tblobj.Rows[0][0].ToString();
                SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, sql);
                return tblobj.Rows[0][2].ToString();
            }
            
        }

        string[] statusArr = new string[] { "NoAction", "StartGame_DefaultMode_10Human", "EXITGAME_NORESULT", "WIN", "LOSE" };
        string[] statusNotifi = new string[]{""
            ,"Bạn đang bắt đầu 1 trận đấu Liên Minh đủ điều kiện đề nhận thưởng khi thắng cuộc."
            ,"Trận đấu của bạn đã bị hủy do game gặp sự cố"
            ,"Chúc mừng bạn đã chiến thắng. Phần thưởng của bạn là 10.000 YPoint và sẽ được quy đổi ra giờ chơi của bạn."
            ,"Bạn đã thua trong trận đấu Liên Minh của mình. Rất tiếc! Bạn không có phần thưởng nào cả." };
        public void SentLOLGameInfo(string computerName, string gameID, string content, string secretkey)
        {
            if (!(secretkey == MD5Class.GetMd5Hash(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString())
                || secretkey == MD5Class.GetMd5Hash(DateTime.Now.AddMinutes(-1).Hour.ToString() + DateTime.Now.AddMinutes(-1).Minute.ToString())))
            {
                //xac thuc khong thanh cong
                throw new Exception("loi xac thuc roi");
            }
            string Account = ComputerName2Account(computerName);
            if (Account == "") return;//nếu không đăng nhập hoặc là bật máy thì không thực hiện lênh nữa

            gameID.Replace("\"", "").Replace("SELECT", "").Replace("INSERT", "").Replace("UPDATE", "").Replace("DELETE", "");
            if (gameID.Length > 16) gameID = gameID.Substring(0, 15);
            gameID = computerName + "-" + Account + "-" + gameID;
            
            int statusIndex = 0;
            //kiểm tra Lay tinh trang lan truoc cua gameID
            string sql = "SELECT ID, Status FROM LOLGameLog WHERE GameID = \""+gameID+"\" ORDER BY ID DESC LIMIT 1;";
            string lastStatus = "NONE";
            DataTable tbltmp = SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, sql);
            if (tbltmp.Rows.Count > 0)
            {
                lastStatus = tbltmp.Rows[0][1].ToString();
            }
            //neu tran dau chua kety thuc thi xu ly tinh toan
            if (lastStatus != "WIN" && lastStatus != "LOSE")
            {
                //kiểm tra chế độ chơi
                if (content.Contains("Initializing GameModeComponents for mode=CLASSIC") && content.Replace("(is HUMAN PLAYER)", "■").Split('■').Length == 11)
                {
                    statusIndex = 1;
                    //kiêm tra tình trang kết thúc game
                    if (content.Contains("\"message_body\":\"Game exited\""))
                    {
                        statusIndex = 2;
                        //kiem tra tinh xac thuc cua tran dau. Truoc khi xuat hien thong bao thang tran he thong phai nhan duoc thong bao bat dau tran dau
                        sql = "SELECT Count(*) FROM LOLGameLog WHERE Status = \"StartGame_DefaultMode_10Human\" AND GameID = \"" + gameID + "\"";
                        if (SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, sql).Rows[0][0].ToString() != "0")
                        {
                            //kiểm tra thắng thua
                            if (content.Contains("\"exit_code\":\"EXITCODE_LOSE\""))
                            {
                                statusIndex = 4;
                            }
                            else if (content.Contains("\"exit_code\":\"EXITCODE_WIN\""))
                            {
                                statusIndex = 3;
                            }
                            
                        }
                    }
                }
            }
            if (statusArr[statusIndex] != lastStatus)
            {//neu tinh trang moi duoc cap nhat thi xu ly luu tru va cac tyac vu lien quan
                //lưu tình trạng
                try
                {
                    if (statusIndex == 3 || statusIndex == 4)
                    {
                        StreamWriter sw = new StreamWriter(GameMenuConfig.saveDataFolder + gameID + ".txt");
                        sw.Write(content);
                        sw.Close();
                    }
                }catch{}
                sql = "INSERT INTO LOLGameLog (ComputerName,Account,GameID,Description,Status,DateTime) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")";
                SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, string.Format(sql, computerName, Account, gameID, gameID + ".txt", statusArr[statusIndex], DateTime.Now.ToString()));
                //thực hiện đưa dữ liệu vào bảng chờ cộng tiền
                if (statusIndex == 3)
                {
                    if (GameMenuConfig.TienThuongThangLOL > 0)
                    {
                        sql = "INSERT INTO CongT (Account,T) VALUES (\"" + Account + "\"," + GameMenuConfig.TienThuongThangLOL + ")";
                    }
                    else
                    {
                        sql = "INSERT INTO CongT (Account,T,Status) VALUES (\"" + Account + "\"," + GameMenuConfig.TienThuongThangLOL + ",1)";
                    }
                    SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, sql);
                }
                //thực hiện gửi thông báo
                if (statusNotifi[statusIndex] != "")
                {
                    CreateNotifi(Account, statusNotifi[statusIndex]);
                }
                //Thuc hiên kiểm tra xem có đang đấu đội hay không
                try
                {
                    CheckTeamWar(Account, content, statusIndex);
                }
                catch { }
            }
            //Thực hiện chạy Hàm cộng tiền 
            CongT();
        }

        //common funtion
        private void CreateNotifi(string Account, string Content)
        {
            DataTable tblobj = SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, "INSERT INTO Notifi (Account,Content) VALUES(\"" + Account + "\",\"" + Content + "\")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ComputerName"></param>
        /// <returns>Không ai đăng nhập thì trả về "". Có thể máy đã tăt những vẫn get về user</returns>
        public string ComputerName2Account(string ComputerName)
        {
            //ComputerName = "MAY-25";
            string sql = "select systemlogtb.Status, systemlogtb.UserID, usertb.UserName ,systemlogtb.SystemLogId  from systemlogtb left join usertb on (systemlogtb.userID = usertb.userID)  where systemlogtb.MachineName = \"{0}\" order by systemlogtb.SystemLogId DESC limit 1;";
            string AccountID = "";
            //get userID
            try
            {
                DataTable tmp = MySQLDatabase.execute(string.Format(sql, ComputerName));
                string tmpAcName = tmp.Rows[0][2].ToString().Trim();
                if (!(tmpAcName.Contains("-") ||   tmpAcName == "" || tmpAcName == "ADMIN" || tmpAcName == "Quản lý" || tmpAcName == null ))
                {
                    AccountID = tmp.Rows[0][1].ToString().Trim();
                }
                checkHackLog(tmp, ComputerName);
            }
            catch { }
            return AccountID;
        }
        private static List<object[]> lstHacklog = new List<object[]>();
        private void checkHackLog(DataTable tblLog, string ComputerName)
        {
            //1 la sansang
            //2 la mat ket noi
            //3 la dang su dung
            //4 la canh bao
            string hacklog = "";
            string SystemLogId = "";
            if (tblLog.Rows[0][0].ToString() == "2" || tblLog.Rows[0][0].ToString() == "4")
            {
                //Thực hiện ghi nhớ để đếm số lần cảnh báo
                //Nếu cản báo đến 10 lần thì thực hiện tìm kiếm user và báo cáo tình trạng hach vào hacklog
                SystemLogId = tblLog.Rows[0][3].ToString();
                
                bool fountSystemLogId = false;
                for (int i = 0; i < lstHacklog.Count; ++i)
                {
                    object[] ob = lstHacklog[i];
                    //log((string)ob[0] + "-" + (string)ob[1] + "-" + ((DateTime)ob[2]).ToString());
                    if ((string)ob[0] == SystemLogId)
                    {//nếu tìm thấy SystemLogId thì cập nhật số lần mà cập nhật thời gian update
                        ob[1] = ((int)ob[1]) + 1;
                        ob[2] = DateTime.Now;
                        if ((int)ob[1] >= 10)
                        {
                            try
                            {
                                string sql = "select systemlogtb.Status, systemlogtb.UserID, usertb.UserName ,systemlogtb.SystemLogId,usertb.MiddleName,systemlogtb.MachineName, systemlogtb.EnterDate, systemlogtb.EnterTime from systemlogtb left join usertb on (systemlogtb.userID = usertb.userID)  where systemlogtb.MachineName = \"{0}\" and systemlogtb.userID is not null order by systemlogtb.SystemLogId DESC limit 1;";
                                DataTable tmp = MySQLDatabase.execute(string.Format(sql, ComputerName));
                                hacklog = "WARRNING HACK >> UserID = " + tmp.Rows[0][1].ToString() + " >> UserName = " + tmp.Rows[0][4].ToString() + " >> May = " + tmp.Rows[0][5].ToString() + " >> SystemLogID = " + tmp.Rows[0][3].ToString() + " >> Time = " + tmp.Rows[0][6].ToString().Split(' ')[0] + " " + tmp.Rows[0][7].ToString();
                                ob[1] = 0;
                            }
                            catch { }
                        }
                        fountSystemLogId = true;
                    }
                    else
                    {// nếu không phải là SystemLogId thì kiểm tra thời gian nếu đã quá 30p thì xóa log đó
                        if (((DateTime)ob[2]).AddMinutes(30) < DateTime.Now)
                        {
                            lstHacklog.RemoveAt(i);
                            i--;
                        }
                    }
                }
                if (fountSystemLogId == false)
                {
                    lstHacklog.Add(new object[3] { SystemLogId, 1, DateTime.Now });
                }
            }

            try
            {
                if (hacklog != "")
                {
                    StreamWriter sw = new StreamWriter(GameMenuConfig.saveDataFolder + "HackLog.txt", true);
                    sw.WriteLine(DateTime.Now.ToString() + ": " + hacklog);
                    sw.Close();
                }

            }
            catch { }
        }

        private static DateTime LastRunCongT = DateTime.Now.AddSeconds(-21);
        private static bool CongTRunning = false;
        /// <summary>
        /// Thuc hieen chay cong tien moi 20 giay
        /// </summary>
        private void CongT()
        {
            if (CongTRunning == false && LastRunCongT.AddSeconds(20) < DateTime.Now)
            {
                CongTRunning = true;
                try
                {
                    string sql = "SELECT * FROM CongT Where Status = 0  ORDER BY ID ASC";
                    DataTable tbl = SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, sql);
                    sql = "Update usertb set RemainTime = {0}, RemainMoney = {1} where UserId = {2}";
                    string sqlUpdateStaus = "Update CongT set Status = 1 Where ID = {0}";
                    string sqlUpdateCSMLog = "INSERT INTO `paymenttb` ( `UserId`, `VoucherNo`, `VoucherDate`, `VoucherTime`, `Amount`, `AutoAmount`, `TimeTotal`, `Active`, `UserNote`, `Note`, `ServicePaid`, `StaffId`, `MachineName`, `PaymentType`, `PaymentWaitId`) VALUES ( {0}, '', '{1}', '{2}', {3}, {4}, 0, 1, '', 'Hệ Thống Tặng Khuyến Mãi Thắng Trận LOL', 1, 2, NULL, 11, 0);";
                    foreach (DataRow rw in tbl.Rows)
                    {
                        try
                        {
                            //xử lý tính thời gian mới cho người choi
                            string sqlLayThongTinTien = "Select * from usertb where UserId = {0}";
                            DataTable userInfo = MySQLDatabase.execute(string.Format(sqlLayThongTinTien, rw[1].ToString()));
                            int RemainTime = Convert.ToInt32(userInfo.Rows[0]["RemainTime"]);
                            int RemainMoney = Convert.ToInt32(userInfo.Rows[0]["RemainMoney"]);
                            if (RemainTime != 0)
                            {
                                //Phải thực hiện tính cả RemainTime
                                double tientrenphut = (double)RemainMoney / (double)RemainTime;
                                RemainTime = (int)((RemainMoney + Convert.ToInt32(rw[2].ToString())) / tientrenphut);
                            }
                            RemainMoney += Convert.ToInt32(rw[2].ToString());
                            //bắt đầu cập nhâtj dữ liệu
                            MySQLDatabase.execute(string.Format(sql, RemainTime,RemainMoney, rw[1].ToString()));
                            SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, string.Format(sqlUpdateStaus, rw[0].ToString()));
                            MySQLDatabase.execute(string.Format(sqlUpdateCSMLog, rw[1].ToString(), DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString(), DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString(), rw[2].ToString(), rw[2].ToString()));
                            //lấy top 3 người thắng để hiển thị
                            string top3 = "";
                            DataTable tbltop3 = SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, "SELECT Account, SUM(t) as TT FROM CongT GROUP BY Account ORDER BY TT DESC LIMIT 3");
                            foreach (DataRow rwt in tbltop3.Rows)
                            {
                                top3 += " - " + MySQLDatabase.execute("select MiddleName from usertb where userId = " + rwt[0].ToString()).Rows[0][0].ToString() + ":" + rwt[1].ToString() + "đ";
                            }
                            CreateNotifi(rw[1].ToString(), "Bạn đã được cộng " + rw[2].ToString() + " vào tài khoản. Số tiền sẽ cập nhật khi bạn đăng nhập lại vào máy. Top3 người thắng nhiều nhất là " + top3 + ". Cố gắng lên nhé.");
                        }
                        catch (Exception er) { }
                    }
                }
                catch { }
                LastRunCongT = DateTime.Now;
                CongTRunning = false;
            }

        }

        private static DateTime lastClearUp = DateTime.Now;
        private void clearUpData()
        {
            if (lastClearUp.AddDays(1) < DateTime.Now)
            {//sau 1 ngay thi` xoa du bang LOLGameLog
                SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, "DELETE  FROM LOLGameLog");
            }
        }

        /// <summary>
        /// Bảng lưu tạm các tình trạn trận đấu của từng cá nhân để thực hiện xác thực đội đấu
        /// </summary>
        private static DataTable tblCacheTeamWar = null;
        /// <summary>
        /// Hàm xử lý sau khi xác nhận tình trạng đấu của 1 cá nhân, Hàm này lấy ra nick lol dang chơi, các nick cùng đội.
        /// Sau khi tập hợp được đủ 5 bản ghi của 5 người chơi cùng đội hàm này sẽ phát đi 1 lệnh là team bắt đầu trận đấu, thua hoặc là thắng trong trận đấu
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="content"></param>
        /// <param name="statusIndex"></param>
        private void CheckTeamWar(string Account,string content, int statusIndex)
        {
            //Tam thoi chi xu ly truong hop team da thang
            if (statusIndex != 3) return;
            if (tblCacheTeamWar == null)
            {
                tblCacheTeamWar = new DataTable();
                tblCacheTeamWar.Columns.Add("CSMAcc");//AccountID trong csm
                tblCacheTeamWar.Columns.Add("lolAcc");//tên nick lol của cá nhân
                tblCacheTeamWar.Columns.Add("lolTeamName");//tên đội gồm tên các nick lol xếp theo alphab
                tblCacheTeamWar.Columns.Add("lolEnemyName");//tên đội gồm tên các nick lol xếp theo alphab
                tblCacheTeamWar.Columns.Add("lolStaus");//Tình trạng trận đấu
                tblCacheTeamWar.Columns.Add("DataTime");//Thời gian taoj ban ghi
            }
            //xu ly xoa bo cac ban ghi cu hon 5 phut di
            foreach (DataRow rw in tblCacheTeamWar.Rows)
            {
                if (Convert.ToDateTime(rw["DataTime"]) < DateTime.Now.AddMinutes(-5))
                {
                    rw.Delete();
                }
            }
            //lấy tài khoản lol đang chơi
            string clientID = content.Substring(content.IndexOf("netUID: ") +"netUID: ".Length, 1).Trim();
            string lolAcc = content.Substring(content.IndexOf("clientID " + clientID + " and summonername (") + "clientID 2 and summonername (".Length);
            lolAcc = lolAcc.Substring(0, lolAcc.IndexOf(")")).Trim();
            log("\r\nStartCheckTeamWar - lolAcc:" + lolAcc);
            //lấy tên đội lol được ghép với các tk cùng đội theo alphab
            string[] Team100 = getTeamListAndName(content, "team 100");
            string[] Team200 = getTeamListAndName(content, "team 200");
            string TeamName, EnemyName;
            if (Team100[0].Contains("[" + lolAcc + "]"))
            {
                TeamName = Team100[0];
                EnemyName = Team200[0];
            }
            else
            {
                TeamName = Team200[0];
                EnemyName = Team100[0];
            }
            log("TeamName: " + TeamName + " - EnemyName: " + EnemyName);
            //Lưu trữ vào bảng tạm
            if (tblCacheTeamWar.Select("lolAcc = '" + lolAcc + "' AND lolTeamName = '" + TeamName + "'").Length == 0)
            {
                tblCacheTeamWar.Rows.Add(new string[] { Account, lolAcc, TeamName, EnemyName, statusIndex.ToString(), DateTime.Now.ToString() });
            }
            //Thực hiện kiểm tra các bản ghi hiện có xem có đội nào đã đủ 5 staus giống nhau chưa
            DataRow[] rwArr = tblCacheTeamWar.Select("lolTeamName = '" + TeamName + "'");
            log("Number Of Status win = " + rwArr.Length.ToString());
            if (rwArr.Length == 5)
            {//da xac nhan du 5 ban ghi chen thang, day la 1 chien thang hop le
                string sql = "INSERT INTO TeamWar (\"TeamName\",\"EnemyName\",\"Status\") VALUES (\"{0}\",\"{1}\",\"{2}\")";
                SQLiteDatabase.connectDBAndGetData(GameMenuConfig.databaseFile, string.Format(sql,rwArr[0]["lolTeamName"].ToString(),rwArr[0]["lolEnemyName"].ToString(),"WIN"));
                foreach (DataRow rw in rwArr)
                {
                    CreateNotifi(rw["CSMAcc"].ToString(), "Chúc mừng đội bạn đã chiến thắng. Đội " + TeamName + " của bạn đã được cộng thêm 1 điểm thắng trận.");
                    rw.Delete();
                }
                log("TeamWIN");
            }
            log("check fault");
        }
        private string[] getTeamListAndName(string content, string TeamID){
            string[] tmpTeam100Text = content.Replace(TeamID, "■").Split('■');
            string[] Team100 = new string[6];
            for (int i = 0; i<5; i++)
            {
                Team100[i] = tmpTeam100Text[i+1].Substring(tmpTeam100Text[i+1].IndexOf('(')+1,tmpTeam100Text[i+1].IndexOf(')')-tmpTeam100Text[i+1].IndexOf('(')-1).Trim();
            }
            Array.Sort<string>(Team100);
            for (int i = 1; i <= 5; ++i)
            {
                Team100[0] += "[" + Team100[i] + "]-";
            }
            Team100[0] = Team100[0].Substring(0, Team100[0].Length - 1);
            return Team100;
        }

        
        private void log(string msg)
        {
            try
            {
                StreamWriter sw = new StreamWriter(GameMenuConfig.saveDataFolder + "SysLog.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + msg);
                sw.Close();
                
            }
            catch { }
        }



        public string GetAccountName(string ComputerName)
        {
            string sql = "select systemlogtb.Status, systemlogtb.UserID, usertb.MiddleName ,systemlogtb.SystemLogId  from systemlogtb left join usertb on (systemlogtb.userID = usertb.userID)  where systemlogtb.MachineName = \"{0}\" order by systemlogtb.SystemLogId DESC limit 1;";
            string AccountName = "";
            //get userID
            try
            {
                DataTable tmp = MySQLDatabase.execute(string.Format(sql, ComputerName));
                AccountName = tmp.Rows[0][2].ToString().Trim();
            }
            catch { }
            return AccountName;
        }
    }
}
