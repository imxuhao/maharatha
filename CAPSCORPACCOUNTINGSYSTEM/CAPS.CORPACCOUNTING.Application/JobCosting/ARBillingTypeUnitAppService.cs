using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class ARBillingTypeUnitAppService : CORPACCOUNTINGServiceBase, IARBillingTypeUnitAppService
    {
        private readonly IRepository<ARBillingTypeUnit> _arBillingTypeUnitUnitRepository;
        private readonly ARBillingTypeUnitManager _arBillingTypeUnitManager;        
        /// 
        public ARBillingTypeUnitAppService(IRepository<ARBillingTypeUnit> arbillingtypeunitunitrepository, ARBillingTypeUnitManager arbillingtypeunitmanager)
        {
            _arBillingTypeUnitUnitRepository = arbillingtypeunitunitrepository;
            _arBillingTypeUnitManager = arbillingtypeunitmanager;
        }

        /// <summary>
        /// Create the ARBillingType.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ARBillingTypeDto> CreateARBillingTypeUnit(CreateARBillingTypeUnitInput input)
        {
            var arBillingTypeUnit = input.MapTo<ARBillingTypeUnit>();
            await _arBillingTypeUnitManager.CreateAsync(arBillingTypeUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return arBillingTypeUnit.MapTo<ARBillingTypeDto>();
        }
        /// <summary>
        /// Delete ARBillingType based on ARBillingTypeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteARBillingTypeUnit(IdInput input)
        {
            await _arBillingTypeUnitManager.DeleteAsync(input.Id);
        }
        /// <summary>
        /// Get the ARBillingType based on ARBillingTypeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ARBillingTypeDto> GetARBillingTypeUnitById(IdInput input)
        {
            ARBillingTypeUnit arBillingTypeUnit = await _arBillingTypeUnitUnitRepository.GetAsync(input.Id);
            ARBillingTypeDto result = arBillingTypeUnit.MapTo<ARBillingTypeDto>();
            result.ARBillingTypeId = arBillingTypeUnit.Id;
            return result;
        }
        /// <summary>
        /// Update ARBillingType based on ARBillingTypeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ARBillingTypeDto> UpdateARBillingTypeUnit(UpdateARBillingTypeUnitInput input)
        {
            var arBillingTypeUnit = await _arBillingTypeUnitUnitRepository.GetAsync(input.ARBillingTypeId);
            //AutoMapper.Mapper.CreateMap<UpdateARBillingTypeUnitInput, ARBillingTypeUnit>().ForMember(u => u.Id, ap => ap.MapFrom(src => src.ARBillingTypeId));
            //Mapper.Initialize(cfg => {
            //    cfg.CreateMap<UpdateARBillingTypeUnitInput, ARBillingTypeUnit>();
            //});
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UpdateARBillingTypeUnitInput, ARBillingTypeUnit>();
            });
            AutoMapper.Mapper.Map(input, arBillingTypeUnit);
            await _arBillingTypeUnitManager.UpdateAsync(arBillingTypeUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return arBillingTypeUnit.MapTo<ARBillingTypeDto>();
        }
    }
}
