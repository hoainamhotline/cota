using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYS.Dataclass
{
    public interface IResource
    {
        long ID{get;set;}
        long resourceTypeID { get; set; }
        System.Nullable<long> parentID { get; set; }
    }
}
