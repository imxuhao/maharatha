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
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Helpers.CacheItems
{

    /// <summary>
    /// Vendor CacheItem Class
    /// </summary>
    [AutoMapFrom(typeof(JobUnit))]
    public class DivisionCacheItem
    {
        /// <summary> Gets or sets LastName </summary>
        public int JobId { get; set; }
        /// <summary> Gets or sets LastName </summary>
        public string JobNumber { get; set; }

        /// <summary> Gets or sets FirstName </summary>
        public string Caption { get; set; }

        /// <summary> Gets or sets IsDivision </summary>
        public bool IsDivision { get; set; }

        /// <summary> Gets or sets IsDivision </summary>
        public ProjectStatus? TypeOfJobStatusId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsCorporateDefault field. </summary>
        public virtual bool IsCorporateDefault { get; set; }

        /// <summary>Gets or sets the ChartOfAccountId field. </summary>
        public virtual int ChartOfAccountId { get; set; }
    }

    public interface IDivisionCache : IEntityCache<DivisionCacheItem>
    {
        //Task<List<DivisionCacheItem>> GetDivisionList(AutoSearchInput input);

        Task<List<DivisionCacheItem>> GetDivisionCacheItemAsync(string divisionkey);

    }

    public class DivisionCache : EntityCache<JobUnit, DivisionCacheItem>, IDivisionCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;

        private readonly ISettingManager _settingManager;
        public DivisionCache(ICacheManager cacheManager, IRepository<JobUnit> repository, CustomAppSession customAppSession, ISettingManager settingManager)
            : base(cacheManager, repository)
        {
            _customAppSession = customAppSession;
            _settingManager = settingManager;
        }

        public override void HandleEvent(EntityChangedEventData<JobUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheDivisionStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId)));
        }

        private async Task<List<DivisionCacheItem>> GetDivisionsFromDb()
        {
            var divisions = await Repository.GetAll()
                 .Select(u => new DivisionCacheItem
                 {
                     JobNumber = u.JobNumber,
                     JobId = u.Id,
                     Caption = u.Caption,
                     TypeOfJobStatusId = u.TypeOfJobStatusId,
                     IsDivision = u.IsDivision,
                     OrganizationUnitId = u.OrganizationUnitId,
                     IsCorporateDefault=u.IsCorporateDefault,
                     ChartOfAccountId=u.ChartOfAccountId

                 }).ToListAsync();
            return divisions;
        }

        public async Task<List<DivisionCacheItem>> GetDivisionCacheItemAsync(string divisionkey)
        {
            if (await _settingManager.GetSettingValueAsync<bool>(AppSettings.General.UseRedisCacheByDefault))
            {
                var cacheDivisionList =
                    await
                        CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheDivisionStore)
                            .GetAsync(divisionkey, async () =>
                            {
                                var newCacheItem = new CacheItem(divisionkey);
                                var divList = await GetDivisionsFromDb();
                                foreach (var div in divList)
                                {
                                    newCacheItem.DivisionCacheItemList.Add(div);
                                }
                                return newCacheItem;
                            });
                return cacheDivisionList.DivisionCacheItemList.ToList();
            }
            return await GetDivisionsFromDb();
        }
    }
}

