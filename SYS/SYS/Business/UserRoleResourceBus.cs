using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYS.Dataclass;
namespace SYS.Business
{
    public sealed class UserRoleResourceBus : Dataclass.UserRoleResource
    {
        /// <summary>
        /// Ghi đè Base 
        /// Check quyền và các sử lý trước và sau khi goi hàm của Base 
        /// </summary>
        /// <param name="type">Kiêu của tài nguyên cần sử lý</param>
        public void update(Type type = null)
        {
            checkValue(this);
            base.update();
        }
        /// <summary>
        /// Ghi đè Base 
        /// Check quyền và các sử lý trước và sau khi goi hàm của Base 
        /// </summary>
        /// <param name="type">Kiêu của tài nguyên cần sử lý</param>
        /// <returns></returns>
        public long insert(Type type = null)
        {
            checkValue(this);
            return base.insert();
        }
        private void checkValue(UserRoleResourceBus obj)
        {
            //kiểm tra trường UserID buộc phải là loại User hoặc là User Group
            Type type = OtherResource.getTypeOfResource(obj.userID);
            if (!(type == typeof(Business.UserBus) || type == typeof(Business.UserGroupBus) || type == typeof(Dataclass.User) || type == typeof(Dataclass.UserGroup)))
            {
                throw new Exception("Đối tượng được cấp quyền phải là User hoặc UserGroup");
            }
            //không được tạo các bản ghi giống nhau
            if (selectByUserIDRoleIDResID(obj.userID, obj.roleID, obj.resourceID).Count > 0)
            {
                throw new Exception("Quyền này hiện đã tồn tại trong CSDL - bạn không thể tạo thêm");
            }
        }
        public List<UserRoleResourceDetailBus> selectByUserIDRoleIDResID(long inpUserID = -1, long inpRoleID = -1, long inpResourceID = -1,long pageNum = 1,long pageSize = 20)
        {
            string where = "";
            if (inpUserID != -1) { where += " AND userID = " + inpUserID.ToString(); }
            if (inpRoleID != -1) { where += " AND roleID = " + inpRoleID.ToString(); }
            if (inpResourceID != -1) { where += " AND resourceID = " + inpResourceID.ToString(); }
            return (new UserRoleResourceDetailBus()).select<UserRoleResourceDetailBus>(pageNum, pageSize, where.Substring(4));
        }
        public long selectByUserIDRoleIDResIDGetCount(long inpUserID = -1, long inpRoleID = -1, long inpResourceID = -1)
        {
            string where = "";
            if (inpUserID != -1) { where += " AND userID = " + inpUserID.ToString(); }
            if (inpRoleID != -1) { where += " AND roleID = " + inpRoleID.ToString(); }
            if (inpResourceID != -1) { where += " AND resourceID = " + inpResourceID.ToString(); }
            return (new UserRoleResourceDetailBus()).selectGetCount(where.Substring(4));
        }
    }
}
