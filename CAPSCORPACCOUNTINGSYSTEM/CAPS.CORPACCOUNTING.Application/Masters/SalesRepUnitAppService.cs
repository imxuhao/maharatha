using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class SalesRepUnitAppService : CORPACCOUNTINGServiceBase, ISalesRepUnitAppService
    {
        private readonly SalesRepUnitManager _salesRepUnitManager;
        private readonly IRepository<SalesRepUnit> _salesRepUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SalesRepUnitAppService(SalesRepUnitManager customerPaymentTermUnitManager, IRepository<SalesRepUnit> salesRepUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _salesRepUnitManager = customerPaymentTermUnitManager;
            _salesRepUnitRepository = salesRepUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// Creating the SalesRepresentative 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<SalesRepUnitDto> CreateSalesRepUnit(CreateSalesRepUnitInput input)
        {
            var salesRepUnit = input.MapTo<SalesRepUnit>();
            await _salesRepUnitManager.CreateAsync(salesRepUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return salesRepUnit.MapTo<SalesRepUnitDto>();
        }
        /// <summary>
        /// Deleting the SalesRepresentative By Id 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteSalesRepUnit(IdInput input)
        {
            await _salesRepUnitManager.DeleteAsync(input.Id);
        }
        /// <summary>
        /// Getting the SalesRep units for grid with sorting and filtering
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<SalesRepUnitDto>> GetSalesRepUnits(SearchInputDto input)
        {

            var query =
                from sr in _salesRepUnitRepository.GetAll()
                select new {SalesRep = sr};
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("SalesRep.LastName ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<SalesRepUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.SalesRep.MapTo<SalesRepUnitDto>();
                dto.SalesRepId = item.SalesRep.Id;
                return dto;
            }).ToList());
        }
        /// <summary>
        /// Updating the SalesRepUnit by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SalesRepUnitDto> UpdateSalesRepUnit(UpdateSalesRepUnitInput input)
        {
            var salesRepUnit = await _salesRepUnitRepository.GetAsync(input.SalesRepId);

            #region Setting the values to be updated

            salesRepUnit.LastName = input.LastName;
            salesRepUnit.FirstName = input.FirstName;
            salesRepUnit.IsActive = input.IsActive;
            salesRepUnit.Region = input.Region;
            salesRepUnit.OrganizationUnitId = input.OrganizationUnitId;
            #endregion

            await _salesRepUnitManager.UpdateAsync(salesRepUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the salesRep is Added*/
            };

            return salesRepUnit.MapTo<SalesRepUnitDto>();
        }
        /// <summary>
        /// Get the SalesRep Details By Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SalesRepUnitDto> GetSalesRepUnitsById(IdInput input)
        {
            SalesRepUnit salesRepItem = await _salesRepUnitRepository.GetAsync(input.Id);
            SalesRepUnitDto result= salesRepItem.MapTo<SalesRepUnitDto>();
            result.SalesRepId = salesRepItem.Id;
            return result;
        }
    }
}
