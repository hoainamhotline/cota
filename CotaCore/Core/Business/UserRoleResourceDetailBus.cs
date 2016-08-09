using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.Text;
using Cota.Core;
using Cota.Core.Dataclass;
using Cota.Core.ExtentionBase;
namespace Cota.Core.Business
{
    /// <summary>
    /// Class phục vụ cho UserRoleResourceBus
    /// </summary>
    public sealed class UserRoleResourceDetailBus : Dataclass.UserRoleResourceDetail
    {
        private string _resource_resourceName;
        [DataMember]
        public string resource_resourceName
        {
            set {
                this._resource_resourceName = value;
            }
            get {
                if (_resource_resourceName == null && base.resourceID != 0)
                {
                    object a = OtherResource.selectByID(base.resourceID);
                    this._resource_resourceName = (OtherResource.selectByID(base.resourceID) as IBusiness).getName();
                }
                return this._resource_resourceName;
            }
        }

        private string _resource_resourceTypeName;
        [DataMember]
        public string resource_resourceTypeName
        {
            set
            {
                this._resource_resourceTypeName = value;
            }
            get
            {
                if (_resource_resourceTypeName == null)
                {
                    _resource_resourceTypeName = (Init.getConfigData(base.resource_resourceTypeID) as IInitConfig).RESOURCE_TYPE_NAME;
                }
                return this._resource_resourceTypeName;
            }
        }
    }
}
