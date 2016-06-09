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
    }

    public interface IDivisionCache : IEntityCache<DivisionCacheItem>
    {
        //Task<List<DivisionCacheItem>> GetDivisionList(AutoSearchInput input);

        Task<CacheItem> GetDivisionCacheItemAsync(string divisionkey, AutoSearchInput input);

    }

    public class DivisionCache : EntityCache<JobUnit, DivisionCacheItem>, IDivisionCache, ITransientDependency
    {

        private readonly CustomAppSession _customAppSession;
        public DivisionCache(ICacheManager cacheManager, IRepository<JobUnit> repository, CustomAppSession customAppSession)
            : base(cacheManager, repository)
        {

            _customAppSession = customAppSession;
        }
        public override void HandleEvent(EntityChangedEventData<JobUnit> eventData)
        {
            CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheDivisionStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId), eventData.Entity.OrganizationUnitId));
        }



      

        private async Task<List<DivisionCacheItem>> GetDivisionsFromDb(AutoSearchInput input)
        {
            var divisions = await Repository.GetAll()
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .Select(u => new DivisionCacheItem { JobNumber = u.JobNumber, JobId = u.Id , Caption = u.Caption,
                     TypeOfJobStatusId = u.TypeOfJobStatusId,IsDivision = u.IsDivision}).ToListAsync();
            return divisions;
        }

        public async Task<CacheItem> GetDivisionCacheItemAsync(string divisionkey, AutoSearchInput input)
        {
            return await CacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheDivisionStore).GetAsync(divisionkey, async () =>
            {
                var newCacheItem = new CacheItem(divisionkey);
                var divList = await GetDivisionsFromDb(input);
                foreach (var div in divList)
                {
                    newCacheItem.DivisionCacheItemList.Add(div);
                }
                return newCacheItem;
            });
        }

    }
}
