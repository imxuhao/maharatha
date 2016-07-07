using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    /// <summary>
    /// SubAccountRestriction InputDto
    /// </summary>
    [AutoMapTo(typeof(SubAccountRestrictionUnit))]
    public class SubAccountRestrictionUnitInput : IInputDto
    {
        /// <summary>Gets or sets the SubAccountRestrictionId</summary>
        public long SubAccountRestrictionId { get; set; }

        /// <summary>Gets or sets the AccountId</summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "Account is Required")]
        public long AccountId { get; set; }
       

        /// <summary>Gets or sets the IsActive </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId </summary>
       
        public long? OrganizationUnitId { get; set; }
    }
}
