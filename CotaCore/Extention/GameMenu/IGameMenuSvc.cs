using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GameMenuLib;
using Extention.DOC.Dataclass;
using Extention.DOC;
namespace Extention.DOC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGameMenuSvc" in both code and config file together.
    [ServiceContract]
    public interface IGameMenuSvc
    {
        [OperationContract]
        List<GameInfo> GetMenuData();
        [OperationContract]
        string GetAccountName(string computerName);
        [OperationContract]
        string GetNotifi(string computerName);
        [OperationContract]
        void SentLOLGameInfo(string computerName, string gameID, string content,string secretkey);
    }
}
