using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace Cota.Core
{
    public interface IBase
    {
        string _getTableName(Type type = null);
        void insertList<O>(List<O> lst);
        long insert(Type type = null);
        void updateList<O>(List<O> lst);
        void update(Type type = null);
        void deleteList<O>(List<O> lst);
        void delete();
        O selectByID<O>(long ID);
        List<O> select<O>(long pageNumber = 1, long pageSize = 20, string where = "", string orderBy = null, SortOrder orderType = SortOrder.Descending);
        List<O> search<O>(long pageNumber = 1 ,long pageSize = 20,int CompareType = 1, params string[] param);
    }
}
