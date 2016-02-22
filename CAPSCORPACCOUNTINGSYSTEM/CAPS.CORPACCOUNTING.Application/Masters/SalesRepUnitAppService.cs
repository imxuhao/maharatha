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

namespace CAPS.CORPACCOUNTING.Masters
{
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

        [UnitOfWork]
        public async Task<SalesRepUnitDto> CreateSalesRepUnit(
            CreateSalesRepUnitInput input)
        {
            var salesRepUnit = new SalesRepUnit(lastname:input.LastName,firstname:input.FirstName,region:input.Region,isactive:input.IsActive,
                organizationunitid:input.OrganizationUnitId);
            await _salesRepUnitManager.CreateAsync(salesRepUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return salesRepUnit.MapTo<SalesRepUnitDto>();
        }

        public async Task DeleteSalesRepUnit(IdInput input)
        {
            await _salesRepUnitManager.DeleteAsync(input.Id);
        }

        public async Task<PagedResultOutput<SalesRepUnitDto>> GetSalesRepUnits(GetSalesRepInput input)
        {

            var query =
                from sr in _salesRepUnitRepository.GetAll()
                select new {SalesRep = sr};
            query = query
                .WhereIf(input.OrganizationUnitId != null,
                    item => item.SalesRep.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<SalesRepUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.SalesRep.MapTo<SalesRepUnitDto>();
                dto.SalesRepId = item.SalesRep.Id;
                return dto;
            }).ToList());
        }

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
                /*Do Something when the Chart of salesRep is Added*/
            };

            return salesRepUnit.MapTo<SalesRepUnitDto>();
        }
        /// <summary>
        /// Get the SalesRep Details By CustomerPaymentTermsId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SalesRepUnitDto> GetSalesRepUnitsById(IdInput input)
        {
            var salesRepQuery = await _salesRepUnitRepository.GetAsync(input.Id);
            return salesRepQuery.MapTo<SalesRepUnitDto>();
        }
    }
}
