using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    /// <summary>
    /// SubAccountResriction OutputDto
    /// </summary>
    [AutoMapFrom(typeof(SubAccountRestrictionUnit))]
    public class SubAccountRestrictionUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the SubAccountRestrictionId</summary>
        public long SubAccountRestrictionId { get; set; }

        /// <summary>Gets or sets the AccountId</summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the Caption</summary>

        public string Caption { get; set; }
        /// <summary>Gets or sets the Description</summary>
        /// 

        public string AccountNumber { get; set; }
        /// <summary>Gets or sets the AccountNumber</summary>
        /// 
        public string Description { get; set; }

        /// <summary>Gets or sets the SubAccountId </summary>

        public long SubAccountId { get; set; }

        /// <summary>Gets or sets the IsActive </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public long OrganizationUnitId { get; set; }
    }
}
