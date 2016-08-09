using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;
namespace Cota.Core
{
    /// <summary>
    /// Sử lý các tác vụ chung kết nối với CSDL
    /// </summary>
    /// <typeparam name="O">Kiểu DataClass dùng để sử lý</typeparam>
    /// 
    [DataContract]
    public abstract class Base : Object , IBase
    {
        private Type T;
        public Base()
        {
            Plugin.beforCall("Base", this, null);
            Plugin.Call("Befor_Base_Init", this, null);
            Type cache = Cache.load<Type>("Base" + this.GetType().FullName);
            if (cache == null)
            {
                //tìm đối tượng cao nhất là DataClass
                T = this.GetType();
                while (((TableAttribute[])T.GetCustomAttributes(typeof(TableAttribute), false)).Length == 0)
                {
                    T = T.BaseType;
                }
                Cache.save("Base" + this.GetType().FullName,T);
            }
            else
            {
                T = cache;
            }
        }
        
        private PropertyInfo[] _getPropertyList(Type type = null)
        {
            if(type == null){
                type = T;
            }
            PropertyInfo[] arrObj = Cache.load<PropertyInfo[]>("_getPropertyList" + type.FullName);
            if (arrObj == null){
                arrObj = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                Cache.save("_getPropertyList" + type.FullName, arrObj);
            }
            return arrObj;
        }

        /// <summary>
        /// Phục vụ cho hàm insert 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="longID"></param>
        /// <returns></returns>
        private long _insertThis(Type type = null,long longID = -1)
        {
            string tableName = _getTableName(type);
            PropertyInfo[] propertyInfos = _getPropertyList(type);
            //sql string
            string sql = "INSERT INTO {0} ({1}) VALUES ({2});SELECT scope_identity()";
            string pos1 = "";
            string pos2 = "";
            //Create command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //set value
            object tmpValue;
            foreach (PropertyInfo ProInfo1 in propertyInfos)
            {
                if (((ColumnAttribute)(ProInfo1.GetCustomAttributes(typeof(ColumnAttribute), true)[0])).IsDbGenerated == false)
                {
                    //set id cho cac lop thua ke
                    if (longID != -1 && ((ColumnAttribute)(ProInfo1.GetCustomAttributes(typeof(ColumnAttribute), true)[0])).IsPrimaryKey == true)
                    {
                        ProInfo1.SetValue(this, longID, null);
                    }
                    //cau hinh cac gia tri
                    tmpValue = ProInfo1.GetValue(this, null);
                    if (tmpValue != null)
                    {
                        pos1 += " , [" + ProInfo1.Name + "]";
                        pos2 += " , @" + ProInfo1.Name;
                        cmd.Parameters.AddWithValue("@" + ProInfo1.Name, tmpValue);
                    }
                }
            }
            cmd.CommandText = string.Format(sql, tableName, pos1.Substring(2), pos2.Substring(2));
            //run sqlCommand
            DataTable tmlTable = Common.sqlCmd(cmd);
            if (longID != -1)
            {
                return longID;
            }
            return Convert.ToInt64(tmlTable.Rows[0][0]); ;
        }

        /// <summary>
        /// Chuyển từ dataRow sang đối tượng tương ứng
        /// </summary>
        /// <param name="rw">dataRow chứa dữ liệu</param>
        /// <param name="oj">đối tượng tương ứng đã được khởi tạo sẵn</param>
        /// <param name="type">Kiểu của đối tượng cần gán dữ liệu</param>
        private void _copyDataRow2Object(DataRow rw, object oj, Type type = null)
        {
            if (type == null)
            {
                type = T;
            }

            if (type.BaseType.BaseType.Name != "Object")
            {
                _copyDataRow2Object(rw, oj, type.BaseType);
            }

            PropertyInfo[] propertyInfos = _getPropertyList(type);
            string tblName = _getTableName(type);
            string tblnameRepled = tblName.Replace('.', '_') + "_";
            foreach (PropertyInfo ProInfo1 in propertyInfos)
            {
                if (rw[tblnameRepled + ProInfo1.Name] != DBNull.Value)
                {
                    ProInfo1.SetValue(oj, rw[tblnameRepled + ProInfo1.Name], null);
                }
            }
        }

        /// <summary>
        /// Tạo ra hàng đợi chứu các tên bảng liên kết trong CSDL
        /// </summary>
        /// <returns></returns>
        private Queue<Type> _getTableQueue()
        {
            Queue<Type> tableType = new Queue<Type>();
            Type tmpType = T;
            while (tmpType.BaseType.Name != "Object")
            {
                tableType.Enqueue(tmpType);
                tmpType = tmpType.BaseType;
            }
            return tableType;
        }
        /// <summary>
        /// Tạo ra câu lên INNER JOIN ví dụ: "User INNER JOIN resource where (user.ID = resource.ID)"
        /// </summary>
        /// <param name="tableName">Hàng đợi chứa tên các table cần join</param>
        /// <returns></returns>
        private string _getStringInnerJoin(Queue<Type> tableType)
        {
            string res = "";
            string lastTableName = _getTableName(tableType.Dequeue());
            string nowTableName = "";
            res += lastTableName;
            while (tableType.Count != 0)
            {
                nowTableName = _getTableName(tableType.Dequeue());
                res += string.Format(" INNER JOIN {0} ON ({1}.ID = {2}.ID) ", nowTableName, lastTableName, nowTableName);
                lastTableName = nowTableName;
            }
            return res;
        }
        /// <summary>
        /// Lấy ra các trường và tên trường cần select
        /// </summary>
        /// <param name="tableType">Hàng đợi chứa các kiểu dữ liệ từ cao xuốg</param>
        /// <returns></returns>
        private string _getStringFeildReqSelect(Queue<Type> tableType)
        {
            string res = "";
            while (tableType.Count != 0)
            {
                Type type = tableType.Dequeue();
                string tableName = _getTableName(type);
                PropertyInfo[] propertyInfos = _getPropertyList(type);
                foreach (PropertyInfo ProInfo1 in propertyInfos)
                {
                    res += " , " + tableName + "." + ProInfo1.Name + " as " + tableName.Replace('.','_') + "_" + ProInfo1.Name;
                }
            }
            return res.Substring(2);
        }
        /// <summary>
        /// Chuyền vào câu sql select ra Datatable và chuyển toàn bộ sang List chứa các đối tượng của Dataclass
        /// </summary>
        /// <typeparam name="O">Kiểu đối tượng cần tạo để trả về</typeparam>
        /// <param name="sql">Câu lệnh sql</param>
        /// <returns></returns>
        private List<O> _getList4SQLText<O>(string sql)
        {
            return _getList4SQLCommand<O>(new SqlCommand(sql));
        }
        /// <summary>
        /// Chuyền vào câu sqlCommand select ra Datatable và chuyển toàn bộ sang List chứa các đối tượng của Dataclass
        /// </summary>
        /// <typeparam name="O">Kiểu đối tượng cần tạo để trả về</typeparam>
        /// <param name="cmd">Câu lệnh sqlComand đã add sẵn các giá trị</param>
        /// <returns></returns>
        private List<O> _getList4SQLCommand<O>(SqlCommand cmd)
        {
            List<O> rs = new List<O>();
            DataTable result = null;
            try
            {
                result = Common.sqlCmd(cmd);
            }
            catch
            {
                return rs;
            }
            foreach (DataRow dbRow in result.Rows)
            {
                object oj = Activator.CreateInstance(this.GetType());
                _copyDataRow2Object(dbRow, oj, T);
                rs.Add((O)oj);
            }
            return rs;
        }
        /// <summary>
        /// Sửa lý đưa ra câu lênh sql select theo page
        /// </summary>
        /// <param name="innerJoinString">chuỗi chứa các bảng innerjoin với nhau</param>
        /// <param name="where">chuỗi chứa điều kiện - có thể là rỗng</param>
        /// <param name="oderByString">chuỗi chứa kiểu sắp xếp</param>
        /// <param name="pageNumber">Trang cần lấy</param>
        /// <param name="pageSize">Số bản ghi có trong mỗi trang</param>
        /// <returns></returns>
        /// WITH paged AS ( SELECT ROW_NUMBER() OVER(ORDER BY ID DESC) AS RowNum , * FROM dbo.SYS_Users )
        /// SELECT * FROM paged WHERE RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize ORDER BY ID DESC
        private string _getSqlTextPaged(string feildReqString, string innerJoinString, string where, string oderByString, long pageNumber, long pageSize)
        {
            string sql = "WITH paged AS ( SELECT ROW_NUMBER() OVER({0}) AS RowNum , {1} FROM {2} {3} ) ";
            sql += "SELECT * FROM paged WHERE RowNum BETWEEN {4} AND {5} ";
            return string.Format(sql, oderByString, feildReqString, innerJoinString, where, (pageNumber - 1) * pageSize + 1, pageNumber * pageSize);
        }
        private string _getSqlTextPageCount(string innerJoinString, string where)
        {
            string sql = " SELECT  count(*) FROM {0} {1} ";
            return string.Format(sql, innerJoinString, where);
        }
        //========================================================================================//
        /// <summary>
        /// Lấy table tương ứng với kiểu dữ liệu
        /// </summary>
        /// <param name="type">Kiểu dữ liệu</param>
        /// <returns></returns>
        public string _getTableName(Type type = null)
        {
            if (type == null)
            {
                return ((TableAttribute)(T.GetCustomAttributes(typeof(TableAttribute), false)[0])).Name;
            }
            else
            {
                return ((TableAttribute)(type.GetCustomAttributes(typeof(TableAttribute), false)[0])).Name;
            }
        }
        /// <summary>
        /// insert list các đối tượng vào DB
        /// </summary>
        /// <param name="lst"></param>
        public void insertList<O>(List<O> lst){
            Plugin.beforCall("insertList", this, new object[] { lst });

            foreach (object rw1 in lst)
            {
                (this as IBase).insert();
            }

            Plugin.afterCall("insertList", this, new object[] { lst });
        }
        /// <summary>
        /// insert du lieu lop hien tai vao db
        /// </summary>
        /// <param name="type">mac dinh bang null - kieu du lieu se tuong ung voi kieu cua lop dang goi ham nay</param>
        /// <returns>ID cua ban ghi vua insert</returns>
        public long insert(Type type = null)
        {
            var firstCall = (type == null) ? true : false;//biến này giúp cho plugin chỉ được gọi 1 lần vì hàm này sẽ được gọi đệ quy nhiều lần
            if (firstCall) Plugin.beforCall("insert", this, new object[] { type });

            if (type == null)
            {
                type = T;
            }

            long id = -1;
            if (type.BaseType.BaseType.Name != "Object")
            {
                id = insert(type.BaseType);
            }
            long rID = _insertThis(type, id);
            type.GetProperty("ID").SetValue(this,rID,null);

            if (firstCall) Plugin.afterCall("insert", this, new object[] { type });
            return rID;
        }
        /// <summary>
        /// Update list các đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng thừa kế cần update dữ liệu</typeparam>
        /// <param name="lst"></param>
        public void updateList<O>(List<O> lst)
        {
            Plugin.beforCall("updateList", this, new object[] { lst });

            foreach (object rw1 in lst)
            {
                (this as IBase).update();
            }

            Plugin.afterCall("updateList", this, new object[] { lst });
        }
        /// <summary>
        /// Update bàn ghi dựa vào dữ liệu của object hiện tại
        /// </summary>
        public void update(Type type = null)
        {
            bool firstCall = (type == null) ? true : false;//biến này giúp cho plugin chỉ được gọi 1 lần vì hàm này sẽ được gọi đệ quy nhiều lần
            if (firstCall) Plugin.beforCall("update", this, new object[] { type });

            if (type == null)
            {
                type = T;
            }
            if (type.BaseType.Name == "Object") { return; }
            //su ly
            string tableName = _getTableName(type);
            PropertyInfo[] propertyInfos = _getPropertyList(type);
            //Create command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;///////////////////////////////////////////////////////////////;;p
            //sql string
            string sql = "UPDATE " + tableName + " SET {0} WHERE [ID] = @ID";
            string pos0 = "";
            object objTmp;
            foreach (PropertyInfo ProInfo1 in propertyInfos)
            {
                if (((ColumnAttribute)(ProInfo1.GetCustomAttributes(typeof(ColumnAttribute), true)[0])).IsPrimaryKey == false)
                {
                    pos0 += " , [" + ProInfo1.Name + "] = @" + ProInfo1.Name;
                }
                objTmp = ProInfo1.GetValue(this,null);
                if(objTmp == null){
                    objTmp = DBNull.Value;
                }
                cmd.Parameters.AddWithValue("@" + ProInfo1.Name, objTmp);
            }
            cmd.CommandText = string.Format(sql, pos0.Substring(2));
            Common.sqlCmd(cmd);
            //lap dq
            update(type.BaseType);

            if (firstCall) Plugin.afterCall("update", this, new object[] { type });
        }
        /// <summary>
        /// Xóa toàn bộ các bạn ghi có trogn list 
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của lớp đôi tượng cần xóa</typeparam>
        /// <param name="lst"></param>
        public void deleteList<O>(List<O> lst)
        {
            Plugin.beforCall("deleteList", this, new object[] { lst });

            foreach (object rw1 in lst)
            {
                (this as IBase).delete();
            }

            Plugin.afterCall("deleteList", this, new object[] { lst });
        }
        /// <summary>
        /// Xóa bàn ghi dựa vào ID của lớp hiên tại
        /// </summary>
        public void delete()
        {
            Plugin.beforCall("delete", this, new object[] {});

            long ID = (this as ExtentionBase.IDataClass).ID;
            //tao stack luu cac ten bang
            Queue<Type> tableType = _getTableQueue();
            while (tableType.Count > 0)
            {
                Common.sqlText("DELETE FROM " + _getTableName(tableType.Dequeue()) + " WHERE [ID] = " + ID.ToString());
            }

            Plugin.afterCall("delete", this, new object[] { });
        }
        /// <summary>
        /// Trả về 1 đối tượng mới được lấy ra từ DB
        /// </summary>
        /// <param name="ID">ID của bản ghi muôn select</param>
        /// <returns>Trả về NULL nếu không có kết quả nào</returns>
        public O selectByID<O>(long ID)
        {
            Plugin.beforCall("selectByID", this, new object[] { ID });

            List<O> a = select<O>(1, 1, " " + _getTableName() + ".ID = " + ID.ToString() + " ");

            Plugin.afterCall("selectByID", this, new object[] { ID });
            if (a.Count > 0)
            {
                return a[0];
            }
            return default(O);
        }
        /// <summary>
        /// Select top num  
        /// neu orderBy = null thi se lay khoa chinh
        /// orderType = DESC || ASC
        /// </summary>
        /// <param name="topNum">So ban ghi muon lay</param>
        /// <param name="orderBy">neu orderBy = null thi se lay khoa chinh</param>
        /// <param name="orderType">orderType = DESC || ASC</param>
        public List<O> select<O>(long pageNumber = 1 ,long pageSize = 20 ,string where = "", string orderBy = null, SortOrder orderType = SortOrder.Descending)
        {
            Plugin.beforCall("select", this, new object[] { pageNumber, pageSize, where, orderBy, orderType });
            //set default
            string orderTypeStr = (orderType == SortOrder.Descending)?"DESC":"ASC";
            if(orderBy == null){orderBy = _getTableName(T) + ".ID";}

            //chuỗi các trường cần select
            string feildReqString = _getStringFeildReqSelect(_getTableQueue());
            //lấy câu lênh join
            string innerJoinString = _getStringInnerJoin(_getTableQueue());
            //tạo chuỗi where
            if (where != "") { where = " WHERE " + where; }
            //taoj chuỗi order 
            string orderString = " ORDER BY " + orderBy + " " + orderTypeStr + " ";
            //tạo câu lệnh sql
            string sql = _getSqlTextPaged(feildReqString,innerJoinString, where, orderString, pageNumber, pageSize);
            //lấy dữ liệu trả về
            List<O> rs = _getList4SQLText<O>(sql);

            Plugin.afterCall("select", this, new object[] { pageNumber, pageSize, where, orderBy, orderType });
            return rs;
        }
        public long selectGetCount(string where = "")
        {
            //lấy câu lênh join
            string innerJoinString = _getStringInnerJoin(_getTableQueue());
            //tạo chuỗi where
            if (where != "") { where = " WHERE " + where; }
            //tạo câu lệnh sql
            string sql = _getSqlTextPageCount( innerJoinString, where);
            //lấy dữ liệu trả về
            return Convert.ToInt64(Common.sqlText(sql).Rows[0][0]);
        }
        string search_whereParam = ",(,),and,or,and(,)and,or(,)or";
        /// <summary>
        /// Hàm tìm kiếm - chuyền vào tên các trường và nội dung tìm kiếm 
        /// </summary>
        /// <typeparam name="O">Kiểu dữ liệu trả về</typeparam>
        /// <param name="actions">Tên trường 1, nội dung tìm kiếm 1, tên trường 2, nội dung tìm kiếm 2</param>
        /// <returns></returns>
        public List<O> search<O>(long pageNumber = 1 ,long pageSize = 20,int CompareType = 1,params string[] param)
        {
            Plugin.beforCall("search", this,  param );

            Type type = T;
            //chuỗi các trường cần select
            string feildReqString = _getStringFeildReqSelect(_getTableQueue());
            //lấy câu lênh join
            string innerJoinString = _getStringInnerJoin(_getTableQueue());
            //tạo chuỗi order
            string orderString = " ORDER BY " + _getTableName(type) + ".ID DESC ";
            //Tạo chuỗi where
            string where = "";

            for (int i = 0; i < param.Length-1; i += 3)
            {
                string ToanTu = param[i].ToLower();
                if (!search_whereParam.Contains(ToanTu))
                {
                    throw new Exception("Toán tử tìm kiếm không hợp lệ");
                }
                string truong = param[i + 1];
                string giatri = param[i + 2];
                if (!truong.Contains(" ") && !giatri.Contains(" "))
                {
                    where += " " + ToanTu + " " + _getCompareString(truong,giatri,CompareType);
                }
            }
            if (where != "") { where = " WHERE " + where; }
            //Tạo câu lệnh sqlCommand
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = _getSqlTextPaged(feildReqString,innerJoinString, where, orderString, pageNumber, pageSize);
            

            //thực hiện select thử và convert ra kiểu đối tượng ddataclass  để trả về 
            List<O> rs = _getList4SQLCommand<O>(cmd);
            

            Plugin.afterCall("search", this, param );
            return rs;
        }
        private string _getCompareString(string truong, string giatri, int CompareType = 1)
        {
            if (CompareType == 2)
            {
                return " " + truong + " LIKE " + "N'" + giatri + "%'";
            }
            if (CompareType == 3)
            {
                return " " + truong + " = " + "N'" + giatri + "'";
            }
            return " " + truong + " LIKE " + "N'%" + giatri + "%'";
        }
        public long searchGetCount(int CompareType  =1 ,params string[] param)
        {
            //lấy câu lênh join
            string innerJoinString = _getStringInnerJoin(_getTableQueue());
            //Tạo chuỗi where
            string where = "";
            for (int i = 0; i < param.Length - 1; i += 3)
            {
                string ToanTu = param[i].ToLower();
                if (!search_whereParam.Contains(ToanTu))
                {
                    throw new Exception("Toán tử tìm kiếm không hợp lệ");
                }
                string truong = param[i + 1];
                string giatri = param[i + 2];
                if (!truong.Contains(" ") && !giatri.Contains(" "))
                {
                    where += " " + ToanTu + " " + _getCompareString(truong, giatri, CompareType);
                }
            }
            if (where != "") { where = " WHERE " + where; }
            //Tạo câu lệnh sqlCommand
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = _getSqlTextPageCount( innerJoinString, where);
            //thực hiện select thử và convert ra kiểu đối tượng ddataclass  để trả về 
            return Convert.ToInt64(Common.sqlCmd(cmd).Rows[0][0]);
        }
    }
}
