using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class DeleteAddressUnitInput : IInputDto
    {
        /// <summary>Gets or sets the ObjectId field. </summary>
        [Range(1, Int64.MaxValue)]
        public long ObjectId { get; set; }

        /// <summary>Gets or sets the TypeofObjectId field. </summary>
        [EnumDataType(typeof(TypeofObject))]
        public TypeofObject TypeofObjectId { get; set; }

        /// <summary>
        /// AddressId to be deleted from address table.
        /// </summary>
        public long Id { get; set; }
    }
}