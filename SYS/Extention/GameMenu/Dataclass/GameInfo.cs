using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using GameMenuLib;

namespace Extention.DOC.Dataclass
{
    [DataContract]
    public class GameInfo
    {
        string _ID;
        string _iconUrl;
        byte[] _iconByte;
        string _name;
        string _groupName;
        string _runFIle;
        string _folderPath;
        string _localArchivePath;
        string _processName;
        int _imageListID;
        [DataMember]
        public string ID
        {
            set
            {
                _ID = value;
            }
            get
            {
                return _ID;
            }
        }
        [DataMember]
        public byte[] iconByte
        {
            set
            {
                _iconByte = value;
            }
            get
            {
                return _iconByte;
            }
        }
        public Image iconImage
        {
            set
            {
                _iconByte = Common.objectToByteArr(value);
            }
            get
            {
                return (Image)Common.byteArrToObject(_iconByte);
            }
        }
        [DataMember]
        public string iconUrl
        {
            set
            {
                _iconUrl = value;
            }
            get
            {
                return _iconUrl;
            }
        }
        [DataMember]
        public string name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        [DataMember]
        public string groupName
        {
            set
            {
                _groupName = value;
            }
            get
            {
                return _groupName;
            }
        }
        [DataMember]
        public string runFIle
        {
            set
            {
                _runFIle = value;
            }
            get
            {
                return _runFIle;
            }
        }
        [DataMember]
        public string folderPath
        {
            set
            {
                _folderPath = value;
            }
            get
            {
                return _folderPath;
            }
        }
        [DataMember]
        public string localArchivePath
        {
            set
            {
                _localArchivePath = value;
            }
            get
            {
                return _localArchivePath;
            }
        }
        [DataMember]
        public string processName
        {
            set
            {
                _processName = value;
            }
            get
            {
                return _processName;
            }
        }
        [DataMember]
        public int imageListID
        {
            set
            {
                _imageListID = value;
            }
            get
            {
                return _imageListID;
            }
        }
        [DataMember]
        public string fullPath
        {
            get 
            { 
                return folderPath + runFIle; 
            }
            set { throw new Exception("ko set duoc"); }
        }
    }
}
