﻿using System;
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
        public bool IsCorporate { get; set; }
        
    }

    public interface IAccountCache : IEntityCache<AccountCacheItem>
    {
        Task<CacheItem> GetAccountCacheItemAsync(string accountkey, AutoSearchInput input);

    }

    public class AccountCache : EntityCache<AccountUnit, AccountCacheItem,long>, IAccountCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
        private readonly IRepository<CoaUnit> _coaRepository;

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

        public AccountCache(ICacheManager cacheManager, IRepository<AccountUnit,long> repository, CustomAppSession customAppSession, IRepository<CoaUnit> coaRepository)
            : base(cacheManager, repository)
        {

            _customAppSession = customAppSession;
            _coaRepository = coaRepository;
        }
        public override void HandleEvent(EntityChangedEventData<AccountUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheAccountStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId), eventData.Entity.OrganizationUnitId));
        }
        public AccountCacheItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountCacheItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get Accounts from Database
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        private async Task<List<AccountCacheItem>> GetAccountsFromDb(AutoSearchInput input)
        {
            var querry = from account in Repository.GetAll()
                           join coa in _coaRepository.GetAll() on account.ChartOfAccountId equals coa.Id
                           into coaunits from coaunit in coaunits.DefaultIfEmpty()
                           select new {account, coaunit };


            var result=await querry.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.account.OrganizationUnitId == input.OrganizationUnitId).ToListAsync();
            return result.Select(u => new AccountCacheItem
            {
                AccountNumber = u.account.AccountNumber,
                AccountId = u.account.Id,
                Caption = u.account.Caption,
                Description = u.account.Description,
                ChartOfAccountId = u.account.ChartOfAccountId,
                IsCorporate = u.coaunit.IsCorporate
            }).ToList();

        }

        /// <summary>
        /// Get Accounts
        /// </summary>
        /// <param name="subaccountkey"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
