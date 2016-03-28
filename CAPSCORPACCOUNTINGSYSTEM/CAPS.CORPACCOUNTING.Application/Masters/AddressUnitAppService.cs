using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class AddressUnitAppService : CORPACCOUNTINGServiceBase, IAddressUnitAppService
    {
        private readonly AddressUnitManager _addressUnitManager;
        private readonly IRepository<AddressUnit, long> _addressUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AddressUnitAppService(AddressUnitManager addressUnitManager, IRepository<AddressUnit, long> addressUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _addressUnitManager = addressUnitManager;
            _addressUnitRepository = addressUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public async Task<AddressUnitDto> CreateAddressUnit(CreateAddressUnitInput input)
        {
            var addressUnit = input.MapTo<AddressUnit>();
            await _addressUnitManager.CreateAsync(addressUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return addressUnit.MapTo<AddressUnitDto>();
        }

        public async Task DeleteAddressUnit(DeleteAddressUnitInput input)
        {
            await _addressUnitRepository.DeleteAsync(p => p.ObjectId == input.ObjectId && p.TypeofObjectId == input.TypeofObjectId);
        }

        public async Task<ListResultOutput<AddressUnitDto>> GetAddressUnits(GetAddressUnitInput input)
        {
            var query = _addressUnitRepository.GetAll()
                    .Where(au => au.TypeofObjectId == input.TypeofObjectId
                    && (input.OrganizationUnitId == null || au.OrganizationUnitId == input.OrganizationUnitId))
                    .Select(au => new { au });
            var items = await query.ToListAsync();

            return new ListResultOutput<AddressUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<AddressUnitDto>();
                    dto.AddressId = item.au.Id;
                    return dto;
                }).ToList());
        }

        public async Task<AddressUnitDto> UpdateAddressUnit(UpdateAddressUnitInput input)
        {
            
            var addressUnit = await _addressUnitRepository.GetAsync(input.AddressId);

            #region Setting the values to be updated

            addressUnit.ObjectId = input.ObjectId;
            addressUnit.TypeofObjectId = input.TypeofObjectId;
            addressUnit.AddressTypeId = input.AddressTypeId;
            addressUnit.ContactNumber = input.ContactNumber;
            addressUnit.Line1 = input.Line1;
            addressUnit.Line2 = input.Line2;
            addressUnit.Line3 = input.Line3;
            addressUnit.Line4 = input.Line4;
            addressUnit.City = input.City;
            addressUnit.State = input.State;
            addressUnit.Country = input.Country;
            addressUnit.PostalCode = input.PostalCode;
            addressUnit.Email = input.Email;
            addressUnit.Phone1 = input.Phone1;
            addressUnit.Phone1Extension = input.Phone1Extension;
            addressUnit.Phone2 = input.Phone2;
            addressUnit.Phone2Extension = input.Phone2Extension;
            addressUnit.Website = input.Website;
            addressUnit.OrganizationUnitId = input.OrganizationUnitId;
            addressUnit.IsPrimary = input.IsPrimary;
            addressUnit.Fax = input.Fax;
            #endregion

            await _addressUnitManager.UpdateAsync(addressUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Address is Added*/
            };

            return addressUnit.MapTo<AddressUnitDto>();
        }
    }
}
