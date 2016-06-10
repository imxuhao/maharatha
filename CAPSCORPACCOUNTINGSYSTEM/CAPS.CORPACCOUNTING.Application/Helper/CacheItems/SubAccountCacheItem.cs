using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using System.Data.Entity;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{

    /// <summary>
    /// Vendor CacheItem Class
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
        Task<CacheItem> GetSubAccountCacheItemAsync(string accountkey, AutoSearchInput input);

    }

    public class SubAccountCache : EntityCache<SubAccountUnit, SubAccountCacheItem, long>, ISubAccountCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;

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

        public SubAccountCache(ICacheManager cacheManager, IRepository<SubAccountUnit, long> repository, CustomAppSession customAppSession)
            : base(cacheManager, repository)
        {

            _customAppSession = customAppSession;
        }
        public override void HandleEvent(EntityChangedEventData<SubAccountUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheSubAccountStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountKey, Convert.ToInt32(_customAppSession.TenantId), eventData.Entity.OrganizationUnitId));
        }
       

        private async Task<List<SubAccountCacheItem>> GetSubAccountsFromDb(AutoSearchInput input)
        {
            var subaccounts = await Repository.GetAll()
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .Select(u => new SubAccountCacheItem { Description = u.Description, SubAccountId = u.Id, Caption = u.Caption,
                     SubAccountNumber = u.SubAccountNumber,SearchNo = u.SearchNo}).ToListAsync();
            return subaccounts;
        }
        
        /// <summary>
        /// Get SubAccounts
        /// </summary>
        /// <param name="subaccountkey"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CacheItem> GetSubAccountCacheItemAsync(string subaccountkey, AutoSearchInput input)
        {
            return await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheSubAccountStore).GetAsync(subaccountkey, async () =>
            {
                var newCacheItem = new CacheItem(subaccountkey);
                var subaccountList = await GetSubAccountsFromDb(input);
                foreach (var subaccount in subaccountList)
                {
                    newCacheItem.SubAccountCacheItemList.Add(subaccount);
                }
                return newCacheItem;
            });
        }

    }
}

