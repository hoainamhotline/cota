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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_Users")]
    public abstract class User : Resource, Cota.Core.ExtentionBase.IDataClass
    {
        private long _ID;
		
		private string _firstName;
		
		private string _lastName;
		
		private System.Nullable<System.DateTime> _birthday;
		
		private System.Nullable<bool> _gender;
		
		private string _address;
		
		private string _phoneNumber;

        private long _money;
		
		private string _note;
		
		private string _account;

        private string _password;
		
		private System.Nullable<short> _status;
        
        
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userID", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="NVarChar(50)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="NVarChar(50)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				this._lastName = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_birthday", DbType="Date")]
		public System.Nullable<System.DateTime> birthday
		{
			get
			{
				return this._birthday;
			}
			set
			{
				this._birthday = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gender", DbType="Bit")]
		public System.Nullable<bool> gender
		{
			get
			{
				return this._gender;
			}
			set
			{
				this._gender = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_address", DbType="NVarChar(300)")]
		public string address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_phoneNumber", DbType="VarChar(12)")]
		public string phoneNumber
		{
			get
			{
				return this._phoneNumber;
			}
			set
			{
				this._phoneNumber = value;
			}
		}
        [DataMember]
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_money", DbType = "BigInt")]
        public long money
		{
			get
			{
                return this._money;
			}
			set
			{
                this._money = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_note", DbType="NText")]
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
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_account", DbType="VarChar(20)")]
		public string account
		{
			get
			{
				return this._account;
			}
			set
			{
				this._account = value;
			}
		}
        
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_password", DbType="VarChar(8)")]
		public string password
		{
            get
            {
                return this._password;
            }
			set
			{
				this._password = value;
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status", DbType="SmallInt")]
		public System.Nullable<short> status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}
		
    }
}
