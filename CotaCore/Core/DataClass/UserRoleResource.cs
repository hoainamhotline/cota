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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_UserRoleResource")]
    public abstract class UserRoleResource : Base, ExtentionBase.IDataClass
    {
        private long _ID;
		
		private long _userID;
		
		private long _roleID;
		
		private long _resourceID;
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userRoleResourceID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userID", DbType="BigInt NOT NULL")]
		public long userID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this._userID = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_roleID", DbType="BigInt NOT NULL")]
		public long roleID
		{
			get
			{
				return this._roleID;
			}
			set
			{
				this._roleID = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_resourceID", DbType="BigInt NOT NULL")]
		public long resourceID
		{
			get
			{
				return this._resourceID;
			}
			set
			{
				this._resourceID = value;
			}
		}
    }
}
