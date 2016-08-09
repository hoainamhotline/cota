using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.Text;
using Cota.Core;
namespace Cota.Core.Dataclass
{
    [DataContract]
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_Roles")]
    public abstract class Role : Base, ExtentionBase.IDataClass
    {
        private long _ID;
		
		private string _name;
		
		private System.Nullable<long> _resourceTypeID;

        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rolesID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
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
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_resourceTypeID", DbType="BigInt")]
		public System.Nullable<long> resourceTypeID
		{
			get
			{
				return this._resourceTypeID;
			}
			set
			{
				this._resourceTypeID = value;
			}
		}
    }
}
