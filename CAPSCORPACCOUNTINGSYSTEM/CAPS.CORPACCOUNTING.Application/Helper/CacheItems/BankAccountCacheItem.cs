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
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Sessions;


namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{
    /// <summary>
    /// BankAccountCacheItem Class
    /// </summary>
    [AutoMapFrom(typeof (BankAccountUnit))]
    public class BankAccountCacheItem
    {
        /// <summary> Gets or sets AccountId </summary>
        public long BankAccountId { get; set; }

        /// <summary> Gets or sets Caption </summary>
        public string BankAccountNumber { get; set; }

        /// <summary> Gets or sets Description </summary>
        public string Description { get; set; }

        /// <summary> Gets or sets AccountNumber </summary>
        public string BankAccountName { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
    /// <summary>
    /// BankAccount CacheInterface
    /// </summary>
    public interface IBankAccountCache : IEntityCache<BankAccountCacheItem>
    {
        /// <summary>
        /// Get BankAccounts
        /// </summary>
        /// <param name="bankAccountkey"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<BankAccountCacheItem>> GetBankAccountCacheItemAsync(string bankAccountkey, AutoSearchInput input);

    }
    /// <summary>
    /// BankAccount Implenentation
    /// </summary>
    public class BankAccountCache : EntityCache<BankAccountUnit, BankAccountCacheItem, long>, IBankAccountCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
       
        ITypedCache<int, BankAccountCacheItem> IEntityCache<BankAccountCacheItem, int>.InternalCache
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BankAccountCacheItem this[int id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BankAccountCache(ICacheManager cacheManager, IRepository<BankAccountUnit,long> repository, CustomAppSession customAppSession)
            : base(cacheManager, repository)
        {

            _customAppSession = customAppSession;
        }

        /// <summary>
        /// when BankAccount Entity is modified this event will fire
        /// </summary>
        /// <param name="eventData"></param>
        public override void HandleEvent(EntityChangedEventData<BankAccountUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheBankAccountStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.BankAccountKey, Convert.ToInt32(_customAppSession.TenantId), eventData.Entity.OrganizationUnitId));
        }
        public BankAccountCacheItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BankAccountCacheItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get BankAccounts from Database
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        private async Task<List<BankAccountCacheItem>> GetBankAccountsFromDb(AutoSearchInput input)
        {
            var bankAccounts = await Repository.GetAll()
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                  .Select(u => new BankAccountCacheItem
                  {
                      Description = u.Description,
                      BankAccountId = u.Id,
                      BankAccountNumber = u.BankAccountNumber,
                      BankAccountName = u.BankAccountName,
                      OrganizationUnitId = u.OrganizationUnitId
                  }).ToListAsync();
            return bankAccounts;

        }

        /// <summary>
        /// Get BankAccounts
        /// </summary>
        /// <param name="bankaccountkey"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<BankAccountCacheItem>> GetBankAccountCacheItemAsync(string bankaccountkey, AutoSearchInput input)
        {
           var cacheBankAccountList=  await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheBankAccountStore).GetAsync(bankaccountkey, async () =>
            {
                var newCacheItem = new CacheItem(bankaccountkey);
                var bankAccountList = await GetBankAccountsFromDb(input);
                foreach (var bankaccount in bankAccountList)
                {
                    newCacheItem.BankAccountCacheItemList.Add(bankaccount);
                }
                return newCacheItem;
            });
            return cacheBankAccountList.BankAccountCacheItemList.ToList();
        }
    }
}
