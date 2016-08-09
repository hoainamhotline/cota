using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYS.Business;
using System.Data;
namespace SYS
{
    public class Permission
    {
        /// <summary>
        /// kiểm tra quyền 
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ResourceID"></param>
        public static bool check(long RoleID, long ResourceID) {
            long UserID = Init.UserConfig.LOGEDIN_ID;
            string User_FamilyList = (new OtherResource(UserID)).getFamyliList();
            string Resource_FamilyList = (new OtherResource(ResourceID)).getFamyliList();
            //Tìm kiếm bản ghi có chứa roleID = roleID chuyền vào
            //+ UserID nằm trong User_FamilyList
            //+ ResourceID nằm trong Resource_FamilyList
            string sql = "SELECT TOP 1 * FROM dbo.SYS_UserRoleResource Where '{0}' LIKE '%,' + CAST([resourceID] as varchar) + ',%' " 
                                                                     + " AND '{1}' LIKE '%,' + CAST([userID] as varchar) + ',%' "
                                                                     + " AND roleID = " + RoleID.ToString();
            DataTable tbl = Common.sqlText(string.Format(sql,Resource_FamilyList,User_FamilyList,RoleID));
            if (tbl.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
