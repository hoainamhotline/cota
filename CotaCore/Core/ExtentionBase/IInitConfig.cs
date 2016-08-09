using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cota.Core.ExtentionBase
{
    interface IInitConfig
    {
        /// <summary>
        /// ID của kiểu tài nguyên
        /// </summary>
        long RESOURCETYPE_ID { get; }
        /// <summary>
        /// Tên gọi của loại tài nguyên này
        /// </summary>
        string RESOURCE_TYPE_NAME { get; }
        /// <summary>
        /// Kiểu class tại Bussiness của tài nguyên
        /// </summary>
        Type TYPE { get; }
        /// <summary>
        /// ID của nhóm lớn nhất chứa toàn bộ các  tài nguyên kiểu này
        /// </summary>
        long ALL_GROUP_ID { get; }
        /// <summary>
        /// ID trong bảng role chứa quyền insert
        /// </summary>
        long INSERT_ROLE_ID { get; }
        /// <summary>
        /// ID trong bảng role chứa quyền update
        /// </summary>
        long UPDATE_ROLE_ID { get; }
        /// <summary>
        /// ID trong bảng role chứa quyền delete
        /// </summary>
        long DELETE_ROLE_ID { get; }
        /// <summary>
        /// ID trong bảng role chứa quyền Select
        /// </summary>
        long SELECT_ROLE_ID { get; }
        /// <summary>
        /// Tên trường chứa tên đại diện cho tài nguyên này
        /// Ví dụ: với User là trường account với UserGroup là trường groupName
        /// </summary>
        string NAME_FIELD { get; }
        
    }
}
