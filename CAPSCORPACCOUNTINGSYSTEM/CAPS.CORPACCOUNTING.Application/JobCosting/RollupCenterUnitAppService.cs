using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class RollupCenterUnitAppService : CORPACCOUNTINGServiceBase, IRollupCenterUnitAppService
    {
        private readonly RollupCenterUnitManager _rollupCenterUnitManager;
        private readonly IRepository<RollupCenterUnit> _rollupCenterUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public RollupCenterUnitAppService(RollupCenterUnitManager rollupCenterUnitManager, IRepository<RollupCenterUnit> rollupCenterUnitRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _rollupCenterUnitManager = rollupCenterUnitManager;
            _rollupCenterUnitRepository = rollupCenterUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// To Create the RollupCenter
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RollupCenterUnitDto> CreateRollupCenterUnit(CreateRollupCenterUnitInput input)
        {
            var rollupCenter = input.MapTo<RollupCenterUnit>();
            await _rollupCenterUnitManager.CreateAsync(rollupCenter);
            await CurrentUnitOfWork.SaveChangesAsync();
            return rollupCenter.MapTo<RollupCenterUnitDto>();
        }

        /// <summary>
        /// To Update RollupCenter
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RollupCenterUnitDto> UpdateRollupCenterUnit(UpdateRollupCenterUnitInput input)
        {
            var rollupCenterUnit = await _rollupCenterUnitRepository.GetAsync(input.RollupCenterId);

            #region Setting the values to be updated

            rollupCenterUnit.Caption = input.Caption;
            rollupCenterUnit.AccountId = input.AccountId;
            rollupCenterUnit.JobId = input.JobId;
            rollupCenterUnit.IsActive = input.IsActive;
            rollupCenterUnit.IsApproved = input.IsApproved;
            rollupCenterUnit.RollupTypeId = input.RollupTypeId;
            rollupCenterUnit.OrganizationUnitId = input.OrganizationUnitId;
           
            #endregion

            await _rollupCenterUnitManager.UpdateAsync(rollupCenterUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Job is Added*/
            };

            return rollupCenterUnit.MapTo<RollupCenterUnitDto>();
        }

        /// <summary>
        /// To Delete RollupCenetr
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteRollupCenterUnit(IdInput input)
        {
            await _rollupCenterUnitManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// To Get the record of RollupCenter with Sorting and Searching 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<RollupCenterUnitDto>> GetRollupCenterUnits(SearchInputDto input)
        {
            var query =from rc in _rollupCenterUnitRepository.GetAll()                
                select new { RollupCenter = rc };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.RollupCenter.OrganizationUnitId == input.OrganizationUnitId || item.RollupCenter.OrganizationUnitId == null);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("RollupCenter.Caption ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<RollupCenterUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.RollupCenter.MapTo<RollupCenterUnitDto>();
                dto.RollupCenterId = item.RollupCenter.Id;
                return dto;
            }).ToList());
        }
        /// <summary>
        /// Get the RollupCenterDetails by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       public async Task<RollupCenterUnitDto> GetRollupCenterUnitById(IdInput input)
        {
            RollupCenterUnit rollupCenteritem = await _rollupCenterUnitRepository.GetAsync(input.Id);
            RollupCenterUnitDto result = rollupCenteritem.MapTo<RollupCenterUnitDto>();
            result.RollupCenterId = rollupCenteritem.Id;
            return result;
        }
    }
}
