using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Address.
    /// </summary>
    public interface IAddressUnitAppService : IApplicationService
    {
        /// <summary>
        ///  Create the Address.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddressUnitDto> CreateAddressUnit(CreateAddressUnitInput input);

        /// <summary>
        /// Get the list of all Addresses based on ObjectType.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<AddressUnitDto>> GetAddressUnits(GetAddressUnitInput input);

        /// <summary>
        /// Update the Address based on AddressId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddressUnitDto> UpdateAddressUnit(UpdateAddressUnitInput input);

        /// <summary>
        /// Delete the Address based on AddressId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAddressUnit(DeleteAddressUnitInput input);

        /// <summary>
        /// Get TerritoriesList
        /// </summary>
        /// <returns></returns>
          Task<List<NameValueDto>> GetTerritoriesList();



    }
}