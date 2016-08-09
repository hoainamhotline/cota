using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYS.Dataclass
{
    public interface IGroup
    {
        string groupName { get; set; }
        string familyList { get; set; }
    }
}
