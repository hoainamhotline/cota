using SYS.ExtentionBase;
using SYS;

namespace Extention.LOG.Plugin
{
    public class After_SYS_Business_UserGroupBus_update_10 : PluginBase, IPlugin
    {
        public void run(object sender, object[] pram)
        {
            Log.add("Sửa nhóm người dùng " + (sender as SYS.ExtentionBase.IBusiness).getName());
        }
    }
}
