using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Common
{
    public interface IMayHaveAttachments
    {
        bool IsObjectAttached { get; set; }
    }
}
