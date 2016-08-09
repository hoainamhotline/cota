using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYS
{
    public partial class Init
    {
        public void LOG_Init()
        {

            Plugin.reg("After_SYS_Business_UserBus_delete", new Extention.LOG.Plugin.After_SYS_Business_UserBus_delete_10(),10);
            Plugin.reg("After_SYS_Business_UserBus_insert", new Extention.LOG.Plugin.After_SYS_Business_UserBus_insert_10(),10);
            Plugin.reg("After_SYS_Business_UserBus_update", new Extention.LOG.Plugin.After_SYS_Business_UserBus_update_10(), 10);
            
            Plugin.reg("After_SYS_Business_UserGroupBus_delete", new Extention.LOG.Plugin.After_SYS_Business_UserGroupBus_delete_10(),10);
            Plugin.reg("After_SYS_Business_UserGroupBus_insert", new Extention.LOG.Plugin.After_SYS_Business_UserGroupBus_insert_10(),10);
            Plugin.reg("After_SYS_Business_UserGroupBus_update", new Extention.LOG.Plugin.After_SYS_Business_UserGroupBus_update_10(), 10);
            
        }
    }
}
