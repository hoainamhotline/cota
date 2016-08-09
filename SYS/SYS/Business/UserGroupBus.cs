using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYS;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.Data.SqlClient;
namespace SYS.Business
{
    /// <summary>
    /// CLass nằm trong module User
    /// </summary>
    public sealed class UserGroupBus : Dataclass.UserGroup, SYS.ExtentionBase.IBusinessTemp<UserGroupBus>, SYS.ExtentionBase.IBusiness
    {

        /// <summary>
        /// Lấy ra toàn bộ UserGroup là con trực tiếp - có parentID = giá trị chuyền vào
        /// Nếu chuyền vào bằng -1 thì mặc đinh sẽ lấy parentID = all group id
        /// </summary>
        /// <param name="inpParentID">ParentID chuyền vào</param>
        /// <returns></returns>
        public List<UserGroupBus> getAllUserGroup(long inpParentID = -1)
        {
            if (inpParentID == -1) { inpParentID = Init.UserConfig.ALL_GROUP_ID; }
            return select<UserGroupBus>(1, 99999, " [parentID] = " + inpParentID.ToString(), "dbo.SYS_UserGroup.ID", SortOrder.Ascending);
        }

        /// <summary>
        /// Ghi đè Base 
        /// Check quyền và các sử lý trước và sau khi goi hàm của Base 
        /// </summary>
        /// <param name="type">Kiêu của tài nguyên cần sử lý</param>
        public void update(Type type = null)
        {
            preProcess(this);
            if (!Permission.check(Init.UserGroupConfig.UPDATE_ROLE_ID, this.ID))
            {
                throw new Exception("Bạn không có quyền thực hiện việc update với UserGroup này");
            }
            checkDefaut(this);
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
            if (!Permission.check(Init.UserGroupConfig.INSERT_ROLE_ID, (long)this.parentID))
            {
                throw new Exception("Bạn không có quyền thực hiện việc insert với UserGroup này");
            }
            checkDefaut(this);
            checkValue(this);
            long rs = base.insert();
            afterProcess(this);
            return rs;
        }
        /// <summary>
        /// Ghi đè Base 
        /// Check quyền và các sử lý trước và sau khi goi hàm của Base 
        /// </summary>
        public void delete()
        {
            if (!Permission.check(Init.UserGroupConfig.DELETE_ROLE_ID, this.ID))
            {
                throw new Exception("Bạn không có quyền thực hiện việc delete với UserGroup này");
            }
            checkDefaut(this);
            //không được xóa các Group có chưa Group Con
            DataTable tblTmp = Common.sqlText("SELECT top 1 * FROM dbo.SYS_Resources WHERE parentID = " + this.ID.ToString());
            if (tblTmp.Rows.Count > 0)
            {
                throw new Exception("Chỉ được phép xóa các Group rỗng");
            }
            base.delete();
            afterProcess(this);
        }

        
        public void preProcess(UserGroupBus obj)
        {
            obj.resourceTypeID = Init.UserGroupConfig.RESOURCETYPE_ID;
            if (obj.parentID == null && obj.ID != Init.UserGroupConfig.ALL_GROUP_ID)
            {
                obj.parentID = Init.UserGroupConfig.ALL_GROUP_ID;
            }
            if (obj.parentID == null)
            {
                obj.familyList = "," + obj.ID.ToString() + ",";
            }
        }
        public void checkValue(UserGroupBus obj)
        {
            //+ Tên Group phải có độ dài lớn hơn 2 + Trong cùng 1 parent không được có 2 Group trùng tên
            if (obj.groupName.Length < 3)
            {
                throw new Exception("Tên UserGroup phải có độ dài lớn hơn 2");
            }
            //nếu ParentID khác null thì check các quyền sau
            if (obj.parentID != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT top 1 * FROM dbo.SYS_UserGroup INNER JOIN dbo.SYS_Resources ON (dbo.SYS_UserGroup.ID = dbo.SYS_Resources.ID) WHERE dbo.SYS_UserGroup.ID <> " + obj.ID.ToString() + " AND dbo.SYS_UserGroup.groupName = @groupName AND dbo.SYS_Resources.parentID = " + obj.parentID.ToString();
                cmd.Parameters.AddWithValue("@groupName",obj.groupName);
                DataTable tblTmp = Common.sqlCmd(cmd);
                if (tblTmp.Rows.Count > 0)
                {
                    throw new Exception("Trong cùng UserGroup không được có nhiều UserGroup trùng tên");
                }
                //Không được đặt parentID là con của chính nó
                if (obj.parentID == obj.ID)
                {
                    throw new Exception("Không thể đặt đối tượng là con của chính đối tượng đó");
                }
                //không được đặt parentID là con của con nó
                UserGroupBus parent = base.selectByID<UserGroupBus>((long)obj.parentID);
                if (parent.familyList.Contains("," + obj.ID.ToString() + ","))
                {
                    throw new Exception("Không thể đặt đối tượng là con của các đối tượng là con của chính đối tượng đó");
                }
            }
        }
        public void checkDefaut(UserGroupBus obj)
        {
            //kiểm tra xem nếu là nhóm mặc định thì không cho phép sửa xóa gì 
            if (obj.ID == Init.UserGroupConfig.System_GROUP_ID || obj.ID == Init.UserGroupConfig.KhachHang_GROUP_ID || obj.ID == Init.UserGroupConfig.NhanVien_GROUP_ID || obj.ID == Init.UserGroupConfig.HoiVien_GROUP_ID || obj.ID == Init.UserGroupConfig.KhacVangLai_GROUP_ID)
            {
                throw new Exception("Không được sửa chữa các nhóm hệ thống");
            }
        }
        public void afterProcess(UserGroupBus obj)
        {
            if (obj.parentID != null)
            {
                Common.updateFamilyList4Group(obj._getTableName(), obj.ID);
            }
        }

        public string getName()
        {
            return base.groupName;
        }
    }
}
