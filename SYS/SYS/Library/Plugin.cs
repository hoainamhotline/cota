using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
namespace SYS
{
    internal class PluginData
    {
        private int _order;
        private object _obj;
        public PluginData(object inpObj,int inpOrder)
        {
            _order = inpOrder;
            _obj = inpObj;
        }
        public int order
        {
            get { return _order; }
        }

        public object obj
        {
            get { return _obj; }
        }
    }
    public class Plugin
    {
        /// <summary>
        /// Hàm này dùng để gọi đến các vị trí được xác định không theo chuẩn
        /// </summary>
        /// <param name="PluginPosision">Tên của vị trí</param>
        /// <param name="sender">Đối tượng gọi hàm này</param>
        /// <param name="pram">Mảng các object chuyền vào hàm đã gọi hàm này</param>
        
        private static List<string> _debugPluginPosision = null;
        public static void Call(string PluginPosision, object sender, object[] pram)
        {
            //Dành cho debug
            if (Config.isDebug)
            {//ghi ra list các vị trí
                try
                {
                    string path = (new System.Web.UI.Page()).Server.MapPath("\\SYS\\Library\\pluginList.txt");

                    if (_debugPluginPosision == null)
                    {
                        _debugPluginPosision = new List<string>();
                        try
                        {
                            StreamReader sr = new StreamReader(path);
                            _debugPluginPosision.Add(sr.ReadLine());
                            sr.Close();
                        }
                        catch { }
                    }
                    if (!_debugPluginPosision.Contains(PluginPosision))
                    {
                        _debugPluginPosision.Add(PluginPosision);
                        StreamWriter sw = new StreamWriter(path, true);
                        sw.WriteLine(PluginPosision);
                        sw.Close();
                    }
                }
                catch { }
            }
            //Sử lý
            List<PluginData> lst = (List<PluginData>)_pluginReg[PluginPosision];
            if (lst != null)
            {
                foreach (PluginData row in lst)
                {
                    (row.obj as SYS.ExtentionBase.IPlugin).run(sender, pram);
                }
            }
        }
        /// <summary>
        /// Hàm được gọi ở 1 bị trí bất kỳ nào đó
        /// </summary>
        /// <param name="Posision">Tên vị trí</param>
        /// <param name="FuntionName">Tên hàm gọi đến</param>
        /// <param name="sender">Đối tượng gọi hàm này</param>
        /// <param name="pram">Mảng các object chuyền vào hàm đã gọi hàm này</param>
        public static void Call(string Posision,string FuntionName, object sender, object[] pram)
        {
            string name = sender.GetType().Name;
            if (name.Contains('`'))
            {
                name = name.Substring(0, name.IndexOf('`'));
            }
            string nameFuntion = Posision + "_" + sender.GetType().Namespace.Replace('.', '_') + "_" + name + "_" + FuntionName;
            Call(nameFuntion, sender, pram);
        }
        /// <summary>
        /// Hàm được gọi ở đầu các hàm - dùng để load các plugin được đăng ký tương ứng
        /// </summary>
        /// <param name="FuntionName">Tên hàm gọi đến</param>
        /// <param name="sender">Đối tượng gọi hàm này</param>
        /// <param name="pram">Mảng các object chuyền vào hàm đã gọi hàm này</param>
        public static void beforCall(string FuntionName, object sender, object[] pram)
        {
            Call("Befor", FuntionName, sender, pram);
        }
        /// <summary>
        /// Hàm được gọi ở cuối các hàm - dùng để load các plugin được đăng ký tương ứng
        /// </summary>
        /// <param name="FuntionName">Tên hàm gọi đến</param>
        /// <param name="sender">Đối tượng gọi hàm này</param>
        /// <param name="pram">Mảng các object chuyền vào hàm đã gọi hàm này</param>
        public static void afterCall(string FuntionName, object sender, object[] pram)
        {
            Call("After", FuntionName, sender, pram);
        }
        /// <summary>
        /// Bảng băm chứa anh xạ giữa các vị trí và các object của các plugin đã được đăng ký
        /// </summary>
        private static Hashtable _pluginReg = new Hashtable();
        /// <summary>
        /// Hàm xử lý đăng ký plugin
        /// </summary>
        /// <param name="senderName">Tên của vị trí đăng ký, ví dụ: Befor_SYS_Business_Log_insert </param>
        /// <param name="pluginObject"></param>
        public static void reg(string senderName,object pluginObject, int order = 0){
            if (senderName.Contains('`'))
            {
                senderName = senderName.Substring(0, senderName.IndexOf('`'));
            }
            List<PluginData> lst = new List<PluginData>();
            if (_pluginReg.Contains(senderName))
            {
                lst = (List<PluginData>)_pluginReg[senderName];
            }
            addToPluginData(lst, pluginObject, order);
            _pluginReg[senderName] = lst;
        }

        private static void addToPluginData(List<PluginData> lst,object pluginObject,int pluginObjectOrder)
        {
            //kiểm tra sự trùng lặp
            foreach (PluginData rw in lst)
            {
                if (rw.obj.GetType() == pluginObject.GetType())
                {
                    throw new Exception("Lỗi đăng ký 2 lần 1 plugin trên cùng 1 vị trí");
                }
            }
            //đăng ký
            int i;
            for (i = 0; i < lst.Count; ++i)
            {
                if (lst[i].order >= pluginObjectOrder)
                {
                    lst.Insert(i, new PluginData(pluginObject, pluginObjectOrder));
                    return;
                }
            }
            lst.Insert(i, new PluginData(pluginObject, pluginObjectOrder));
        }
    }
}
