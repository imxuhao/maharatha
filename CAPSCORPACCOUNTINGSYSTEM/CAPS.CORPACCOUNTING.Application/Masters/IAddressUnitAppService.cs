﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IAddressUnitAppService : IApplicationService
    {
        Task<AddressUnitDto> CreateAddressUnit(CreateAddressUnitInput input);

        Task<ListResultOutput<AddressUnitDto>> GetAddressUnits(GetAddressUnitInput input);
        Task<AddressUnitDto> UpdateAddressUnit(UpdateAddressUnitInput input);
        Task DeleteAddressUnit(DeleteAddressUnitInput input);

        
    }
}