using Cota.Core.ExtentionBase;
using Cota.Core;

namespace Extention.LOG.Plugin
{
    public class After_SYS_Business_UserBus_update_10 : PluginBase, IPlugin
    {
        public void run(object sender, object[] pram)
        {
            Log.add("Sửa người dùng " + (sender as Cota.Core.ExtentionBase.IBusiness).getName());
        }
    }
}
