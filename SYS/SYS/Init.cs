using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Services;

namespace SYS
{
    public partial class Init
    {
        //Init for User
        public class UserConfigClass : SYS.ExtentionBase.IInitConfig
        {
            public long RESOURCETYPE_ID
            {
                get { return 1; }
            }
            public string RESOURCE_TYPE_NAME 
            {
                get { return "Người dùng"; }
            }
            public Type TYPE
            {
                get { return typeof(Business.UserBus); }
            }
            public long GUEST_ID
            {
                get { return 244; }
            }
            public long ALL_GROUP_ID
            {
                get { return 155; }
            }
            public long INSERT_ROLE_ID
            {
                get { return 1; }
            }
            public long UPDATE_ROLE_ID
            {
                get { return 2; }
            }
            public long DELETE_ROLE_ID
            {
                get { return 3; }
            }
            public long SELECT_ROLE_ID
            {
                get { return 4; }
            }
            public string NAME_FIELD
            {
                get { return "account"; }
            }
            //=================================
            public long ADMIN_ID
            {
                get { return 120; }
            }
            /// <summary>
            /// Hàm này sẽ get từ seasion để lấy ra UserID đang login - nếu ko có ai login thì trả về -1
            /// </summary>
            public long LOGEDIN_ID
            {
                get
                {
                    try
                    {
                        long uID = Convert.ToInt64(System.Web.HttpContext.Current.Session["USER_LOGEDIN_ID"]);
                        if (uID == 0)
                        {
                            return GUEST_ID;
                        }
                        else
                        {
                            return uID;
                        }
                    }
                    catch {
                        return GUEST_ID; 
                    }
                }
                set
                {
                    System.Web.HttpContext.Current.Session["USER_LOGEDIN_ID"] = value;
                }
            }
        }
        public static UserConfigClass UserConfig;
        public void USER_Init()
        {
            UserConfig = new UserConfigClass();
            reg(typeof(Business.UserBus).Name, UserConfig);
        }

        //Init for UserGroup
        public class UserGroupConfigClass : SYS.ExtentionBase.IInitConfig
        {
            public long RESOURCETYPE_ID
            {
                get { return 2; }
            }
            public string RESOURCE_TYPE_NAME
            {
                get { return "Nhóm người dùng"; }
            }
            public Type TYPE
            {
                get { return typeof(Business.UserGroupBus); }
            }
            public long ALL_GROUP_ID
            {
                get { return 155; }
            }
            public long System_GROUP_ID
            {
                get { return 235; }
            }
            public long NhanVien_GROUP_ID
            {
                get { return 236; }
            }
            public long KhachHang_GROUP_ID
            {
                get { return 237; }
            }
            public long KhacVangLai_GROUP_ID
            {
                get { return 238; }
            }
            public long HoiVien_GROUP_ID
            {
                get { return 239; }
            }
            public long INSERT_ROLE_ID
            {
                get { return 1; }
            }
            public long UPDATE_ROLE_ID
            {
                get { return 2; }
            }
            public long DELETE_ROLE_ID
            {
                get { return 3; }
            }
            public long SELECT_ROLE_ID
            {
                get { return 4; }
            }
            public string NAME_FIELD
            {
                get { return "groupName"; }
            }
        }
        public static UserGroupConfigClass UserGroupConfig;
        public void USERGROUP_Init()
        {
            UserGroupConfig = new UserGroupConfigClass();
            reg(typeof(Business.UserGroupBus).Name, UserGroupConfig);
        }

        //Init chung cho phần SYS
        public void SYS_Init()
        {
            Plugin.reg("Befor_Base_Init", new SYSPlugin.Befor_Base_Init());
        }
    }
}
