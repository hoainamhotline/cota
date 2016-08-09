using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cota.Core.ExtentionBase;
namespace Cota.Core.SYSPlugin
{
    public class Befor_Base_Init : PluginBase, IPlugin
    {
        /// <summary>
        /// Xử lý khi bất kỳ đối tượng nào được khởi tạo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pram"></param>
        public void run(object sender, object[] pram)
        {
            if (Init.UserConfig.LOGEDIN_ID == 0)
            {
                 throw new Exception("Bạn chưa đăng nhập hoặc phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại.");
            }
        }
    }
}
