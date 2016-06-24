using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{

    [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs)] ///This is to ensure only logged in user has access to this module. We will improvise accordingly
    public class ProjectCoaUnitAppService : CORPACCOUNTINGServiceBase, IProjectCoaUnitAppService
    {
        private readonly CoaUnitManager _coaunitManager;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
      
        public ProjectCoaUnitAppService(CoaUnitManager coaunitManager, IRepository<CoaUnit> coaUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _coaunitManager = coaunitManager;
            _coaUnitRepository = coaUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Get the Records for Grid with paging and  sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<CoaUnitDto>> GetProjectCoaList(SearchInputDto input)
        {

            var query = from coa in _coaUnitRepository.GetAll()
                        join linkcoa in _coaUnitRepository.GetAll()
                        on coa.LinkChartOfAccountID equals linkcoa.Id
                        into tempCoa
                        from coaunit in tempCoa.DefaultIfEmpty()
                        where coa.IsCorporate == false
                        select new { Coa = coa, LinkChartOfAccountName = coaunit.Caption };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), item => item.Coa.OrganizationUnitId == input.OrganizationUnitId)
                .Where(p => p.Coa.IsCorporate == false);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Coa.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<CoaUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Coa.MapTo<CoaUnitDto>();
                dto.CoaId = item.Coa.Id;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// Creating ProjectCoa
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs_Create)]
        public async Task<CoaUnitDto> CreateProjectCoaUnit(CreateCoaUnitInput input)
        {
            var coaUnit = input.MapTo<CoaUnit>();
            await _coaunitManager.CreateAsync(coaUnit);
            coaUnit.OrganizationUnitId = input.OrganizationUnitId;
            coaUnit.IsCorporate = false;
            await CurrentUnitOfWork.SaveChangesAsync();
            return coaUnit.MapTo<CoaUnitDto>();
        }
        /// <summary>
        /// Updating ProjectCoa
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs_Edit)]
        public async Task<CoaUnitDto> UpdateProjectCoaUnit(UpdateCoaUnitInput input)
        {
            var coaUnit = await _coaUnitRepository.GetAsync(input.CoaId);

            #region Setting the values to be updated

            coaUnit.Caption = input.Caption;
            coaUnit.Description = input.Description;
            coaUnit.DisplaySequence = input.DisplaySequence;
            coaUnit.IsActive = input.IsActive;
            coaUnit.IsApproved = input.IsApproved;
            coaUnit.IsPrivate = input.IsPrivate;
            coaUnit.OrganizationUnitId = input.OrganizationUnitId;
            coaUnit.IsActive = input.IsActive;
            coaUnit.IsCorporate = input.IsCorporate;
            coaUnit.IsNumeric = input.IsNumeric;
            coaUnit.LinkChartOfAccountID = input.LinkChartOfAccountID;
            coaUnit.StandardGroupTotalId = input.StandardGroupTotalId;
            #endregion

            await _coaunitManager.UpdateAsync(coaUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return coaUnit.MapTo<CoaUnitDto>();
        }
        /// <summary>
        /// Delete ProjectCoa by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs_Delete)]
        public async Task DeleteProjectCoaUnit(IdInput input)
        {
            await _coaunitManager.DeleteAsync(input.Id);
        }

    }
}