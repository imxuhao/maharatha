using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using System.Data.Entity;
using Abp.Configuration;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{

    /// <summary>
    /// SubAccountRestriction CacheItem Class
    /// </summary>
    [AutoMapFrom(typeof(SubAccountRestrictionUnit))]
    public class SubAccountRestrictionCacheItem
    {
        /// <summary> Gets or sets SubAccountId </summary>
        public long SubAccountId { get; set; }

        /// <summary> Gets or sets AccountId </summary>
        public long AccountId { get; set; }

        /// <summary> Gets or sets IsActive </summary>
        public bool IsActive { get; set; }

        public long SubAccountRestrictionId { get; set; }

    }

    public interface ISubAccountRestrictionCache : IEntityCache<SubAccountRestrictionCacheItem>
    {
        /// <summary>
        /// Get SubAccounts by key
        /// </summary>
        /// <param name="accountkey"></param>
        /// <returns></returns>
        Task<List<SubAccountRestrictionCacheItem>> GetSubAccountRestrictionCacheItemAsync(string accountkey);

    }
    /// <summary>
    /// Implementing Cache for SubAccountRestriction.
    /// </summary>
    public class SubAccountRestrictionCache : EntityCache<SubAccountRestrictionUnit, SubAccountRestrictionCacheItem, long>, ISubAccountRestrictionCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
        private readonly ISettingManager _settingManager;

        #region Auto Implemented Properties
        ITypedCache<int, SubAccountRestrictionCacheItem> IEntityCache<SubAccountRestrictionCacheItem, int>.InternalCache
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SubAccountRestrictionCacheItem this[int id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public SubAccountRestrictionCacheItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SubAccountRestrictionCacheItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        public SubAccountRestrictionCache(ICacheManager cacheManager, IRepository<SubAccountRestrictionUnit, long> repository, CustomAppSession customAppSession, ISettingManager settingManager)
            : base(cacheManager, repository)
        {
            _customAppSession = customAppSession;
            _settingManager = settingManager;
        }

        public override void HandleEvent(EntityChangedEventData<SubAccountRestrictionUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheSubAccountRestrictionStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountRestrictionKey, Convert.ToInt32(_customAppSession.TenantId)));
        }

        /// <summary>
        /// Get SubAccounts from Database
        /// </summary>
        /// <returns></returns>
        private async Task<List<SubAccountRestrictionCacheItem>> GetSubAccountRestrictionsFromDb()
        {
            var subaccountRestrictions = await Repository.GetAll()
                 .Select(u => new SubAccountRestrictionCacheItem { AccountId = u.AccountId, SubAccountId = u.SubAccountId, IsActive = u.IsActive, SubAccountRestrictionId = u.Id }).ToListAsync();
            return subaccountRestrictions;
        }

        /// <summary>
        /// Get SubAccounts
        /// </summary>
        /// <param name="subaccountkey"></param>
        /// <returns></returns>
        public async Task<List<SubAccountRestrictionCacheItem>> GetSubAccountRestrictionCacheItemAsync(
            string subaccountkey)
        {
            if (await _settingManager.GetSettingValueAsync<bool>(AppSettings.General.UseRedisCacheByDefault))
            {
                var cacheSubaccountRestrictionList =
                    await
                        CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheSubAccountRestrictionStore)
                            .GetAsync(subaccountkey, async () =>
                            {
                                var newCacheItem = new CacheItem(subaccountkey);
                                var subaccountRestrictionsList = await GetSubAccountRestrictionsFromDb();
                                foreach (var subaccountRestrictions in subaccountRestrictionsList)
                                {
                                    newCacheItem.SubAccountRestrictionCacheItemList.Add(subaccountRestrictions);
                                }
                                return newCacheItem;
                            });
                return cacheSubaccountRestrictionList.SubAccountRestrictionCacheItemList.ToList();
            }
            return await GetSubAccountRestrictionsFromDb();
        }
    }
}

