using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using AutoMapper;
using System.Collections.Generic;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// SubAccount AppService
    /// </summary>
    public class SubAccountUnitAppService : CORPACCOUNTINGServiceBase, ISubAccountUnitAppService
    {

        private readonly SubAccountUnitManager _subAccountUnitManager;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<SubAccountRestrictionUnit, long> _subAccountRestrictionUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly SubAccountRestrictionUnitManager _subAccountRestrictionUnitManager;
        private readonly IAccountCache _accountCache;
        private readonly ISubAccountRestrictionCache _subAccountRestrictionCache;
        private readonly CustomAppSession _customAppSession;
        public SubAccountUnitAppService(SubAccountUnitManager subAccountUnitManager, IRepository<SubAccountUnit, long> subAccountUnitRepository,
             IRepository<SubAccountRestrictionUnit, long> subAccountRestrictionUnitRepository, IRepository<AccountUnit, long> accountUnitRepository,
            SubAccountRestrictionUnitManager subAccountRestrictionUnitManager, IAccountCache accountCache, ISubAccountRestrictionCache subAccountRestrictionCache, CustomAppSession customAppSession)
        {
            _subAccountUnitManager = subAccountUnitManager;
            _subAccountRestrictionUnitRepository = subAccountRestrictionUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _subAccountRestrictionUnitManager = subAccountRestrictionUnitManager;
            _accountCache = accountCache;
            _subAccountRestrictionCache = subAccountRestrictionCache;
            _customAppSession = customAppSession;
            _subAccountUnitRepository = subAccountUnitRepository;
        }


        /// <summary>
        /// Create the Sub Account.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_SubAccounts_Create)]
        [UnitOfWork]
        public async Task<SubAccountUnitDto> CreateSubAccountUnit(CreateSubAccountUnitInput input)
        {
            var subAccountUnit = input.MapTo<SubAccountUnit>();
            long subAccountId = await _subAccountUnitManager.CreateAsync(subAccountUnit);

            if (!ReferenceEquals(input.SubAccountRestrictionList, null))
                await CreateorUpdateSubAccountRestrictions(input: input.SubAccountRestrictionList, id: subAccountId);
            await CurrentUnitOfWork.SaveChangesAsync();
            return subAccountUnit.MapTo<SubAccountUnitDto>();
        }

        /// <summary>
        ///  Update Sub Account.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_SubAccounts_Edit)]
        public async Task<SubAccountUnitDto> UpdateSubAccountUnit(UpdateSubAccountUnitInput input)
        {
            var subAccountUnit = await _subAccountUnitRepository.GetAsync(input.SubAccountId);
            Mapper.Map(input, subAccountUnit);
            await _subAccountUnitManager.UpdateAsync(subAccountUnit);

            if (!ReferenceEquals(input.SubAccountRestrictionList, null))
                await CreateorUpdateSubAccountRestrictions(input: input.SubAccountRestrictionList, id: input.SubAccountId);
            await CurrentUnitOfWork.SaveChangesAsync();
            return subAccountUnit.MapTo<SubAccountUnitDto>();
        }

        /// <summary>
        ///  delete Sub Account.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_SubAccounts_Delete)]
        public async Task DeleteSubAccountUnit(IdInput<long> input)
        {
            await _subAccountUnitManager.DeleteAsync(input);
        }


        /// <summary>
        /// Get the list of all Sub Accounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_SubAccounts)]
        public async Task<PagedResultOutput<SubAccountUnitDto>> GetSubAccountUnits(SearchInputDto input)
        {
            var subAccountUnitQuery = CreateSubAccountQuery(input);
            var resultCount = await subAccountUnitQuery.CountAsync();
            var results = await subAccountUnitQuery
                .AsNoTracking()
                .OrderBy(Helper.GetSort(SubAccountUnit.DefaultSortColumn + " " + "ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            var mapEnumResults = (from value in results
                                  select new SubAccountUnitDto
                                  {

                                      AccountingLayoutItemId = value.AccountingLayoutItemId,
                                      Caption = value.Caption,
                                      Description = value.Description,
                                      DisplaySequence = value.DisplaySequence,
                                      EntityId = value.EntityId,
                                      GroupCopyLabel = value.GroupCopyLabel,
                                      IsAccountSpecific = value.IsAccountSpecific,
                                      IsActive = value.IsActive,
                                      IsApproved = value.IsApproved,
                                      IsBudgetInclusive = value.IsBudgetInclusive,
                                      IsCorporateSubAccount = value.IsCorporateSubAccount,
                                      IsEnterable = value.IsEnterable,
                                      IsMandatory = value.IsMandatory,
                                      IsProjectSubAccount = value.IsProjectSubAccount,
                                      OrganizationUnitId = value.OrganizationUnitId,
                                      SearchNo = value.SearchNo,
                                      SearchOrder = value.SearchOrder,
                                      SubAccountId = value.SubAccountId,
                                      SubAccountNumber = value.SubAccountNumber,
                                      TypeOfInactiveStatusId = value.TypeOfInactiveStatusId,
                                      TypeOfInactiveStatus = value.TypeOfInactiveStatusId != null ? value.TypeOfInactiveStatusId.ToDisplayName() : "",
                                      TypeofSubAccount = value.TypeofSubAccountId.ToDisplayName(),
                                      TypeofSubAccountId = value.TypeofSubAccountId
                                  }).ToList();
            return new PagedResultOutput<SubAccountUnitDto>(resultCount, mapEnumResults);
        }

        /// <summary>
        /// Get Sub Account based on Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SubAccountUnitDto> GetSubAccountUnitsById(IdInput input)
        {
            var subAccountUnitItem = await _subAccountUnitRepository.GetAsync(input.Id);
            return subAccountUnitItem.MapTo<SubAccountUnitDto>();
        }

        private IQueryable<SubAccountUnitDto> CreateSubAccountQuery(SearchInputDto input)
        {

            var subAccountUnitQuery = from subAccount in _subAccountUnitRepository.GetAll()
                                      select new SubAccountUnitDto
                                      {
                                          AccountingLayoutItemId = subAccount.AccountingLayoutItemId,
                                          Caption = subAccount.Caption,
                                          Description = subAccount.Description,
                                          DisplaySequence = subAccount.DisplaySequence,
                                          EntityId = subAccount.EntityId,
                                          GroupCopyLabel = subAccount.GroupCopyLabel,
                                          IsAccountSpecific = subAccount.IsAccountSpecific,
                                          IsActive = subAccount.IsActive,
                                          IsApproved = subAccount.IsApproved,
                                          IsBudgetInclusive = subAccount.IsBudgetInclusive,
                                          IsCorporateSubAccount = subAccount.IsCorporateSubAccount,
                                          IsEnterable = subAccount.IsEnterable,
                                          IsMandatory = subAccount.IsMandatory,
                                          IsProjectSubAccount = subAccount.IsProjectSubAccount,
                                          OrganizationUnitId = subAccount.OrganizationUnitId,
                                          SearchNo = subAccount.SearchNo,
                                          SearchOrder = subAccount.SearchOrder,
                                          SubAccountId = subAccount.Id,
                                          SubAccountNumber = subAccount.SubAccountNumber,
                                          TypeOfInactiveStatusId = subAccount.TypeOfInactiveStatusId.Value,
                                          TypeofSubAccountId = subAccount.TypeofSubAccountId
                                      };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    subAccountUnitQuery = Helper.CreateFilters(subAccountUnitQuery, mapSearchFilters);
            }
            return subAccountUnitQuery;
        }

        /// <summary>
        /// Get SubAccountTypes
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofSubAccountList()
        {
            return EnumList.GetTypeofSubAccountList();
        }

        /// <summary>
        /// Get SubAccountRestrictions By SubAccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<SubAccountRestrictionUnitDto>> GetAccountRestrictionList(GetAccountRestrictionInput input)
        {
            var query = from subaccountrestriction in _subAccountRestrictionUnitRepository.GetAll()
                        join account in _accountUnitRepository.GetAll() on subaccountrestriction.AccountId equals account.Id
                        select new { subaccountrestriction, Caption = account.Caption, AccountNumber = account.AccountNumber, Description = account.Description };

            var subAccountRestrictionList = await query.Where(p => p.subaccountrestriction.SubAccountId == input.SubAccountId
            && p.subaccountrestriction.IsActive == true).ToListAsync();
            return subAccountRestrictionList.Select(item =>
            {
                var dto = item.subaccountrestriction.MapTo<SubAccountRestrictionUnitDto>();
                dto.SubAccountRestrictionId = item.subaccountrestriction.Id;
                dto.AccountNumber = item.AccountNumber;
                dto.Caption = item.Caption;
                dto.Description = item.Description;
                return dto;
            }).ToList();

        }


        /// <summary>
        /// Get SubAccountRestrictions By SubAccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<SubAccountRestrictionUnitDto>> GetAccountList(GetAccountRestrictionInput input)
        {
            var cacheItem = await _accountCache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId)));

            var subaccountRestrictioncacheItem = await _subAccountRestrictionCache.GetSubAccountRestrictionCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountRestrictionKey, Convert.ToInt32(_customAppSession.TenantId)));
            List<SubAccountRestrictionCacheItem> subaccountRestrictions = new List<SubAccountRestrictionCacheItem>();

            if (!ReferenceEquals(subaccountRestrictioncacheItem, null))
            {

                subaccountRestrictions = subaccountRestrictioncacheItem.ToList().Where(
                        p => p.IsActive == true && p.SubAccountId == input.SubAccountId.Value).ToList();
            }

            var result = cacheItem.ToList().Where(p => subaccountRestrictions.All(p2 => p2.AccountId != p.AccountId) && p.IsCorporate).ToList();

            return result.Select(item =>
            {
                var dto = new SubAccountRestrictionUnitDto();
                dto.AccountId = item.AccountId;
                dto.SubAccountId = input.SubAccountId.Value;
                dto.AccountNumber = item.AccountNumber;
                dto.SubAccountRestrictionId = 0;
                dto.Caption = item.Caption;
                dto.Description = item.Description;
                return dto;
            }).ToList();
        }

        /// <summary>
        /// Create or Update SubAccountRestrictions
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        private async Task CreateorUpdateSubAccountRestrictions(List<SubAccountRestrictionUnitInput> input, long id)
        {
            foreach (var subaccountrestriction in input)
            {
                if (subaccountrestriction.SubAccountRestrictionId == 0)
                {
                    var subAccountRestrictionUnit = await _subAccountRestrictionUnitRepository.FirstOrDefaultAsync(p => p.SubAccountId == id
                    && p.AccountId == subaccountrestriction.AccountId);
                    if (!ReferenceEquals(subAccountRestrictionUnit, null))
                    {
                        subAccountRestrictionUnit.IsActive = true;
                        Mapper.Map(subaccountrestriction, subAccountRestrictionUnit);
                        await _subAccountRestrictionUnitManager.UpdateAsync(subAccountRestrictionUnit);

                    }
                    else
                    {
                        var subAccountRestriction = subaccountrestriction.MapTo<SubAccountRestrictionUnit>();
                        subAccountRestriction.IsActive = true;
                        subAccountRestriction.SubAccountId = id;
                        await _subAccountRestrictionUnitManager.CreateAsync(subAccountRestriction);

                    }

                }
                else
                {
                    var subAccountRestrictionUnit = await _subAccountRestrictionUnitRepository.GetAsync(subaccountrestriction.SubAccountRestrictionId);
                    subAccountRestrictionUnit.IsActive = false;
                    subAccountRestrictionUnit.SubAccountId = id;
                    Mapper.Map(subaccountrestriction, subAccountRestrictionUnit);
                    await _subAccountRestrictionUnitManager.UpdateAsync(subAccountRestrictionUnit);


                }
            }

        }

    }
}
