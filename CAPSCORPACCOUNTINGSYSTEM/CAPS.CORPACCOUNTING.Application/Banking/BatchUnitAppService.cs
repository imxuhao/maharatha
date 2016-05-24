using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using AutoMapper;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    public class BatchUnitAppService : CORPACCOUNTINGServiceBase, IBatchUnitAppService
    {

        private readonly BatchUnitManager _batchUnitManager;
        private readonly IRepository<BatchUnit, int> _batchUnitRepository;

        public BatchUnitAppService(BatchUnitManager batchUnitManager, IRepository<BatchUnit, int> batchUnitRepository)
        {
            _batchUnitManager = batchUnitManager;
            _batchUnitRepository = batchUnitRepository;
        }

        /// <summary>
        /// Create the BatchUnit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateBatchUnit(CreateBatchUnitInput input)
        {
            var batchUnit = input.MapTo<BatchUnit>();
            await _batchUnitManager.CreateAsync(batchUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Update the BatchUnit based on BatchId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateBatchUnit(UpdateBatchUnitInput input)
        {
            var batchUnit = await _batchUnitRepository.GetAsync(input.BatchId);
            Mapper.CreateMap<UpdateBatchUnitInput, BatchUnit>()
                          .ForMember(u => u.Id, ap => ap.MapFrom(src => src.BatchId));
            Mapper.Map(input, batchUnit);
            await _batchUnitManager.UpdateAsync(batchUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        /// <summary>
        /// Delete the BatchUnit based on BatchId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteBatchUnit(IdInput input)
        {
            await _batchUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        /// <summary>
        /// Get the list of all Batch List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<BatchUnitDto>> GetBatchUnits(SearchInputDto input)
        {

            var batchUnitQuery = _batchUnitRepository.GetAll();


            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    batchUnitQuery = Helper.CreateFilters(batchUnitQuery, mapSearchFilters);
            }

            batchUnitQuery = batchUnitQuery.Where(item => item.OrganizationUnitId == input.OrganizationUnitId || item.OrganizationUnitId == null);
            var resultCount = await batchUnitQuery.CountAsync();
            var results = await batchUnitQuery
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            var mapEnumResults = (from value in results
                                  select new BatchUnitDto
                                  {
                                      BatchId=value.Id,
                                      ControlTotal=value.ControlTotal,
                                      DefaultCheckDate=value.DefaultCheckDate,
                                      DefaultTransactionDate=value.DefaultTransactionDate,
                                      Description=value.Description,
                                      IsActive=value.IsActive,
                                      IsBatchFinalized=value.IsBatchFinalized,
                                      IsDefault=value.IsDefault,
                                      IsRetained=value.IsRetained,
                                      IsUniversal=value.IsUniversal,
                                      PostingDate=value.PostingDate,
                                      RecurMonthIncrement=value.RecurMonthIncrement,
                                      TypeOfBatchId=value.TypeOfBatchId,
                                      TypeOfInactiveStatusId=value.TypeOfInactiveStatusId,
                                      TypeOfBatch = value.TypeOfBatchId.ToDisplayName(),
                                      TypeOfInactiveStatus = value.TypeOfInactiveStatusId != null ? value.TypeOfInactiveStatusId.ToDisplayName() : ""
                                  }).ToList();

            return new PagedResultOutput<BatchUnitDto>(resultCount, mapEnumResults);
        }

        /// <summary>
        /// Get BatchUnit based on BatchId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BatchUnitDto> GetBatchUnitsById(IdInput input)
        {
            var batchUnitQuery = await _batchUnitRepository.GetAsync(input.Id);
            var result = batchUnitQuery.MapTo<BatchUnitDto>();
            result.BatchId = batchUnitQuery.Id;
            return result;
        }

        /// <summary>
        /// Get Batch Type
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetBatchTypeList()
        {
            return EnumList.GetBatchTypeList();
        }


        /// <summary>
        /// Get Batch List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetBatchList(AutoSearchInput input)
        {
            var batchList = await (from subaccount in _batchUnitRepository.GetAll()
                                    .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                                    .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                                        select new NameValueDto { Name = subaccount.Description, Value = subaccount.Id.ToString() })
                              .ToListAsync();
            return batchList;
        }
    }
}
