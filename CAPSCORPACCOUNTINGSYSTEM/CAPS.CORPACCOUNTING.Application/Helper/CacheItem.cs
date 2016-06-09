using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
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

        public HashSet<AutoFillDto> ItemList { get; set; }

        public HashSet<VendorCacheItem> VendorCacheItemList { get; set; }

        public HashSet<DivisionCacheItem> DivisionCacheItemList { get; set; }

        public HashSet<AccountCacheItem> AccountCacheItemList { get; set; }

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
            if (key.Contains(CacheKeyStores.VendorKey))
                VendorCacheItemList = new HashSet<VendorCacheItem>();
            else
            if (key.Contains(CacheKeyStores.DivisionKey))
                DivisionCacheItemList = new HashSet<DivisionCacheItem>();
            if (key.Contains(CacheKeyStores.DivisionKey))
                AccountCacheItemList = new HashSet<AccountCacheItem>();
            
            else
                ItemList = new HashSet<AutoFillDto>();

            Key = key;

        }
    }
}
