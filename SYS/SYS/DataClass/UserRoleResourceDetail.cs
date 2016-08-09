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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.SYS_UserRoleResourceDetail")]
    public abstract class UserRoleResourceDetail : Base ,ExtentionBase.IDataClass
    {
        private long _ID;

        private long _userID;

		private long _roleID;
		
		private long _resourceID;

        private string _user_account;

        private string _user_groupName;
		
		private System.Nullable<long> _role_resourceTypeID;
		
		private string _role_name;

        private long _resource_resourceTypeID;

        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="BigInt NOT NULL")]
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
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_userID", DbType = "BigInt NOT NULL")]
        public long userID
        {
            get
            {
                return this._userID;
            }
            set
            {
                if ((this._userID != value))
                {
                    this._userID = value;
                }
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
				if ((this._roleID != value))
				{
					this._roleID = value;
				}
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
				if ((this._resourceID != value))
				{
					this._resourceID = value;
				}
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_role_resourceTypeID", DbType="BigInt")]
		public System.Nullable<long> role_resourceTypeID
		{
			get
			{
				return this._role_resourceTypeID;
			}
			set
			{
				if ((this._role_resourceTypeID != value))
				{
					this._role_resourceTypeID = value;
				}
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_role_name", DbType="NVarChar(50)")]
		public string role_name
		{
			get
			{
				return this._role_name;
			}
			set
			{
				if ((this._role_name != value))
				{
					this._role_name = value;
				}
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_account", DbType="VarChar(12)")]
		public string user_account
		{
			get
			{
				return this._user_account;
			}
			set
			{
				if ((this._user_account != value))
				{
					this._user_account = value;
				}
			}
		}
        [DataMember]
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_groupName", DbType="NVarChar(250)")]
		public string user_groupName
		{
			get
			{
				return this._user_groupName;
			}
			set
			{
				if ((this._user_groupName != value))
				{
					this._user_groupName = value;
				}
			}
		}
        [DataMember]
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_resource_resourceType", DbType = "BigInt NOT NULL")]
        public long resource_resourceTypeID
        {
            get
            {
                return this._resource_resourceTypeID;
            }
            set
            {
                if ((this._resource_resourceTypeID != value))
                {
                    this._resource_resourceTypeID = value;
                }
            }
        }
    }
}
