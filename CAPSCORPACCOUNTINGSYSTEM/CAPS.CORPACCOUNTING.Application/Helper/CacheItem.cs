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

        public HashSet<SubAccountCacheItem> SubAccountCacheItemList { get; set; }

        public HashSet<SubAccountRestrictionCacheItem> SubAccountRestrictionCacheItemList { get; set; }

        public HashSet<BankAccountCacheItem> BankAccountCacheItemList { get; set; }

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
            int index = key.LastIndexOf("#", StringComparison.Ordinal);
            switch (key.Substring(index + 1))
            {
                case CacheKeyStores.EmployeeKey:
                    EmployeeItemList = new HashSet<EmployeeUnitDto>();
                    break;
                case CacheKeyStores.VendorKey:
                    VendorCacheItemList = new HashSet<VendorCacheItem>();
                    break;
                case CacheKeyStores.DivisionKey:
                    DivisionCacheItemList = new HashSet<DivisionCacheItem>();
                    break;
                case CacheKeyStores.AccountKey:
                    AccountCacheItemList = new HashSet<AccountCacheItem>();
                    break;
                case CacheKeyStores.SubAccountKey:
                    SubAccountCacheItemList = new HashSet<SubAccountCacheItem>();
                    break;
                case CacheKeyStores.SubAccountRestrictionKey:
                    SubAccountRestrictionCacheItemList = new HashSet<SubAccountRestrictionCacheItem>();
                    break;
                case CacheKeyStores.BankAccountKey:
                    BankAccountCacheItemList = new HashSet<BankAccountCacheItem>();
                    break;
                default:
                    ItemList = new HashSet<AutoFillDto>();
                    break;

            }
            Key = key;

        }
    }
}
