using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYS;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
namespace SYS.Business
{
    /// <summary>
    /// Class nằm trong module User
    /// </summary>
    public sealed class UserBus : Dataclass.User, SYS.ExtentionBase.IBusinessTemp<UserBus>, SYS.ExtentionBase.IBusiness
    {
        /// <summary>
        /// Yêu cầu login nếu không sẽ trả exeption
        /// </summary>
        public static void requiceLogin()
        {
            if (!isLogin())
            {
                throw new Exception("Cần phải login trước khi thực hiện chức năng này");
            }
        }
        /// <summary>
        /// Kiểm tra xem có tài khoản khác khách đang login hay không
        /// </summary>
        /// <returns></returns>
        public static bool isLogin()
        {
            if (Init.UserConfig.LOGEDIN_ID == null || Init.UserConfig.LOGEDIN_ID == Init.UserConfig.GUEST_ID)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Login - trả về true|false
        /// </summary>
        /// <param name="inpAccount">Account</param>
        /// <param name="inpPassword">Pasword</param>
        /// <returns></returns>
        public static UserBus login(string inpAccount, string inpPassword)
        {
            if (inpAccount.Contains(' '))
            {
                throw new Exception("Tài khoản không được có dấu cách");
            }

            //chuyền tham biến vào hàm plugin
            object[] logininfo = new object[] { inpAccount, inpPassword };
            Plugin.Call("Befor_UserBus_login", null, logininfo);
            inpAccount = (string)logininfo[0];
            inpPassword = (string)logininfo[1];

            
            if (inpPassword.Contains(' '))
            {
                throw new Exception("Mật khẩu phải được mã hóa MD5");
            }

            //mã hóa MD
            inpPassword = inpPassword.ToUpper();
            inpPassword = passWordEndcode(inpPassword);
            if (inpAccount.Contains(" ")) return null;//acc co dau cachnen khong can query nua
            DataTable tbl = Common.sqlText(String.Format("SELECT ID FROM dbo.SYS_Users WHERE account = '{0}' AND password = '{1}' AND status = 1 ", inpAccount, inpPassword));
            UserBus res = null;
            if (tbl.Rows.Count > 0)
            {
                Init.UserConfig.LOGEDIN_ID = Convert.ToInt64(tbl.Rows[0][0]);
                res = (new UserBus()).selectByID<UserBus>(Init.UserConfig.LOGEDIN_ID);
                Plugin.Call("After_UserBus_login", null, new object[] { res });
            }
            return res;
        }

        public static void logout()
        {
            Plugin.Call("Befor_UserBus_logout", null, new object[] { Init.UserConfig.LOGEDIN_ID });
            long tmp = Init.UserConfig.LOGEDIN_ID;
            Init.UserConfig.LOGEDIN_ID = 0;
            Plugin.Call("After_UserBus_logout", null, new object[] { tmp });
        }
        /// <summary>
        /// Lấy ra toàn bộ User là con trực tiếp và con các cấp dưới - có parentID = giá trị chuyền vào
        /// Nếu chuyền vào bằng -1 thì mặc đinh sẽ lấy parentID = all group id
        /// </summary>
        /// <param name="inpParentID">ParentID chuyền vào</param>
        /// <returns></returns>
        public List<UserBus> getAllUser(long inpParentID = -1,long pageNum = 1,long pageSize = 20)
        {
            if (inpParentID == -1) { inpParentID = Init.UserConfig.ALL_GROUP_ID; }
            List<UserGroupBus> lstBb = (new UserGroupBus()).select<UserGroupBus>(1, 9999, " familyList like '%," + inpParentID.ToString() + ",%' ");
            string where = "";
            foreach (UserGroupBus rw in lstBb)
            {
                where += " dbo.SYS_Resources.parentID = " + rw.ID.ToString() + " OR ";
            }
            if (where == "")
            {
                return new List<UserBus>();
            }
            else
            {
                where = where.Substring(0, where.Length - 3);
                return select<UserBus>(pageNum, pageSize, where);
            }
        }
        public long getAllUserGetCount(long inpParentID = -1)
        {
            if (inpParentID == -1) { inpParentID = Init.UserConfig.ALL_GROUP_ID; }
            List<UserGroupBus> lstBb = (new UserGroupBus()).select<UserGroupBus>(1, 9999, " familyList like '%," + inpParentID.ToString() + ",%' ");
            string where = "";
            foreach (UserGroupBus rw in lstBb)
            {
                where += " dbo.SYS_Resources.parentID = " + rw.ID.ToString() + " OR ";
            }
            if (where == "")
            {
                return 0;
            }
            else
            {
                where = where.Substring(0, where.Length - 3);
                return selectGetCount(where);
            }
            
        }

        /// <summary>
        /// Hàm tìm kiếm - chuyền vào tên các trường và nội dung tìm kiếm 
        /// </summary>
        /// <typeparam name="O">Kiểu dữ liệu trả về</typeparam>
        /// <param name="actions">Tên trường 1, nội dung tìm kiếm 1, tên trường 2, nội dung tìm kiếm 2</param>
        /// <returns></returns>
        public List<UserBus> search(long inpParentID, long pageNumber, long pageSize, string truong, string data, int CompareType = 1)
        {
            List<UserGroupBus> lstBb = (new UserGroupBus()).select<UserGroupBus>(1, 9999, " familyList like '%," + inpParentID.ToString() + ",%' ");
            if (lstBb.Count > 0)
            {
                string[] newparam = new string[(lstBb.Count + 1) * 3];
                int index = 0;
                newparam[index++] = "(";
                foreach (UserGroupBus rw in lstBb)
                {
                    newparam[index++] = "parentID";
                    newparam[index++] = rw.ID.ToString();
                    newparam[index++] = "or";
                }
                newparam[index - 1] = ")and";
                newparam[index++] = truong;
                newparam[index++] = data;
                return base.search<UserBus>(pageNumber, pageSize, CompareType, newparam);
            }
            return new List<UserBus>();
            
        }
        public long searchGetCount(long inpParentID, string truong, string data, int CompareType = 1)
        {
            List<UserGroupBus> lstBb = (new UserGroupBus()).select<UserGroupBus>(1, 9999, " familyList like '%," + inpParentID.ToString() + ",%' ");
            if (lstBb.Count > 0)
            {
                string[] newparam = new string[(lstBb.Count + 1) * 3];
                int index = 0;
                newparam[index++] = "(";
                foreach (UserGroupBus rw in lstBb)
                {
                    newparam[index++] = "parentID";
                    newparam[index++] = rw.ID.ToString();
                    newparam[index++] = "or";
                }
                newparam[index - 1] = ")and";
                newparam[index++] = truong;
                newparam[index++] = data;
                return base.searchGetCount(CompareType, newparam);
            }
            return 0;
            
        }
        /// <summary>
        /// Lấy cả các user co trong CSDL
        /// </summary>
        /// <param name="inpParentID"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<UserBus> getAllUserInAllLevel(long pageNum = 1, long pageSize = 20)
        {
            return select<UserBus>(pageNum, pageSize);
        }
        public long getAllUserInAllLevelGetCount()
        {
            return selectGetCount();
        }
        public bool changePassword(long UserID, string oldPass, string newPass)
        {
            string tmp = oldPass;
            if (oldPass.Length != 32 || oldPass.Contains(' ') || newPass.Length != 32 || newPass.Contains(' '))
            {
                throw new Exception("Các mật khẩu gửi lên server chưa được mã hóa MD5");
            }
            UserBus ub = new UserBus().selectByID<UserBus>(UserID);
            if (ub == default(UserBus))
            {
                throw new Exception("Tài khoản không tồn tại hoặc đã bị xóa");
            }

            oldPass = passWordEndcode(oldPass.ToUpper());
            if (((SYS.Dataclass.User)ub).password != oldPass)
            {
                throw new Exception("Mật khẩu cũ không trùng khớp");
            }

            ub.password = newPass;
            ub.update(tmp);

            return true;
        }
        /// <summary>
        /// Ghi đè Base 
        /// Update không cần Oldpass khi 1 người dùng này update thông tin của người dùng khác
        /// </summary>
        /// <param name="type">Kiêu của tài nguyên cần sử lý</param>
        public void update(Type type = null)
        {
            update("", type);
        }
        /// <summary>
        /// Update toàn bộ thông tin của User - thow exception nếu các trường chuyền vào không hợp lệ
        /// Khi update thông tin cho 1 chính mình thì phải chuyền vào pass mới update được
        /// </summary>
        /// <param name="oldPass">Trường ollPass phải được nhập khi update chinh thông tin của minh</param>
        /// <param name="type">Kiêu của tài nguyên cần sử lý</param>
        public void update(string oldPass = "",Type type = null)
        {
            preProcess(this);
            checkDefaut(this);
            if (this.ID == Init.UserConfig.LOGEDIN_ID)
            {
                if (oldPass == "")
                {
                    throw new Exception("Bạn đang sửa thông tin của chính mình, Bạn phải tới trang sửa thông tin cá nhân để nhập pass cũ và sửa thông tin của mình");
                }
                //check mat khau cu
                DataTable tbl = Common.sqlText(String.Format("SELECT ID FROM dbo.SYS_Users WHERE account = '{0}' AND password = '{1}' AND status = 1 ", this.account, passWordEndcode(oldPass.ToUpper())));
                if (tbl.Rows.Count <= 0)
                {
                    throw new Exception("Mật khẩu xác nhận không đúng vui lòng nhập đúng mật khẩu của bạn");
                }
            }
            else
            {
                if (!Permission.check(Init.UserConfig.UPDATE_ROLE_ID, this.ID))
                {
                    throw new Exception("Bạn không có quyền thực hiện việc update với đối tượng này");
                }
            }
            checkValue(this);
            base.update();
            
            afterProcess(this);
        }
        /// <summary>
        /// Ghi đè Base 
        /// Check quyền và các sử lý trước và sau khi goi hàm của Base 
        /// </summary>
        /// <param name="type">Kiêu của tài nguyên cần sử lý</param>
        /// <returns></returns>
        public long insert(Type type = null)
        {
            preProcess(this);
            if (!Permission.check(Init.UserConfig.INSERT_ROLE_ID, (long)this.parentID))
            {
                throw new Exception("Bạn không có quyền thực hiện việc insert với User này vào UserGroup có ID = " + parentID.ToString());
            }
            checkDefaut(this);
            checkValue(this);
            long IDRes = base.insert();
            afterProcess(this);
            return IDRes;
        }
        /// <summary>
        /// Ghi đè Base 
        /// Check quyền và các sử lý trước và sau khi goi hàm của Base 
        /// </summary>
        public void delete()
        {
            if (!Permission.check(Init.UserConfig.DELETE_ROLE_ID, this.ID))
            {
                throw new Exception("Bạn không có quyền thực hiện việc delete với User này");
            }
            checkDefaut(this);
            base.delete();
            afterProcess(this);
        }
        public string getPassword()
        {
            return base.password;
        }
        [DataMember]
        public string password
        {
            get
            {
                return "";
            }
            set
            {
                base.password = value;
            }
        }
        /// <summary>
        /// Kiểm tra tính hợp lệ của User trước khi insert hoặc update
        /// </summary>
        /// <param name="obj">Đối tượng User cần kiểm tra</param>
        public void checkValue(UserBus obj)
        {
            //Tên tài khoản phải có độ dài lớn hơn 1 ký tự + không chứa đấu cách + và là duy nhất
            if (obj.account.Length < 1 || obj.account.Contains(' '))
            {
                throw new Exception("Tên tài khoản phải có độ dài lớn hơn 0 và không chứa dấu cách");
            }
            DataTable tblTmp = Common.sqlText("SELECT top 1 * FROM dbo.SYS_Users WHERE ID <> " + obj.ID.ToString() + " AND account = '" + obj.account + "'");
            if (tblTmp.Rows.Count > 0)
            {
                throw new Exception("Tên tài khoản này đã tồn tại");
            }
            ////+ Tên email phải đúng định dạng + email và phải là duy nhất
            //if (!Common.isEmailString(obj.email))
            //{
            //    throw new Exception("Email không đúng định dạng");
            //}
            //tblTmp = Common.sqlText("SELECT top 1 * FROM dbo.SYS_Users WHERE ID <> " + obj.ID.ToString() + " AND email = '" + obj.email + "'");
            //if (tblTmp.Rows.Count > 0)
            //{
            //    throw new Exception("Email này đã tồn tại");
            //}
        }
        /// <summary>
        /// Sử lý các trường trước khi Insert - chứa các bước sử lý buộc phải thực hiện trên service
        /// </summary>
        /// <param name="obj">Đối tượng User cần kiểm tra</param>
        public void preProcess(UserBus obj)
        {
            obj.resourceTypeID = Init.UserConfig.RESOURCETYPE_ID;
            //sử lý pass word 
            //Nếu client không chuyển pass lên thì lấy mật khẩu là mật khẩu có trong server
            if (obj.getPassword() == null || obj.getPassword().Trim() == "")
            {
                UserBus tmp = base.selectByID<UserBus>(obj.ID);
                if (tmp == default(UserBus))
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }
                else
                {
                    obj.password = tmp.getPassword();
                }
                
            }
            else
            {
                //Nếu client chuyền pass lên thì check và mã hóa pass
                //mật khẩu phải có dạng MD5 + đội dài 32 ký tự + không có đấu cách
                if (obj.getPassword().Length != 32 || obj.getPassword().Contains(' '))
                {
                    throw new Exception("Lỗi hệ thống: Mật khẩu gửi lên server chưa được mã hóa MD5");
                }
                obj.password = passWordEndcode(obj.getPassword().ToUpper());
            }

        }
        public void checkDefaut(UserBus obj)
        {
            //kiểm tra xem nếu là nhóm mặc định thì không cho phép sửa xóa gì 
            if (obj.ID == Init.UserConfig.ADMIN_ID || obj.ID == Init.UserConfig.GUEST_ID)
            {
                throw new Exception("Không được sửa chữa các tài khoản hệ thống");
            }
        }
        public void afterProcess(UserBus obj)
        {
            
        }
        private static string passWordEndcode(string inpPass)
        {
            return Common.MD5Encode(inpPass + "EOffice");
        }
        public string getName()
        {
            return base.account;
        }

        
        [DataMember]
        public string parentGroupName
        {
            get
            {
                UserGroupBus ub = new UserGroupBus();
                ub = ub.selectByID<UserGroupBus>((long)base.parentID);
                return ub.getName();
            }
            set
            {

            }
        }
    }
}
