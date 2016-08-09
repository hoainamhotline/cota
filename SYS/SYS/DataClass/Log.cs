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
    [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SYS_Log")]
    public abstract class Log : Base, ExtentionBase.IDataClass
    {
		
		private long _ID;
		
		private long _userID;
		
		private string _logConten;
		
		private System.DateTime _createdDate;

        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_logConten", DbType="NVarChar(500) NOT NULL", CanBeNull=false)]
		public string logConten
		{
			get
			{
				return this._logConten;
			}
			set
			{
				this._logConten = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_createdDate", DbType="DateTime NOT NULL")]
		public System.DateTime createdDate
		{
			get
			{
				return this._createdDate;
			}
			set
			{
				this._createdDate = value;
			}
		}
    }
}
