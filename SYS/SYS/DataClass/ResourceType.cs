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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_ResourceType")]
    public abstract class ResourceType : Base, ExtentionBase.IDataClass
    {
        private long _ID;
		
		private string _name;
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_resourceTypeID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long ID
		{
			get
			{
                return this._ID;
			}
            set
            {
                this._ID = value;
            }
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="NVarChar(50)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

    }
}
