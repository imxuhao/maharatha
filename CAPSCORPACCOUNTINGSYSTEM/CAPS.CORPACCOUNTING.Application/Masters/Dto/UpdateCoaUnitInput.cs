using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class UpdateCoaUnitInput : IInputDto
    {
        [Range(1, Int32.MaxValue)]
        public  int CoaId { get; set;}
        
        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(CoaUnit.MaxDisplayNameLength)]
        [Required]
        public string Caption { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(CoaUnit.MaxDesc)]
        public string Description { get; set; }        

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public int? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; } = false;

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public bool IsPrivate { get; set; } = false;

        /// <summary>Gets or sets the OrganizationId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsCorporate field. </summary>
        public bool IsCorporate { get; set; }

        /// <summary>Gets or sets the IsNumeric field. </summary>
        public bool IsNumeric { get; set; }

        /// <summary>Gets or sets the LinkChartOfAccountID field. </summary>
        public int? LinkChartOfAccountID { get; set; }

        /// <summary>Gets or sets the StandardGroupTotalId field. </summary>      
        public StandardGroupTotal? StandardGroupTotalId { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>      
        public virtual long? RollupAccountId { get; set; }

        /// <summary>Gets or sets the RollupDivisionId field. </summary>      
        public virtual int? RollupDivisionId { get; set; }
    }
}
