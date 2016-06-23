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
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using System.Data.Entity;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{
    /// <summary>
    /// CustomerCustomer CacheItem Class
    /// </summary>
    [AutoMapFrom(typeof(CustomerUnit))]
    public class CustomerCacheItem
    {
        /// <summary> Gets or sets CustomerId </summary>
        public int CustomerId { get; set; }

        /// <summary> Gets or sets LastName </summary>
        public string LastName { get; set; }

        /// <summary> Gets or sets FirstName </summary>
        public string FirstName { get; set; }
        /// <summary> Gets or sets CustomerNumber </summary>
        public string CustomerNumber { get; set; }

        /// <summary> Gets or sets CustomerPayTermsId </summary>
        public int? CustomerPayTermsId { get; set; }

    }

    public interface ICustomerCache : IEntityCache<CustomerCacheItem>
    {
        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="customerkey"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CustomerCacheItem>> GetCustomersCacheItemAsync(string customerkey, AutoSearchInput input);

    }

    /// <summary>
    /// Implementing Icustomer Interface
    /// </summary>
    public class CustomerCache : EntityCache<CustomerUnit, CustomerCacheItem>, ICustomerCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
        public CustomerCache(ICacheManager cacheManager, IRepository<CustomerUnit> repository, CustomAppSession customAppSession)
            : base(cacheManager, repository)
        {

            _customAppSession = customAppSession;
        }
        public override void HandleEvent(EntityChangedEventData<CustomerUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheCustomerStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.CustomerKey, Convert.ToInt32(_customAppSession.TenantId), eventData.Entity.OrganizationUnitId));
        }

        private async Task<List<CustomerCacheItem>> GetCustomersFromDb(AutoSearchInput input)
        {

            var query = from customers in Repository.GetAll()
                        select  customers;

            return await query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                            .Select(u => new CustomerCacheItem
                            {
                                CustomerId = u.Id,
                                LastName = u.LastName,
                                FirstName = u.FirstName,
                                CustomerNumber = u.CustomerNumber,
                                CustomerPayTermsId=u.CustomerPayTermsId
                            }).ToListAsync();

        }

        public async Task<List<CustomerCacheItem>> GetCustomersCacheItemAsync(string customerkey, AutoSearchInput input)
        {
            var cacheItem =
                await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheCustomerStore)
                        .GetAsync(customerkey, async () =>
                        {
                            var newCacheItem = new CacheItem(customerkey);
                            var customerList = await GetCustomersFromDb(input);
                            foreach (var customers in customerList)
                            {
                                newCacheItem.CustomerCacheItemList.Add(customers);
                            }
                            return newCacheItem;
                        });
            return cacheItem.CustomerCacheItemList.ToList();
        }

    }
}
