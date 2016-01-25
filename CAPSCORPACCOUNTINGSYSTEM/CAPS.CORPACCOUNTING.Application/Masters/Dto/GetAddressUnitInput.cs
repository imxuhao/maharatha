using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetAddressUnitInput : IInputDto
    {
        [Range(1, Int32.MaxValue)]
        public int ObjectId { get; set; }

        /// <summary>Gets or sets the TypeofObjectId field. </summary>
        [EnumDataType(typeof(TypeofObject))]
        public TypeofObject TypeofObjectId { get; set; }

        /// <summary>Gets or sets the AddressTypeId field. </summary>
        public TypeofAddress AddressTypeId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
