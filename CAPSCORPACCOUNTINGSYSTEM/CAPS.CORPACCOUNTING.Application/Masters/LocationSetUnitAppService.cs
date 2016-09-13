using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Authorization;
using System.Data.Entity;
using Abp.Linq.Extensions;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class LocationSetUnitAppService : CORPACCOUNTINGServiceBase, ILocationSetUnitAppService
    {
        private readonly LocationSetUnitManager _locationSetUnitManager;
        private readonly IRepository<LocationSetUnit> _locationSetUnitRepository;
        private readonly IRepository<SubAccountUnit,long> _subAccountUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LocationSetUnitAppService(LocationSetUnitManager locationSetUnitManager,
            IRepository<LocationSetUnit> locationSetUnitRepository, 
            IUnitOfWorkManager unitOfWorkManager, 
            IRepository<SubAccountUnit, long> subAccountUnitRepository) {

            _locationSetUnitManager = locationSetUnitManager;
            _locationSetUnitRepository = locationSetUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _subAccountUnitRepository = subAccountUnitRepository;
        }
        /// <summary>
        /// Create the LocationSet.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LocationSetUnitDto> CreateLocationSetUnit(CreateLocationSetUnitInput input)
        {
            var locationSetUnit = new LocationSetUnit(typeoflocationsetid: input.TypeOfLocationSetId, description: input.Description, number: input.Number,organizationunitid:input.OrganizationUnitId);
            await _locationSetUnitManager.CreateAsync(locationSetUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return locationSetUnit.MapTo<LocationSetUnitDto>();
        }

        /// <summary>
        /// Delete the LocationSet based on LocationSetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteLocationSetUnit(IdInput input)
        {
            await _locationSetUnitManager.DeleteAsync(input.Id);
        }


        /// <summary>
        /// Get the LocationSet based on LocationSetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LocationSetUnitDto> GetLocationSetUnitsById(IdInput input)
        {
            var locationSet =await _locationSetUnitRepository.GetAsync(input.Id);
            LocationSetUnitDto result = locationSet.MapTo<LocationSetUnitDto>();
            result.LocationSetId = locationSet.Id;
            return result;
        }

        /// <summary>
        /// Update the LocationSet based on LocationSetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LocationSetUnitDto> UpdateLocationSetUnit(UpdateLocationSetUnitInput input)
        {
            var locationSetUnit = await _locationSetUnitRepository.GetAsync(input.LocationSetId);

            #region Setting the values to be updated

            locationSetUnit.Description = input.Description;
            locationSetUnit.Number = input.Number;
            locationSetUnit.TypeOfLocationSetId = input.TypeOfLocationSetId;
            locationSetUnit.OrganizationUnitId = input.OrganizationUnitId;
            locationSetUnit.OrganizationUnitId = input.OrganizationUnitId;
            #endregion

            await _locationSetUnitManager.UpdateAsync(locationSetUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return locationSetUnit.MapTo<LocationSetUnitDto>();
        }
      
    }
}
