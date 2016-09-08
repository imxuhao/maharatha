using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Authorization;
using AutoMapper;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Accounting
{

    /// <summary>
    /// Classification Application Service
    /// </summary>
    /// 
    [AbpAuthorize]
    public class ClassificationUnitAppService : CORPACCOUNTINGAppServiceBase, IClassificationUnitAppService
    {

        private readonly TypeOfAccountUnitManager _typeOfAccountUnitManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<TypeOfAccountUnit, int> _typeOfAccountUnitRepository;
        private readonly IRepository<TypeOfAccountClassificationUnit, short> _typeOfAccountClassificationUnitRepository;
        private readonly CustomAppSession _customAppSession;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeofaccountunitmanager"></param>
        /// <param name="typeofaccountunitrepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="typeOfAccountClassificationUnitRepository"></param>
        /// <param name="customAppSession"></param>
        public ClassificationUnitAppService(TypeOfAccountUnitManager typeofaccountunitmanager,
            IRepository<TypeOfAccountUnit, int> typeofaccountunitrepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<TypeOfAccountClassificationUnit, short> typeOfAccountClassificationUnitRepository,
            CustomAppSession customAppSession)
        {
            _typeOfAccountUnitManager = typeofaccountunitmanager;
            _unitOfWorkManager = unitOfWorkManager;
            _typeOfAccountUnitRepository = typeofaccountunitrepository;
            _customAppSession = customAppSession;
            _typeOfAccountClassificationUnitRepository = typeOfAccountClassificationUnitRepository;
        }

        /// <summary>
        /// Create the Classification.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_TypeofClassification_Create)]
        [UnitOfWork]
        public async Task<int> CreateClassificationUnit(CreateTypeOfAccountInputUnit input)
        {
            var typeOfAccountUnit = input.MapTo<TypeOfAccountUnit>();
            typeOfAccountUnit.IsEditable = true;
            typeOfAccountUnit.TenantId = AbpSession.TenantId;
            var typeOfAccountId = await _typeOfAccountUnitManager.CreateAsync(typeOfAccountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return typeOfAccountId;
        }


        /// <summary>
        /// Update the Classification based on TypeOfAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_TypeofClassification_Edit)]
        [UnitOfWork]
        public async Task UpdateClassificationUnit(UpdateTypeOfAccountInputUnit input)
        {
            var typeOfAccountUnit = await _typeOfAccountUnitRepository.GetAsync(input.TypeOfAccountId);
            Mapper.Map(input, typeOfAccountUnit);
            typeOfAccountUnit.TenantId = AbpSession.TenantId;
            await _typeOfAccountUnitManager.UpdateAsync(typeOfAccountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete the Classification based on TypeOfAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_TypeofClassification_Delete)]
        public async Task DeleteClassificationUnit(IdInput input)
        {
            await _typeOfAccountUnitManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// Get the list of all Classification and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_TypeofClassification)]
        public async Task<PagedResultOutput<TypeOfAccountUnitDto>> GetClassificationUnits(SearchInputDto input)
        {
            var query = from typeofaccount in _typeOfAccountUnitRepository.GetAll()
                        join typeofaccclassfication in _typeOfAccountClassificationUnitRepository.GetAll()
                        on typeofaccount.TypeOfAccountClassificationId equals typeofaccclassfication.Id
                        into typeofaccountss
                        from typeofaccounts in typeofaccountss.DefaultIfEmpty()
                        select new { typeofaccounts = typeofaccount, typeOfAccountClassification = typeofaccounts.Description };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("typeofaccounts.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<TypeOfAccountUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.typeofaccounts.MapTo<TypeOfAccountUnitDto>();
                dto.TypeOfAccountId = item.typeofaccounts.Id;
                dto.TypeOfAccountClassificationDesc = item.typeOfAccountClassification;
                dto.AllowDelete = item.typeofaccounts.IsEditable;
                dto.AllowEdit = item.typeofaccounts.IsEditable;
                return dto;
            }).ToList());
        }


        /// <summary>
        /// Get the Classification based on TypeOfAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<TypeOfAccountUnitDto> GetClassificationUnitById(IdInput input)
        {
            TypeOfAccountUnit typeOfAccountUnit = await _typeOfAccountUnitRepository.GetAsync(input.Id);
            TypeOfAccountUnitDto result = typeOfAccountUnit.MapTo<TypeOfAccountUnitDto>();
            result.TypeOfAccountId = typeOfAccountUnit.Id;
            return result;
        }

        /// <summary>
        /// Get TypeOfAccountClassification List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfAccountClassificationList(AutoSearchInput input)
        {
            var accountClassificationList = await _typeOfAccountClassificationUnitRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                .OrderBy(u => u.Name)
                .ToListAsync();
            return accountClassificationList;
        }
    }
}
