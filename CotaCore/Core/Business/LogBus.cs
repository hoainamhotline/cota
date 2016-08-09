using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cota.Core;
namespace Cota.Core.Business
{
    /// <summary>
    /// Quản lý log
    /// </summary>
    public sealed class LogBus: Dataclass.Log 
    {
        internal void insertList<O>(List<O> lst)
        {
            base.insertList<O>(lst);
        }
        internal long insert(Type type = null)
        {
            createdDate = DateTime.Now;
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
        public List<LogBus> selectByUserID(long inpUserID,long pageNum = 1,long pageSize = 20)
        {
            return select<LogBus>(pageNum, pageSize, "dbo.SYS_Log.UserID = " + inpUserID.ToString());
        }
        public long selectByUserIDGetCount(long inpUserID)
        {
            return selectGetCount("dbo.SYS_Log.UserID = " + inpUserID.ToString());
        }
    }
}
