using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cota.Core;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.Reflection;
using Cota.Core.Dataclass;
using Cota.Core.ExtentionBase;
namespace Cota.Core.Business
{
    /// <summary>
    /// Xử lý trung cho tất cả các loai tài nguyên
    /// Khi select sẽ tự nhận diện và trả về 1 object là class nằm trong SYS.Business của tài nguyên đó
    /// </summary>
    public sealed class OtherResource : IResource
    {
        private object _ResObject;
        /// <summary>
        /// Khởi tạo và tự load thông tin tài nguyên
        /// </summary>
        /// <param name="ID">ID của tài nguyên cần load</param>
        public OtherResource(long ID)
        {
            _ResObject = selectByID(ID);
        }
        
        /// <summary>
        /// Lấy ra chuỗi Family List chứa ID của chính đối tượng và id của các parent của nó
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string getFamyliList()
        {
            if ((this as IResource).parentID == null)
            {
                return "," + (this as IResource).ID.ToString() + ",";
            }
            else
            {
                object resParent = selectByID((long)(this as IResource).parentID);
                return "," + (this as IResource).ID.ToString() + (resParent as IGroup).familyList;
            }
        }
        /// <summary>
        /// kiểm tra loại tài nguyên và trả về tài nguyên tương ứng
        /// </summary>
        /// <param name="ID">ID của tài nguyên</param>
        /// <returns></returns>
        public static object selectByID(long ID)
        {
            Type type = getTypeOfResource(ID);
            return (Activator.CreateInstance(type) as IBase).selectByID<object>(ID);
        }
        /// <summary>
        /// Dựa vào class Init để tìm ra kiểu của tài nguyên
        /// </summary>
        /// <param name="ID">ID của tài nguyên</param>
        /// <returns></returns>
        public static Type getTypeOfResource(long ID)
        {
            DataTable tblTmp = Common.sqlText("SELECT resourceTypeID FROM dbo.SYS_Resources WHERE ID = " + ID.ToString());
            if (tblTmp.Rows.Count <= 0)
            {
                throw new Exception("Tài nguyên cần tìm không tồn tại");
            }
            long resourceTypeID = Convert.ToInt64(tblTmp.Rows[0]["resourceTypeID"]);
            //tìm kiếm trong Init để lấy ra kiểu của đối tượng
            object configData = Init.getConfigData(resourceTypeID);
            if (configData == null)
            {
                throw new Exception("Tài nguyên cần tìm không tồn tại");
            }
            else
            {
                return (configData as IInitConfig).TYPE;
            }
        }


        /// <summary>
        /// ResourceID của tài nguyên
        /// </summary>
        public long ID
        {
            get
            {
                return (_ResObject as IResource).ID;
            }
            set
            {
                (_ResObject as IResource).ID = value;
            }
        }
        /// <summary>
        /// resourceTypeID của tài nguyên
        /// </summary>
        public long resourceTypeID
        {
            get
            {
                return (_ResObject as IResource).resourceTypeID;
            }
            set
            {
                (_ResObject as IResource).resourceTypeID = value;
            }
        }
        /// <summary>
        /// parentID của tài nguyên
        /// </summary>
        public long? parentID
        {
            get
            {
                return (_ResObject as IResource).parentID;
            }
            set
            {
                (_ResObject as IResource).parentID = value;
            }
        }
    }
}
