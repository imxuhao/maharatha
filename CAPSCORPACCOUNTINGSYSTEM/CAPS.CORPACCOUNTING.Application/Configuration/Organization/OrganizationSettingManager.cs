using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Logging;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Settings;

namespace CAPS.CORPACCOUNTING.Configuration.Organization
{
    public class OrganizationSettingManager : IOrganizationSettingManager, ITransientDependency
    {
        private readonly ITypedCache<string, Dictionary<string, SettingInfoExtended>> _organizationSettingCache;

        public const string OrganizationSettings = "SumitOrganizationSettingsCache";
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Reference to the setting store.
        /// </summary>
        public ISettingStore SettingStore { get; set; }
        private readonly IRepository<SettingExtended, long> _settingExtendedRepository;

        private readonly ISettingDefinitionManager _settingDefinitionManager;

        public OrganizationSettingManager(ICacheManager cacheManager, IAbpSession abpSession, ISettingDefinitionManager settingDefinitionManager, IRepository<SettingExtended, long> settingExtendedRepository)

        {
            AbpSession = abpSession;
            _settingDefinitionManager = settingDefinitionManager;
            _settingExtendedRepository = settingExtendedRepository;
            _organizationSettingCache = cacheManager.GetOrganizationSettingsCache();
            SettingStore = DefaultConfigSettingStore.Instance;

        }
        /// <summary>
        /// Change the Settings of Organization
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task ChangeSettingForOrganizationAsync(long organizationUnitId, string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value, organizationUnitId);
            await _organizationSettingCache.RemoveAsync(AbpSession.TenantId + "#" + organizationUnitId);

        }

        private async Task<SettingInfoExtended> InsertOrUpdateOrDeleteSettingValueAsync(string name, string value, long organizationUnitId)
        {
            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);
            var settingValue = await GetSettingValueForOrganizationOrNullAsync(organizationUnitId, name);
           
            if (settingValue != null)
            {
                if (settingValue.Value != value)
                    await _settingExtendedRepository.DeleteAsync(p => p.OrganizationUnitId == organizationUnitId && p.Name == settingValue.Name);
                return null;
            }
            

            //If it's not default value and not stored on database, then insert it
            if (settingDefinition.DefaultValue != value)
            {
                SettingExtended settingValue1 = new SettingExtended
                {
                    TenantId = AbpSession.TenantId,
                    UserId = AbpSession.GetUserId(),
                    OrganizationUnitId = organizationUnitId,
                    Name = name,
                    Value = value
                };
                await _settingExtendedRepository.InsertAsync(settingValue1);
                settingValue = settingValue1.MapTo<SettingInfoExtended>();
                return settingValue;
            }

            return settingValue;
        }


        public async Task<SettingInfoExtended> GetSettingValueForOrganizationOrNullAsync(long organizationUnitId, string name)
        {
            return (await GetReadOnlyOrganizationSettings(organizationUnitId)).GetOrDefault(name);
        }


        private async Task<ImmutableDictionary<string, SettingInfoExtended>> GetReadOnlyOrganizationSettings(long organizationUnitId)
        {
            var cachedDictionary = await GetOrganizationSettingsFromCache(organizationUnitId);
            lock (cachedDictionary)
            {
                return cachedDictionary.ToImmutableDictionary();
            }
        }

        private async Task<Dictionary<string, SettingInfoExtended>> GetOrganizationSettingsFromCache(long organizationUnitId)
        {
            string cacheKey = AbpSession.TenantId + "#"+organizationUnitId.ToString();
            return await _organizationSettingCache.GetAsync(
                cacheKey,
                async () =>
                {
                    var dictionaryList = new List<Dictionary<string, SettingInfoExtended>>();
                    var dictionary =new Dictionary<string, SettingInfoExtended>();

                    var settingValues = await GetAllListAsync(organizationUnitId);
                    foreach (var settingValue in settingValues)
                    {
                        dictionary[settingValue.Name] = settingValue;
                    }

                    return dictionary;
                });
        }
        public async Task<List<SettingInfoExtended>> GetAllListAsync(long organizationUnitId)
        {

            var results = await _settingExtendedRepository.GetAll().Where(p => p.OrganizationUnitId == organizationUnitId).ToListAsync();
            return new List<SettingInfoExtended>(results.Select(item =>
            {
                var dto = item.MapTo<SettingInfoExtended>();
                return dto;
            }).ToList());



        }
        /// <summary>
        /// Get the SettingValue of Organization
        /// </summary>
        /// <param name="OrganizationUnitId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<string> GetSettingValueForOrganization(long OrganizationUnitId, string name)
        {
            return await GetSettingValueInternalAsync(OrganizationUnitId, name);
        }

        private async Task<string> GetSettingValueInternalAsync(long OrganizationUnitId, string name)
        {
            //this will get the default settings
            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);

            //will get Modified Settings
            var settingValue = await GetSettingValueForOrganizationOrNullAsync(OrganizationUnitId, name);
            if (settingValue != null)
            {
                return settingValue.Value;
            }
            //Not defined, get default value
            return settingDefinition.DefaultValue;
        }

    }
}
