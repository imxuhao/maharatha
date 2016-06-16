using System.Collections.Generic;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
   public class UpdateSubAccountUnitInput : IInputDto
    {
        /// <summary>Gets or sets the SubAccountId field. </summary>
        public long SubAccountId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(SubAccountUnit.MaxLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(SubAccountUnit.MaxCaptionLength)]
        public string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary> 
        public short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the SubAccountNumber field. </summary>
        [Required]
        [StringLength(SubAccountUnit.MaxLength)]
        public string SubAccountNumber { get; set; }

        /// <summary>Gets or sets the AccountingLayoutItemID field. </summary>
        public int? AccountingLayoutItemId { get; set; }

        /// <summary>Gets or sets the GroupCopyLabel field. </summary>
        [StringLength(SubAccountUnit.MaxCaptionLength)]
        public string GroupCopyLabel { get; set; }

        /// <summary>Gets or sets the IsAccountSpecific field. </summary>
        public bool IsAccountSpecific { get; set; }

        /// <summary>Gets or sets the IsMandatory field. </summary>
        public bool IsMandatory { get; set; }

        /// <summary>Gets or sets the IsBudgetInclusive field. </summary>
        public bool IsBudgetInclusive { get; set; }

        /// <summary>Gets or sets the IsCorporateSubAccount field. </summary>// IsBudgetInclusive
        public bool IsCorporateSubAccount { get; set; }

        /// <summary>Gets or sets the IsProjectSubAccount field. </summary>
        public bool IsProjectSubAccount { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public int EntityId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool? IsEnterable { get; set; }

        /// <summary>Gets or sets the SearchOrder field. </summary> 
        public long? SearchOrder { get; set; }

        /// <summary>Gets or sets the SearchNo field. </summary>
        [StringLength(SubAccountUnit.MaxSerialNoLength)]
        public string SearchNo { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TypeofSubAccountId field. </summary>
        [EnumDataType(typeof(TypeofSubAccount))]
        public TypeofSubAccount TypeofSubAccountId { get; set; }

        /// <summary>
        ///Gets or sets the  AccountRestrictionList
        /// </summary>
        public List<SubAccountRestrictionUnitInput> SubAccountRestrictionList { get; set; }
    }
}


