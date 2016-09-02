using System;
using System.Collections.Generic;
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
using CAPS.CORPACCOUNTING.Financials.Preferences.Dto;
using Abp.UI;
using AutoMapper;

namespace CAPS.CORPACCOUNTING.Financials.Preferences
{
    public class FiscalYearAppService : CORPACCOUNTINGServiceBase, IFiscalYearAppService
    {
        private readonly FiscalYearUnitManager _fiscalYearUnitManager;
        private readonly IRepository<FiscalYearUnit> _fiscalYearUnitRepository;
        private readonly IRepository<FiscalPeriodUnit> _fiscalPeriodUnitRepository;
        private readonly FiscalPeriodUnitManager _fiscalPeriodUnitManager;


        public FiscalYearAppService(FiscalYearUnitManager fiscalYearUnitManager, IRepository<FiscalYearUnit> fiscalYearUnitUnitRepository,
         IRepository<FiscalPeriodUnit> fiscalPeriodUnitRepository,FiscalPeriodUnitManager fiscalPeriodUnitManager)
        {
            _fiscalYearUnitManager = fiscalYearUnitManager;
            _fiscalYearUnitRepository = fiscalYearUnitUnitRepository;
            _fiscalPeriodUnitRepository = fiscalPeriodUnitRepository;
            _fiscalPeriodUnitManager = fiscalPeriodUnitManager;
        }

        /// <summary>
        /// Creating FiscalYear
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateFiscalYearUnit(CreateFiscalYearUnitInput input)
        {
            
            #region Inserting FiscalYear
            var fiscalYearUnit = input.MapTo<FiscalYearUnit>();
            int id= await _fiscalYearUnitManager.CreateAsync(fiscalYearUnit);
           
            #endregion

            #region Bulk Insertion of Fiscal Period

            if (!ReferenceEquals(input.FiscalPeriodUnitList, null))
            {
                foreach (FiscalPeriodUnitInput createFiscalPeriodUnit in input.FiscalPeriodUnitList)
                {
                    createFiscalPeriodUnit.FiscalYearId = id;
                    if (input.IsYearOpen == false)
                    {
                        createFiscalPeriodUnit.IsPreClose = false;
                        createFiscalPeriodUnit.IsClose = true;
                    }
                    var fiscalPeriodUnit = createFiscalPeriodUnit.MapTo<FiscalPeriodUnit>();
                    fiscalPeriodUnit.IsPeriodOpen = (!createFiscalPeriodUnit.IsClose);
                    await _fiscalPeriodUnitManager.CreateAsync(fiscalPeriodUnit);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            #endregion

            await CurrentUnitOfWork.SaveChangesAsync();
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
            //Deleting FiscalPeriods By FiscalYearId
            await _fiscalPeriodUnitRepository.DeleteAsync(p => p.FiscalYearId == input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
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

            //If  Isyearopen is false then closing the FiscalPeriods.
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("FiscalYear.YearStartDate DESC", input.Sorting))
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
            var fiscalYearUnit = await _fiscalYearUnitRepository.GetAsync(input.FiscalYearId); ;
            Mapper.Map(input, fiscalYearUnit);
            await _fiscalYearUnitManager.UpdateAsync(fiscalYearUnit);

            if (!ReferenceEquals(input.FiscalPeriodUnitList, null))
            {
                foreach (var updateFiscalPeriodUnit in input.FiscalPeriodUnitList.OrderByDescending(p => p.FiscalPeriodId))
                {
                    //If  Isyearopen is false then closing the FiscalPeriods.
                    if (input.IsYearOpen == false)
                    {
                        updateFiscalPeriodUnit.IsPreClose = false;
                        updateFiscalPeriodUnit.IsClose = true;
                    }
                    //updating FiscalPeriod
                    if (updateFiscalPeriodUnit.FiscalPeriodId > 0)
                    {
                        var fiscalPeriodUnit = await _fiscalPeriodUnitRepository.GetAsync(updateFiscalPeriodUnit.FiscalPeriodId);
                        Mapper.Map(updateFiscalPeriodUnit, fiscalPeriodUnit);
                        fiscalPeriodUnit.IsPeriodOpen = (!updateFiscalPeriodUnit.IsClose);
                        await _fiscalPeriodUnitManager.UpdateAsync(fiscalPeriodUnit);
                    }
                    else
                    {
                        var fiscalPeriodUnit = updateFiscalPeriodUnit.MapTo<FiscalPeriodUnit>();
                        fiscalPeriodUnit.IsPeriodOpen = (!updateFiscalPeriodUnit.IsClose);
                        await _fiscalPeriodUnitManager.CreateAsync(fiscalPeriodUnit);
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// Get FiscalYear and Its FiscalPeriodList By FiscalPeriodId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FiscalYearUnitDto> GetFiscalYearById(IdInput input)
        {
            var fiscalYearUnit = await _fiscalYearUnitRepository.GetAll()
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


        /// <summary>
        /// Get the FiscalPeriods by FiscalYearId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<FiscalPeriodUnitDto>> GetFiscalPeriodUnits(GetFiscalPeriodDto input)
        {
            var query = from fiscalPeriod in _fiscalPeriodUnitRepository.GetAll()
                        select new { FiscalPeriod = fiscalPeriod };
           
            query = query.Where(p => p.FiscalPeriod.FiscalYearId == input.FiscalYearId);
           
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("FiscalPeriod.PeriodStartDate ASC", input.Sorting))
                .ToListAsync();

            return new PagedResultOutput<FiscalPeriodUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.FiscalPeriod.MapTo<FiscalPeriodUnitDto>();
                dto.FiscalPeriodId = item.FiscalPeriod.Id;
                dto.IsClose = (!item.FiscalPeriod.IsPeriodOpen);
                return dto;
            }).ToList());
        }
        /// <summary>
        /// Deleting FiscalPeriof By FiscalPeriod By Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteFiscalPeriodUnit(IdInput input)
        {
            await _fiscalPeriodUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
