using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Cota.Core
{
    /// <summary>
    /// Sử dụng các biến static và Hashtable để lưu trữ cache trên bộ nhớ
    /// </summary>
    public class Cache
    {
        private static Hashtable _objectCache = new Hashtable();
        public static void save<T>(string senderName, T pluginObject)
        {
            _objectCache[senderName] = Clone<T>(pluginObject);
        }
        public static T load<T>(string senderName)
        {
            return Clone<T>((T)_objectCache[senderName]);
        }
        /// <summary>
        /// Hàm này sẽ copy toàn bộ đối tượng
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}