using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.Text;
using SYS;
namespace SYS.Dataclass
{
    [DataContract]
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_UserGroup")]
    public abstract class UserGroup : Resource, SYS.ExtentionBase.IDataClass, IGroup
    {

        private long _ID;

        private string _groupName;

        private string _familyList;

        private string _note;

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "BigInt NOT NULL", IsPrimaryKey = true)]
        public long ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        [DataMember]
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_groupName", DbType = "NVarChar(250)")]
        public string groupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                this._groupName = value;
            }
        }
        [DataMember]
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_familyList", DbType = "VarChar(250)")]
        public string familyList
        {
            get
            {
                return this._familyList;
            }
            set
            {
                this._familyList = value;
            }
        }
        [DataMember]
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_note", DbType = "NVarChar(500)")]
        public string note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }
    }
}
