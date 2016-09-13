using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using System.Linq;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class VendorPaymentTermUnitAppService : CORPACCOUNTINGServiceBase, IVendorPaymentTermUnitAppService
    {
        private readonly VendorPaymentTermUnitManager _vendorPaymentTermUnitManager;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaymentTermUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VendorPaymentTermUnitAppService(VendorPaymentTermUnitManager vendorPaymentTermUnitManager, IRepository<VendorPaymentTermUnit> vendorPaymentTermUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _vendorPaymentTermUnitManager = vendorPaymentTermUnitManager;
            _vendorPaymentTermUnitRepository = vendorPaymentTermUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Creating the VendorPaymentTerm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<VendorPaymentTermUnitDto> CreateVendorPaymentTermUnit(CreateVendorPaymentTermUnitInput input)
        {
            var vendorPaymentTermUnit = input.MapTo<VendorPaymentTermUnit>();
            await _vendorPaymentTermUnitManager.CreateAsync(vendorPaymentTermUnit);
            await   CurrentUnitOfWork.SaveChangesAsync();

            #region Example to show the usage of Event Bus as well Unit of Work Completion

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of VendorPaymentTerm is Added*/
            };

            EventBus.Register<EntityChangedEventData<VendorPaymentTermUnitDto>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when VendorPaymentTerm is added
                });

            #endregion

            return vendorPaymentTermUnit.MapTo<VendorPaymentTermUnitDto>();
        }
        /// <summary>
        /// Deleting the VendorPaymentTerm by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteVendorPaymentTermUnit(IdInput input)
        {
            await _vendorPaymentTermUnitManager.DeleteAsync(input.Id);
        }
        /// <summary>
        /// Get the VendorPaymentTerms for grid with filtering and sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<VendorPaymentTermUnitDto>> GetVendorPaymentTermUnits(SearchInputDto input)
        {
            var query =
                from vpt in _vendorPaymentTermUnitRepository.GetAll()
                select new { VendorPayTerms=vpt };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("VendorPayTerms.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<VendorPaymentTermUnitDto>(resultCount, results.Select(item =>
                {
                    var dto = item.VendorPayTerms.MapTo<VendorPaymentTermUnitDto>();
                    dto.VendorPaymentTermId = item.VendorPayTerms.Id;
                    return dto;
                }).ToList());
        }

        /// <summary>
        /// Updating the VendorPaymentTerms by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<VendorPaymentTermUnitDto> UpdateVendorPaymentTermUnit(UpdateVendorPaymentTermUnitInput input)
        {
            var vendorPaymentTermUnit = await _vendorPaymentTermUnitRepository.GetAsync(input.VendorPaymentTermId);

            #region Setting the values to be updated

            vendorPaymentTermUnit.Description = input.Description;
            vendorPaymentTermUnit.DueDays = input.DueDays;
            vendorPaymentTermUnit.DiscountDays = input.DiscountDays;
            vendorPaymentTermUnit.OrganizationUnitId = input.OrganizationUnitId;
            vendorPaymentTermUnit.IsActive = input.IsActive;
            #endregion

            await _vendorPaymentTermUnitManager.UpdateAsync(vendorPaymentTermUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of VendorPaymentTerm is Added*/
            };

            EventBus.Register<EntityChangedEventData<CoaUnit>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when VendorPaymentTerm is added
                });

            return vendorPaymentTermUnit.MapTo<VendorPaymentTermUnitDto>();
        }

        /// <summary>
        /// Getting the VendorPaymentTerm by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<VendorPaymentTermUnitDto> GetVendorPayTermUnitsById(IdInput input)
        {
            var vendorPayTerms = await _vendorPaymentTermUnitRepository.GetAsync(input.Id);
            VendorPaymentTermUnitDto result = vendorPayTerms.MapTo<VendorPaymentTermUnitDto>();
            result.VendorPaymentTermId = vendorPayTerms.Id;
            return result;
        }

        /// <summary>
        /// Get all VendorPayment Terms Order by Description
        /// </summary>
        /// <returns></returns>
        public async Task<List<VendorPaymentTermUnitDto>> GetVendorPayTerms()
        {
            var vendorPayTerms = await _vendorPaymentTermUnitRepository.GetAll().OrderBy(p => p.Description).ToListAsync();
            return new List<VendorPaymentTermUnitDto>(vendorPayTerms.Select(item =>
            {
                var dto = item.MapTo<VendorPaymentTermUnitDto>();
                dto.VendorPaymentTermId = item.Id;
                return dto;
            }).ToList());
        }
    }
}
