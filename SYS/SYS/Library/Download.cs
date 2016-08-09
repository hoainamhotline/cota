using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace SYS
{
    public class Download
    {
        private static Hashtable _token = new Hashtable();
        public static void regToken(string tokenKey,string filePath)
        {
            KeyValuePair<string, DateTime> obj = new KeyValuePair<string, DateTime>(filePath, DateTime.Now.AddMinutes(1));
            _token[tokenKey] = obj;
        }
        public static string getToken(string tokenKey){
            if (_token.Contains(tokenKey))
            {
                KeyValuePair<string, DateTime> tmp = (KeyValuePair<string, DateTime>)_token[tokenKey];
                _token.Remove(tokenKey);
                if (DateTime.Now <= tmp.Value)
                {
                    return tmp.Key;
                }
            }
            return "";
        }
    }
}