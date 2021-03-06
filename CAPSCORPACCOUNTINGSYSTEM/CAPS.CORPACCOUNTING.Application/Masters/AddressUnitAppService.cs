﻿using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Helpers;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class AddressUnitAppService : CORPACCOUNTINGServiceBase, IAddressUnitAppService
    {
        private readonly AddressUnitManager _addressUnitManager;
        private readonly IRepository<AddressUnit, long> _addressUnitRepository;
        private readonly IRepository<TerritoriesUnit, int> _territoriesUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AddressUnitAppService(AddressUnitManager addressUnitManager, 
            IRepository<AddressUnit, long> addressUnitRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<TerritoriesUnit, int> territoriesUnitRepository)
        {
            _addressUnitManager = addressUnitManager;
            _addressUnitRepository = addressUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _territoriesUnitRepository = territoriesUnitRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<AddressUnitDto> CreateAddressUnit(CreateAddressUnitInput input)
        {
            var addressUnit = input.MapTo<AddressUnit>();
            await _addressUnitManager.CreateAsync(addressUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            var addr = addressUnit.MapTo<AddressUnitDto>();
            addr.AddressId = addressUnit.Id;
            return addr;
        }

        /// <summary>
        /// Delete Address by AddressId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteAddressUnit(IdInput<long> input)
        {
            await _addressUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

        /// <summary>
        /// Deleting the addresses by ObjectId and TypeofObject
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteAddressUnitByEntity(DeleteAddressUnitInput input)
        {
            await _addressUnitRepository.DeleteAsync(p => p.TypeofObjectId == input.TypeofObjectId && p.ObjectId==input.ObjectId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<AddressUnitDto>> GetAddressUnits(GetAddressUnitInput input)
        {
            var query =from address in _addressUnitRepository.GetAll().Where(au => au.TypeofObjectId == input.TypeofObjectId && au.ObjectId == input.ObjectId)
                       join territorie in _territoriesUnitRepository.GetAll() on address.TerritorieId equals territorie.Id into addr
                       from addresss in addr.DefaultIfEmpty()
                       select new { au=address,territorie= addresss.Description };
            var items = await query.ToListAsync();

            return new ListResultOutput<AddressUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<AddressUnitDto>();
                    dto.AddressId = item.au.Id;
                    dto.AddressType = item.au.AddressTypeId.ToDisplayName();
                    dto.TypeofObject = item.au.TypeofObjectId.ToDisplayName();
                    dto.TerritorieName = item.territorie;
                    return dto;
                }).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Get TerritoriesList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTerritoriesList()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var territoriesList = await _territoriesUnitRepository.GetAll().Select(u => new NameValueDto
                {
                    Name = u.Description,
                    Value = u.Id.ToString(),

                }).ToListAsync();
                return territoriesList;
            }
        }
    }
}
