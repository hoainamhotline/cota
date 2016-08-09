using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace SYS.Business
{
    /// <summary>
    /// Class này đa số các hàm là danh riêng cho hệ thống 
    /// chỉ có các hàm select và search là có thể public lên service
    /// </summary>
    public sealed class RoleBus : Dataclass.Role
    {
        internal void insertList<O>(List<O> lst)
        {
            base.insertList<O>(lst);
        }
        internal long insert(Type type = null)
        {
            return base.insert(type);
        }
        internal void updateList<O>(List<O> lst)
        {
            base.updateList<O>(lst);
        }
        internal void update(Type type = null)
        {
            base.update();
        }
        internal void deleteList<O>(List<O> lst)
        {
            base.deleteList<O>(lst);
        }
        internal void delete()
        {
            base.delete();
        }
    }
}
