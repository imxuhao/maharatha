using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Helpers
{
    [Serializable]
    public class CacheItem
    {
      
        /// <summary>
        /// The cache name.
        /// </summary>
      
        public string Key { get; set; }

        public HashSet<NameValueDto> ItemList { get; set; }

        /// <summary>
        /// Gets or Sets Employee List
        /// </summary>
        public HashSet<EmployeeUnitDto> EmployeeItemList { get; set; }

        public static TimeSpan CacheExpireTime { get; private set; }
        static CacheItem()
        {
            CacheExpireTime = TimeSpan.FromMinutes(20);
        }

        public CacheItem()
        {
        }
        public CacheItem(string key)
        {
            if (key.Contains(CacheKeyStores.EmployeeKey))
                EmployeeItemList = new HashSet<EmployeeUnitDto>();
            else
                ItemList = new HashSet<NameValueDto>();
            Key = key;

        }
    }
}
