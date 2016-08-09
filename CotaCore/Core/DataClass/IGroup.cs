using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cota.Core.Dataclass
{
    public interface IGroup
    {
        string groupName { get; set; }
        string familyList { get; set; }
    }
}
