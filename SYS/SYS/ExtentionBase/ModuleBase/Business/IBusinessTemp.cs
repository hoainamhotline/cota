using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYS.ExtentionBase
{
    public interface IBusinessTemp<T>
    {
        /// <summary>
        /// Sử lý các trường trước khi Insert - chứa các bước sử lý buộc phải thực hiện trên service
        /// Hàm này thường được thực hiện trước khi Insert hoặc Update CSDL
        /// Sử dụng với các đối tượng do các Client gửi lên
        /// </summary>
        /// <param name="obj"></param>
        void preProcess(T obj);
        /// <summary>
        /// Kiểm tra tính hợp lệ của User trước khi insert hoặc update
        /// Hàm này thường được thực hiện trước khi Inser hoặc Update CSDL
        /// Sử dụng với các đối tượng do các Client gửi lên
        /// </summary>
        /// <param name="obj"></param>
        void checkValue(T obj);
        /// <summary>
        /// Xử lý sau khi đã đã làm thay đổi CSDL
        /// </summary>
        /// <param name="obj"></param>
        void afterProcess(T obj);
        
    }
}
