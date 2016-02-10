using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;

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

        public async Task<ListResultOutput<SalesRepUnitDto>> GetSalesRepUnits(GetSalesRepInput input)
        {
            input.SortOrder = input.SortOrder == "ASC" ? " ascending" : " descending";
            var query =
                from sr in _salesRepUnitRepository.GetAll().OrderBy(input.SortColumn + input.SortOrder)
                        .Skip((input.PageNumber - 1) * input.NumberofColumnsperPage)
                        .Take(input.NumberofColumnsperPage)
                where (input.OrganizationUnitId == null || sr.OrganizationUnitId == input.OrganizationUnitId)&&
                (input.LastName == null || sr.LastName .Contains(input.LastName))
                select new { sr };
            var items = await query.ToListAsync();

            return new ListResultOutput<SalesRepUnitDto>(
                items.Select(item =>
                {
                    var dto = item.sr.MapTo<SalesRepUnitDto>();
                    dto.SalesRepId = item.sr.Id;
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
            var salesRepQuery =
               from cpt in _salesRepUnitRepository.GetAll()
               where cpt.Id == input.Id
               select new { cpt };
            var salesRepItems = await salesRepQuery.ToListAsync();

            var result = salesRepItems[0].cpt.MapTo<SalesRepUnitDto>();
            result.SalesRepId = salesRepItems[0].cpt.Id;
            return result;
        }
    }
}
