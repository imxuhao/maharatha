using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{
    /// <summary>
    /// Employee Cache Items
    /// </summary>
    [AutoMapFrom(typeof(EmployeeUnit))]
    public class EmployeeCacheItem
    {
        /// <summary> Gets or sets EmployeeId </summary>
        public int EmployeeId { get; set; }
        /// <summary> Gets or sets LastName </summary>
        public string LastName { get; set; }
        /// <summary> Gets or sets FirstName </summary>
        public string FirstName { get; set; }
        /// <summary> Gets or sets IsProducer </summary>
        public bool IsProducer { get; set; }
        /// <summary> Gets or sets IsDirector </summary>
        public bool IsDirector { get; set; }
        /// <summary> Gets or sets IsSetDesigner </summary>
        public bool IsSetDesigner { get; set; }
        /// <summary> Gets or sets IsArtDirector </summary>
        public bool IsArtDirector { get; set; }
    }


    public interface IEmployeeCache : IEntityCache<EmployeeUnit>
    {
        Task<List<EmployeeCacheItem>> GetEmployeeCacheItemAsync(string accountkey);

    }

    public class EmployeeCache : EntityCache<EmployeeUnit, EmployeeCacheItem>, IEmployeeCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;

        private readonly ISettingManager _settingManager;

        ITypedCache<int, EmployeeUnit> IEntityCache<EmployeeUnit, int>.InternalCache
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        EmployeeUnit IEntityCache<EmployeeUnit, int>.this[int id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public EmployeeCache(ICacheManager cacheManager, IRepository<EmployeeUnit> repository, CustomAppSession customAppSession, ISettingManager settingManager)
            : base(cacheManager, repository)
        {
            _customAppSession = customAppSession;
            _settingManager = settingManager;
        }

        public override void HandleEvent(EntityChangedEventData<EmployeeUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheEmployeeStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.EmployeeKey, Convert.ToInt32(_customAppSession.TenantId)));
        }
        private async Task<List<EmployeeCacheItem>> GetEmployeesFromDb()
        {
            var employees = await Repository.GetAll()
                 .Select(u => new EmployeeCacheItem
                 {
                     LastName = u.LastName,
                     EmployeeId = u.Id,
                     FirstName = u.FirstName
                 }).OrderBy(p => p.LastName).ToListAsync();
            return employees;
        }

        EmployeeUnit IEntityCache<EmployeeUnit, int>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<EmployeeUnit> IEntityCache<EmployeeUnit, int>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<EmployeeCacheItem>> GetEmployeeCacheItemAsync(string employeekey)
        {
            if (await _settingManager.GetSettingValueAsync<bool>(AppSettings.General.UseRedisCacheByDefault))
            {
                var cacheEmployeeList =
                    await
                        CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheEmployeeStore)
                            .GetAsync(employeekey, async () =>
                            {
                                var newCacheItem = new CacheItem(employeekey);
                                var divList = await GetEmployeesFromDb();
                                foreach (var emp in divList)
                                {
                                    newCacheItem.EmployeeItemList.Add(emp);
                                }
                                return newCacheItem;
                            });
                return cacheEmployeeList.EmployeeItemList.ToList();
            }
            return await GetEmployeesFromDb();
        }
    }

}
