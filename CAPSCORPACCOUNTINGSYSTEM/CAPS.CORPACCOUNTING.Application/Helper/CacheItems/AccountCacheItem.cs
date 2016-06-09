using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using System.Data.Entity;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Sessions;


namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{
    /// <summary>
    /// AccountCacheItem Class
    /// </summary>
    [AutoMapFrom(typeof (AccountUnit))]
    public class AccountCacheItem
    {
        /// <summary> Gets or sets AccountId </summary>
        public long AccountId { get; set; }

        /// <summary> Gets or sets Caption </summary>
        public string Caption { get; set; }

        /// <summary> Gets or sets Description </summary>
        public string Description { get; set; }

        /// <summary> Gets or sets AccountNumber </summary>
        public string AccountNumber { get; set; }

        /// <summary> Gets or sets TypeOfAccountId </summary>
        public int? TypeOfAccountId { get; set; }
        /// <summary> Gets or sets TypeOfAccountId </summary>
        public int? ChartOfAccountId { get; set; }

        
    }

    public interface IAccountCache : IEntityCache<AccountCacheItem>
    {
        Task<CacheItem> GetAccountCacheItemAsync(string accountkey, AutoSearchInput input);

    }

    public class AccountCache : EntityCache<AccountUnit, AccountCacheItem,long>, IAccountCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;

        ITypedCache<int, AccountCacheItem> IEntityCache<AccountCacheItem, int>.InternalCache
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AccountCacheItem this[int id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AccountCache(ICacheManager cacheManager, IRepository<AccountUnit,long> repository, CustomAppSession customAppSession)
            : base(cacheManager, repository)
        {

            _customAppSession = customAppSession;
        }
        public override void HandleEvent(EntityChangedEventData<AccountUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheVendorStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.VendorKey, Convert.ToInt32(_customAppSession.TenantId), eventData.Entity.OrganizationUnitId));
        }
        public AccountCacheItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountCacheItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<AccountCacheItem>> GetAccountsFromDb(AutoSearchInput input)
        {
            var accounts = await Repository.GetAll()
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .Select(u => new AccountCacheItem { AccountNumber = u.AccountNumber, AccountId = u.Id, Caption = u.Caption,Description = u.Description}).ToListAsync();
            return accounts;
        }

        public async Task<CacheItem> GetAccountCacheItemAsync(string accountkey, AutoSearchInput input)
        {
            return await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheAccountStore).GetAsync(accountkey, async () =>
            {
                var newCacheItem = new CacheItem(accountkey);
                var accountList = await GetAccountsFromDb(input);
                foreach (var account in accountList)
                {
                    newCacheItem.AccountCacheItemList.Add(account);
                }
                return newCacheItem;
            });
        }
    }
}
