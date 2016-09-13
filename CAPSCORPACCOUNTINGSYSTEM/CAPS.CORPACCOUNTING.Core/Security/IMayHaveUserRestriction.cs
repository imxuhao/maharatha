using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security
{
   public interface IMayHaveUserRestriction
    {
        long? UserId { get; set; }
    }
}
