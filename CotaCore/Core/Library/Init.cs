using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using Cota.Core.ExtentionBase;
namespace Cota.Core
{
    public partial class Init
    {
        private static object syncObj = new object();
        private static bool Inited = false;
        public static bool IsInited
        {
            get
            {
                return Inited;
            }
        }
        /// <summary>
        /// Chạy toàn bộ các hàm có kết thúc bằng "_Init"
        /// Đây là các hàm được khai báo bởi các Module 
        /// </summary>
        public static void Run()
        {
            if (!Inited)
            {
                Init a = new Init();
                MethodInfo[] methodInfos = a.GetType().GetMethods();
                foreach (MethodInfo row in methodInfos)
                {
                    if (row.Name.Length > 5)
                    {
                        if (row.Name.Substring(row.Name.Length - 5) == "_Init")
                        {
                            row.Invoke(a, null);
                        }
                    }
                }
                Inited = true;
            }
        }
        /// <summary>
        /// Bảng băm chứa anh xạ giữa các vị trí và các object của các plugin đã được đăng ký
        /// </summary>
        private static Hashtable _InitReg = new Hashtable();
        /// <summary>
        /// Hàm xử lý đăng ký InitConfigData
        /// </summary>
        /// <param name="senderName">Tên của vị trí đăng ký, ví dụ: Befor_SYS_Business_Log_insert </param>
        /// <param name="pluginObject"></param>
        public static void reg(string senderTypeName, object InitConfigObject)
        {
            if (_InitReg.Contains(senderTypeName))
            {
                throw new ArgumentException("Đã có module đăng ký vị trí " + senderTypeName);
            }
            _InitReg.Add(senderTypeName, InitConfigObject);
        }
        public static object getConfigData(string senderTypeName)
        {
            return _InitReg[senderTypeName];
        }
        public static object getConfigData(long resourceTypeID)
        {
            foreach (DictionaryEntry entry in _InitReg)
            {
                if ((entry.Value as IInitConfig).RESOURCETYPE_ID == resourceTypeID)
                {
                    return entry.Value;
                }
            }
            return null;
        }

    }
}
