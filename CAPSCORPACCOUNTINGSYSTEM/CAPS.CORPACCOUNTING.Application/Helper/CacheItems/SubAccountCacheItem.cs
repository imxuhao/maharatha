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
using CAPS.CORPACCOUNTING.Sessions;
using CAPS.CORPACCOUNTING.Configuration;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{

    /// <summary>
    /// SubAccount CacheItem Class
    /// </summary>
    [AutoMapFrom(typeof(SubAccountUnit))]
    public class SubAccountCacheItem
    {
        /// <summary> Gets or sets SubAccountId </summary>
        public long SubAccountId { get; set; }
        /// <summary> Gets or sets SubAccountNumber </summary>
        public string SubAccountNumber { get; set; }

        /// <summary> Gets or sets Caption </summary>
        public string Caption { get; set; }

        /// <summary> Gets or sets Description </summary>

        public string Description { get; set; }

        /// <summary> Gets or sets SearchNo </summary>
        public string SearchNo { get; set; }
    }

    public interface ISubAccountCache : IEntityCache<SubAccountCacheItem>
    {
        /// <summary>
        /// Get SubAccounts by key
        /// </summary>
        /// <param name="accountkey"></param>
        /// <returns></returns>
        Task<List<SubAccountCacheItem>> GetSubAccountCacheItemAsync(string accountkey);

    }

    public class SubAccountCache : EntityCache<SubAccountUnit, SubAccountCacheItem, long>, ISubAccountCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
        private readonly ISettingManager _settingManager;

        ITypedCache<int, SubAccountCacheItem> IEntityCache<SubAccountCacheItem, int>.InternalCache
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SubAccountCacheItem this[int id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SubAccountCacheItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SubAccountCacheItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public SubAccountCache(ICacheManager cacheManager, IRepository<SubAccountUnit, long> repository, CustomAppSession customAppSession, ISettingManager settingManager)
            : base(cacheManager, repository)
        {
            _customAppSession = customAppSession;
            _settingManager = settingManager;
        }

        public override void HandleEvent(EntityChangedEventData<SubAccountUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheSubAccountStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountKey, Convert.ToInt32(_customAppSession.TenantId)));
        }

        /// <summary>
        /// Get SubAccounts from Database
        /// </summary>
        /// <returns></returns>
        private async Task<List<SubAccountCacheItem>> GetSubAccountsFromDb()
        {
            var subaccounts = await Repository.GetAll()
                 .Select(u => new SubAccountCacheItem
                 {
                     Description = u.Description,
                     SubAccountId = u.Id,
                     Caption = u.Caption,
                     SubAccountNumber = u.SubAccountNumber,
                     SearchNo = u.SearchNo
                 }).OrderBy(p => p.SubAccountNumber).ToListAsync();
            return subaccounts;
        }

        /// <summary>
        /// Get SubAccounts
        /// </summary>
        /// <param name="subaccountkey"></param>
        /// <returns></returns>
        public async Task<List<SubAccountCacheItem>> GetSubAccountCacheItemAsync(string subaccountkey)
        {
            if (await _settingManager.GetSettingValueAsync<bool>(AppSettings.General.UseRedisCacheByDefault))
            {
                var subAccountCacheItem = await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheSubAccountStore).
                    GetAsync(subaccountkey, async () =>
                    {
                        var newCacheItem = new CacheItem(subaccountkey);
                        var subaccountList = await GetSubAccountsFromDb();
                        foreach (var subaccount in subaccountList)
                        {
                            newCacheItem.SubAccountCacheItemList.Add(subaccount);
                        }
                        return newCacheItem;
                    });
                return subAccountCacheItem.SubAccountCacheItemList.ToList();
            }
            return await GetSubAccountsFromDb();
        }
    }
}

