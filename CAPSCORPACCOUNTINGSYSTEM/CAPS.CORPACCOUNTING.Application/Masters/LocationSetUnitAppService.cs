using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class LocationSetUnitAppService : CORPACCOUNTINGServiceBase, ILocationSetUnitAppService
    {
        private readonly LocationSetUnitManager _locationSetUnitManager;
        private readonly IRepository<LocationSetUnit> _locationSetUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LocationSetUnitAppService(LocationSetUnitManager locationSetUnitManager, IRepository<LocationSetUnit> locationSetUnitRepository, IUnitOfWorkManager unitOfWorkManager) {

            _locationSetUnitManager = locationSetUnitManager;
            _locationSetUnitRepository = locationSetUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<LocationSetUnitDto> CreateLocationSetUnit(CreateLocationSetUnitInput input)
        {
            var locationSetUnit = new LocationSetUnit(typeoflocationsetid: input.TypeOfLocationSetId, description: input.Description, number: input.Number,organizationunitid:input.OrganizationUnitId);
            await _locationSetUnitManager.CreateAsync(locationSetUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return locationSetUnit.MapTo<LocationSetUnitDto>();
        }

        public async Task DeleteLocationSetUnit(IdInput input)
        {
            await _locationSetUnitManager.DeleteAsync(input.Id);
        }

        public async Task<LocationSetUnitDto> GetLocationSetUnitsById(IdInput input)
        {
            var locationSet =await _locationSetUnitRepository.GetAsync(input.Id);
            return locationSet.MapTo<LocationSetUnitDto>();
        }

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
