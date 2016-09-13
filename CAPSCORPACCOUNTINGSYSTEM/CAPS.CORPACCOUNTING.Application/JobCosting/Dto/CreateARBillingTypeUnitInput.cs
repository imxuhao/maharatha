using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapTo(typeof(ARBillingTypeUnit))]
    public class CreateARBillingTypeUnitInput : IInputDto
    {
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(ARBillingTypeUnit.MaxDescLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Job is Required")]
        public int JobId { get; set; }       

        /// <summary>Gets or sets the AccountId field. </summary>
        [Range(1, Int32.MaxValue,ErrorMessage = "Account is Required")]
        public long AccountId { get; set; }       

        /// <summary>Gets or sets the IsIctBillingType field. </summary>
        public bool IsIctBillingType { get; set; }

        /// <summary>Gets or sets the IsProjectBilling field. </summary>
        public bool IsProjectBilling { get; set; }

        /// <summary>Gets or sets the TypeofBillingId field. </summary>
        [EnumDataType(typeof(TypeofBilling))]
        public TypeofBilling TypeofBillingId { get; set; }      

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
