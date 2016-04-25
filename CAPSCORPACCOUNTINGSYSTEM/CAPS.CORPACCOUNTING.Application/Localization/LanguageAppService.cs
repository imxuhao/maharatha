using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Localization;
using Abp.UI;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Localization.Dto;
using System.Data.Entity;
using Abp.Domain.Uow;

namespace CAPS.CORPACCOUNTING.Localization
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Languages)]
    public class LanguageAppService : CORPACCOUNTINGAppServiceBase, ILanguageAppService
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;
        private readonly IRepository<ApplicationLanguage> _languageRepository;
        private  IRepository<CustomLanguageTextsUnit, long> _customLanguageTextsUnitrepository;
        private readonly CustomLanguageTextsUnitManager _customLanguageTextsUnitManager;
        public LanguageAppService(
            IApplicationLanguageManager applicationLanguageManager,
            IApplicationLanguageTextManager applicationLanguageTextManager,
            IRepository<ApplicationLanguage> languageRepository,
            IRepository<CustomLanguageTextsUnit, long> customLanguageTextsUnitrepository,
            CustomLanguageTextsUnitManager customLanguageTextsUnitManager)
        {
            _applicationLanguageManager = applicationLanguageManager;
            _languageRepository = languageRepository;
            _applicationLanguageTextManager = applicationLanguageTextManager;
            _customLanguageTextsUnitrepository = customLanguageTextsUnitrepository;
            _customLanguageTextsUnitManager=customLanguageTextsUnitManager;
    }

        public async Task<GetLanguagesOutput> GetLanguages()
        {
            var languages = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).OrderBy(l => l.DisplayName);
            var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);

            return new GetLanguagesOutput(
                languages.MapTo<List<ApplicationLanguageListDto>>(),
                defaultLanguage == null ? null : defaultLanguage.Name
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Languages_Create, AppPermissions.Pages_Administration_Languages_Edit)]
        public async Task<GetLanguageForEditOutput> GetLanguageForEdit(NullableIdInput input)
        {
            ApplicationLanguage language = null;
            if (input.Id.HasValue)
            {
                language = await _languageRepository.GetAsync(input.Id.Value);
            }

            var output = new GetLanguageForEditOutput();

            //Language
            output.Language = language != null
                ? language.MapTo<ApplicationLanguageEditDto>()
                : new ApplicationLanguageEditDto();

            //Language names
            output.LanguageNames = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .OrderBy(c => c.DisplayName)
                .Select(c => new ComboboxItemDto(c.Name, c.DisplayName + " (" + c.Name + ")") { IsSelected = output.Language.Name == c.Name })
                .ToList();

            //Flags
            output.Flags = FamFamFamFlagsHelper
                .FlagClassNames
                .OrderBy(f => f)
                .Select(f => new ComboboxItemDto(f, FamFamFamFlagsHelper.GetCountryCode(f)) { IsSelected = output.Language.Icon == f})
                .ToList();

            return output;
        }

        public async Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input)
        {
            if (input.Language.Id.HasValue)
            {
                await UpdateLanguageAsync(input);
            }
            else
            {
                await CreateLanguageAsync(input);
            }
        }

        public async Task DeleteLanguage(IdInput input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            await _applicationLanguageManager.RemoveAsync(AbpSession.TenantId, language.Name);
        }

        public async Task SetDefaultLanguage(SetDefaultLanguageInput input)
        {
            await _applicationLanguageManager.SetDefaultLanguageAsync(
                AbpSession.TenantId,
                GetCultureInfoByChecking(input.Name).Name
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Languages_ChangeTexts)]
        public async Task<PagedResultOutput<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input)
        {
            /* Note: This method is used by SPA without paging, MPA with paging.
             * So, it can both usable with paging or not */

            //Normalize base language name
            if (input.BaseLanguageName.IsNullOrEmpty())
            {
                var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
                if (defaultLanguage == null)
                {
                    defaultLanguage = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).FirstOrDefault();
                    if (defaultLanguage == null)
                    {
                        throw new ApplicationException("No language found in the application!");
                    }
                }

                input.BaseLanguageName = defaultLanguage.Name;
            }

            var source = LocalizationManager.GetSource(input.SourceName);
            var baseCulture = CultureInfo.GetCultureInfo(input.BaseLanguageName);
            var targetCulture = CultureInfo.GetCultureInfo(input.TargetLanguageName);
            var res =await _customLanguageTextsUnitrepository.GetAllListAsync();

            var querry = from lang in source.GetAllStrings()
                         join customlang in res on lang.Name equals customlang.Key
                         into templang
                         from custlanguage in templang.DefaultIfEmpty()
                         select new { lang, custlanguage };

            var languageTexts = querry
                .Select(localizedString => new LanguageTextListDto
                {
                    Key = localizedString.lang.Name,
                    BaseValue = _applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, baseCulture, localizedString.lang.Name),
                    TargetValue = _applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, targetCulture, localizedString.lang.Name, false),
                    RegularExpression = !ReferenceEquals(localizedString.custlanguage,null)?localizedString.custlanguage.RegularExpression:"",
                    IsActive = !ReferenceEquals(localizedString.custlanguage, null) ? localizedString.custlanguage.IsActive:false,
                    IsMandatory = !ReferenceEquals(localizedString.custlanguage, null) ? localizedString.custlanguage.IsMandatory : false,
                    OrganizationUnitId = !ReferenceEquals(localizedString.custlanguage, null) ? localizedString.custlanguage.OrganizationUnitId : null
                })
                .AsQueryable();

            //Filters
            if (input.TargetValueFilter == "EMPTY")
            {
                languageTexts = languageTexts.Where(s => s.TargetValue.IsNullOrEmpty());
            }

            if (!input.FilterText.IsNullOrEmpty())
            {
                languageTexts = languageTexts.Where(
                    l => (l.Key != null && l.Key.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                         (l.BaseValue != null && l.BaseValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                         (l.TargetValue != null && l.TargetValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    );
            }

            var totalCount = languageTexts.Count();

            //Ordering
            if (!input.Sorting.IsNullOrEmpty())
            {
                languageTexts = languageTexts.OrderBy(input.Sorting);
            }

            //Paging
            if (input.SkipCount > 0)
            {
                languageTexts = languageTexts.Skip(input.SkipCount);
            }

            if (input.MaxResultCount > 0)
            {
                languageTexts = languageTexts.Take(input.MaxResultCount);
            }

            return new PagedResultOutput<LanguageTextListDto>(
                totalCount,
                languageTexts.ToList()
                );
        }

        [UnitOfWork]
        public async Task UpdateLanguageText(UpdateLanguageTextInput input)
        {
            var culture = GetCultureInfoByChecking(input.LanguageName);
            var source = LocalizationManager.GetSource(input.SourceName);
            await _applicationLanguageTextManager.UpdateStringAsync(AbpSession.TenantId, source.Name, culture, input.Key, input.Value);
            await _customLanguageTextsUnitManager.UpdateAsync(tenantId:AbpSession.TenantId, key:input.Key,regularexpression:input.RegularExpression,
                isactive:input.IsActive,ismandatory:input.IsMandatory,organizationunitid:input.OrganizationUnitId);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Languages_Create)]
        protected virtual async Task CreateLanguageAsync(CreateOrUpdateLanguageInput input)
        {
            var culture = GetCultureInfoByChecking(input.Language.Name);

            await CheckLanguageIfAlreadyExists(culture.Name);

            await _applicationLanguageManager.AddAsync(
                new ApplicationLanguage(
                    AbpSession.TenantId,
                    culture.Name,
                    culture.DisplayName,
                    input.Language.Icon
                    )
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Languages_Edit)]
        protected virtual async Task UpdateLanguageAsync(CreateOrUpdateLanguageInput input)
        {
            Debug.Assert(input.Language.Id != null, "input.Language.Id != null");

            var culture = GetCultureInfoByChecking(input.Language.Name);

            await CheckLanguageIfAlreadyExists(culture.Name, input.Language.Id.Value);

            var language = await _languageRepository.GetAsync(input.Language.Id.Value);

            language.Name = culture.Name;
            language.DisplayName = culture.DisplayName;
            language.Icon = input.Language.Icon;

            await _applicationLanguageManager.UpdateAsync(AbpSession.TenantId, language);
        }

        private CultureInfo GetCultureInfoByChecking(string name)
        {
            try
            {
                return CultureInfo.GetCultureInfo(name);
            }
            catch (CultureNotFoundException ex)
            {
                Logger.Warn(ex.ToString(), ex);
                throw new UserFriendlyException(L("InvlalidLanguageCode"));
            }
        }
        
        private async Task CheckLanguageIfAlreadyExists(string languageName, int? expectedId = null)
        {
            var existingLanguage = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                .FirstOrDefault(l => l.Name == languageName);

            if (existingLanguage == null)
            {
                return;
            }

            if (expectedId != null && existingLanguage.Id == expectedId.Value)
            {
                return;
            }

            throw new UserFriendlyException(L("ThisLanguageAlreadyExists"));
        }
    }
}