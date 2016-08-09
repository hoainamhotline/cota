using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cota.Core.ExtentionBase
{
    public interface IPlugin
    {
        /// <summary>
        /// Hàm dùng chung cho các plugin được dùng để sử lý dữ liệu
        /// </summary>
        /// <param name="sender">Đối tượng chứa hàm gửi yêu cầu</param>
        /// <param name="pram">Các biền chuyền vào của hàm đã gửi yêu cầu</param>
        void run(object sender, object[] pram);
    }
}
