using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Financials.Preferences.Dto;
using Abp.UI;
using AutoMapper;


namespace CAPS.CORPACCOUNTING.Financials.Preferences
{
    public class FiscalYearAppService : CORPACCOUNTINGServiceBase, IFiscalYearAppService
    {
        private readonly FiscalYearUnitManager _fiscalYearUnitManager;
        private readonly IRepository<FiscalYearUnit> _fiscalYearUnitRepository;
        private readonly IFiscalPeriodAppService _fiscalPeriodUnitAppService;
        private readonly IRepository<FiscalPeriodUnit> _fiscalPeriodUnitRepository;

        public FiscalYearAppService(FiscalYearUnitManager fiscalYearUnitManager, IRepository<FiscalYearUnit> fiscalYearUnitUnitRepository,
          IFiscalPeriodAppService fiscalPeriodUnitAppService, IRepository<FiscalPeriodUnit> fiscalPeriodUnitRepository)
        {
            _fiscalYearUnitManager = fiscalYearUnitManager;
            _fiscalYearUnitRepository = fiscalYearUnitUnitRepository;
            _fiscalPeriodUnitAppService = fiscalPeriodUnitAppService;
            _fiscalPeriodUnitRepository = fiscalPeriodUnitRepository;
        }

        /// <summary>
        /// Creating FiscalYear
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateFiscalYearUnit(CreateFiscalYearUnitInput input)
        {
            if (input.YearStartDate > input.YearEndDate)
                throw new UserFriendlyException(L("FiscalStartDate should not greaterthan FiscalEndDate"));
            
            //validating FiscalPeriod Overlaping
            if (input.CreateFiscalPeriodUnits.Count != input.CreateFiscalPeriodUnits.Select(c => new { c.Month, c.Year }).Distinct().Count())
                throw new UserFriendlyException(L("FiscalPeriod should not be overlap"));

            #region Inserting FiscalPeriod
            var fiscalYearUnit = input.MapTo<FiscalYearUnit>();
            await _fiscalYearUnitManager.CreateAsync(fiscalYearUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            #endregion

            #region Inserting Fiscal Periods
            foreach (CreateFiscalPeriodUnitInput createFiscalPeriodUnit in input.CreateFiscalPeriodUnits)
            {
                createFiscalPeriodUnit.FiscalYearId = fiscalYearUnit.Id;
                await _fiscalPeriodUnitAppService.CreateFiscalPeriodUnit(createFiscalPeriodUnit);
            }
            #endregion
        }


        /// <summary>
        /// Deleting FiscalYear by FiscalYearId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task DeleteFiscalYearUnit(IdInput input)
        {
            await _fiscalYearUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
           
            //Deleting FiscalPeriods By FiscalYearId
            await _fiscalPeriodUnitRepository.DeleteAsync(p => p.FiscalYearId == input.Id);
        }

        /// <summary>
        /// Get All List of FiscalYears with Filters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<FiscalYearUnitDto>> GetFiscalYearUnits(SearchInputDto input)
        {
            var query = from fiscalyear in _fiscalYearUnitRepository.GetAll()
                        select new { FiscalYear = fiscalyear };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(p => p.FiscalYear.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("FiscalYear.YearStartDate ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<FiscalYearUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.FiscalYear.MapTo<FiscalYearUnitDto>();
                dto.FiscalYearId = item.FiscalYear.Id;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// Update FiscalYear
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateFiscalYearUnit(UpdateFiscalYearUnitInput input)
        {
            //validating startDate will be lessthan end date
            if (input.YearStartDate > input.YearEndDate)
                throw new UserFriendlyException(L("FiscalStartDate should not greaterthan FiscalEndDate"));

            //validating FiscalPeriod overlap
            if (input.UpdateFiscalPeriodUnits.Count != input.UpdateFiscalPeriodUnits.Select(c => new { c.Month, c.Year }).Distinct().Count())
                throw new UserFriendlyException(L("FiscalPeriod should not be overlap"));

            var fiscalYearUnit = await _fiscalYearUnitRepository.GetAsync(input.FiscalYearId);
            Mapper.CreateMap<UpdateFiscalPeriodUnitInput, FiscalYearUnit>()
                    .ForMember(u => u.Id, ap => ap.MapFrom(src => src.FiscalYearId));
            Mapper.Map(input, fiscalYearUnit);

            await _fiscalYearUnitManager.UpdateAsync(fiscalYearUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            
            foreach (var updateFiscalPeriodUnit in input.UpdateFiscalPeriodUnits)
            {
                //updating FiscalPeriod
                if (updateFiscalPeriodUnit.FiscalPeriodId > 0)
                {
                    await _fiscalPeriodUnitAppService.UpdateFiscalPeriodUnit(updateFiscalPeriodUnit);
                }
                else
                {
                    //Inserting FiscalPeriod
                    AutoMapper.Mapper.CreateMap<UpdateFiscalPeriodUnitInput, CreateFiscalPeriodUnitInput>();
                    await _fiscalPeriodUnitAppService.CreateFiscalPeriodUnit(AutoMapper.Mapper.Map<UpdateFiscalPeriodUnitInput, CreateFiscalPeriodUnitInput>(updateFiscalPeriodUnit));

                }
            }

        }

        /// <summary>
        /// Get FiscalYear and Its FiscalPeriodList By FiscalPeriodId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FiscalYearUnitDto> GetFiscalYearById(IdInput input)
        {
            var fiscalYearUnit = await
                _fiscalYearUnitRepository.GetAll()
                    .Include(p => p.FiscalPeriodList)
                    .FirstOrDefaultAsync(p => p.Id == input.Id);
            FiscalYearUnitDto fiscalYearDto = fiscalYearUnit.MapTo<FiscalYearUnitDto>();
            fiscalYearDto.FiscalYearId = fiscalYearUnit.Id;
            foreach (FiscalPeriodUnitDto fiscalperiod in fiscalYearDto.FiscalPeriodList)
            {
                fiscalperiod.FiscalPeriodId = fiscalperiod.Id;

            }
            return fiscalYearDto;
        }

    }
}
