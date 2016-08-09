using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Runtime.Serialization;
using SYS;
namespace SYS.Dataclass
{
    [DataContract]
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_Resources")]
    public abstract class Resource : Base, IResource, ExtentionBase.IDataClass
    {
        private long _ID;
		
		private long _resourceTypeID;
		
		private System.Nullable<long> _parentID;

        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_resourceID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_resourceTypeID", DbType="BigInt NOT NULL")]
		public long resourceTypeID
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
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_parentID", DbType="BigInt")]
		public System.Nullable<long> parentID
		{
			get
			{
				return this._parentID;
			}
			set
			{
				this._parentID = value;
			}
		}
       
    }
}
