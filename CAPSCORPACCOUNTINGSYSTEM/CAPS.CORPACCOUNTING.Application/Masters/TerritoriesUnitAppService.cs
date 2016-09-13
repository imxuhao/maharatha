using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// 
    /// </summary>
    public class TerritoriesUnitAppService : CORPACCOUNTINGServiceBase, ITerritoriesUnitAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly TerritoriesUnitManager _territoriesUnitManager;
        private readonly IRepository<TerritoriesUnit> _territoriesUnitRepository;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="unitOfWorkManager"></param>
      /// <param name="territoriesUnitManager"></param>
      /// <param name="territoriesUnitRepository"></param>
        public TerritoriesUnitAppService(IUnitOfWorkManager unitOfWorkManager, TerritoriesUnitManager territoriesUnitManager, IRepository<TerritoriesUnit> territoriesUnitRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _territoriesUnitManager = territoriesUnitManager;
            _territoriesUnitRepository = territoriesUnitRepository;
        }

        /// <summary>
        /// Create the TerritoriesUnit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateTerritoriesUnit(CreateTerritoriesUnitInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var territoriesUnit = input.MapTo<TerritoriesUnit>();
                await _territoriesUnitManager.CreateAsync(territoriesUnit);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update the TerritoriesUnit based on TerritorieId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateTerritoriesUnit(UpdateTerritoriesUnitInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var territoriesUnit = await _territoriesUnitRepository.GetAsync(input.TerritorieId);
                Mapper.Map(input, territoriesUnit);
                await _territoriesUnitManager.UpdateAsync(territoriesUnit);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        ///  Get the list of all Territories.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<TerritoriesUnitDto>> GetTerritoriesUnits(SearchInputDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = _territoriesUnitRepository.GetAll();
                var items = await query.ToListAsync();

                return new ListResultOutput<TerritoriesUnitDto>(
                    items.Select(item =>
                    {
                        var dto = item.MapTo<TerritoriesUnitDto>();
                        dto.TerritorieId = item.Id;
                        return dto;
                    }).ToList());
            }
        }

        /// <summary>
        ///  Delete the Territories based on TerritorieId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteTerritoriesUnit(IdInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _territoriesUnitManager.DeleteAsync(input.Id);
            }
        }
    }
}
