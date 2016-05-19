using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Helpers
{
    [Serializable]
    public class DivisionCacheItem
    {
        /// <summary>
        /// The cache name.
        /// </summary>
        public const string CacheStoreName = "SUMITDIVISION";
        public string DivisionKey { get; set; }

        public HashSet<NameValueDto> DivisionList { get; set; }

        public static TimeSpan CacheExpireTime { get; private set; }
        static DivisionCacheItem()
        {
            CacheExpireTime = TimeSpan.FromMinutes(20);
        }

        public DivisionCacheItem()
        {
           // DivisionList = new List<NameValueDto>();
            DivisionList = new HashSet<NameValueDto>();
        }
        public DivisionCacheItem(string divisionkey)
            : this()
        {
            DivisionKey = divisionkey;
        }
    }
}
