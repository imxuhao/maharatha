using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(SalesRepUnit))]
    public class CreateSalesRepUnitInput : IInputDto
    {
        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(SalesRepUnit.MaxName)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(SalesRepUnit.MaxName)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the Region field. </summary>
        [StringLength(SalesRepUnit.MaxRegionLength)]
        public string Region { get; set; }

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
