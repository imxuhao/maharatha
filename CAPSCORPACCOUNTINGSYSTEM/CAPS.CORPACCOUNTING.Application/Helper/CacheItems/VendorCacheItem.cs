using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Masters;
using System.Data.Entity;
using Abp.Configuration;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{
    /// <summary>
    /// Vendor CacheItem Class
    /// </summary>
    [AutoMapFrom(typeof(VendorUnit))]
    public class VendorCacheItem
    {
        public int VendorId { get; set; }

        /// <summary> Gets or sets LastName </summary>
        public string LastName { get; set; }

        /// <summary> Gets or sets FirstName </summary>
        public string FirstName { get; set; }
        /// <summary> Gets or sets VendorNumber </summary>
        public string VendorNumber { get; set; }
        /// <summary> Gets or sets VendorAccountInfo </summary>
        public string VendorAccountInfo { get; set; }

        /// <summary> Gets or sets PaymentTermsId </summary>
        public int? PaymentTermsId { get; set; }

        /// <summary> Gets or sets TypeofVendorId </summary>
        public TypeofVendor TypeofVendorId { get; set; }

    }

    public interface IVendorCache : IEntityCache<VendorCacheItem>
    {
        Task<List<VendorCacheItem>> GetVendorsCacheItemAsync(string vendorkey);

    }

    public class VendorCache : EntityCache<VendorUnit, VendorCacheItem>, IVendorCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
        private readonly ISettingManager _settingManager;
        public VendorCache(ICacheManager cacheManager, IRepository<VendorUnit> repository, CustomAppSession customAppSession, ISettingManager settingManager)
            : base(cacheManager, repository)
        {
            _customAppSession = customAppSession;
            _settingManager = settingManager;
        }

        public override void HandleEvent(EntityChangedEventData<VendorUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheVendorStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.VendorKey, Convert.ToInt32(_customAppSession.TenantId)));
        }

        private async Task<List<VendorCacheItem>> GetVendorsFromDb()
        {
            var query = from vendors in Repository.GetAll()
                        select new { vendors };

            return await query.Select(u => new VendorCacheItem
            {
                VendorId = u.vendors.Id,
                LastName = u.vendors.LastName,
                FirstName = u.vendors.FirstName,
                VendorNumber = u.vendors.VendorNumber,
                VendorAccountInfo = u.vendors.VendorAccountInfo
            }).OrderBy(p=>p.LastName).ToListAsync();
        }

        public async Task<List<VendorCacheItem>> GetVendorsCacheItemAsync(string vendorkey)
        {
            if (await _settingManager.GetSettingValueAsync<bool>(AppSettings.General.UseRedisCacheByDefault))
            {
                var cacheItem =
                await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheVendorStore)
                        .GetAsync(vendorkey, async () =>
                        {
                            var newCacheItem = new CacheItem(vendorkey);
                            var vendorList = await GetVendorsFromDb();
                            foreach (var vendors in vendorList)
                            {
                                newCacheItem.VendorCacheItemList.Add(vendors);
                            }
                            return newCacheItem;
                        });
                return cacheItem.VendorCacheItemList.ToList();
            }
            return await GetVendorsFromDb();
        }

    }
}
