using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// Used to cache Accounts.
    /// </summary>

    [Serializable]
    class AccountingCacheItem

    {
        public const string CacheStoreName = "SUMITAccountCache";

        public static TimeSpan CacheExpireTime { get; private set; }
    }
}
