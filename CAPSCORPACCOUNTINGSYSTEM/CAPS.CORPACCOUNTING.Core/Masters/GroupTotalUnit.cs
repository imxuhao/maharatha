using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    public enum TypeOfGroup
    {
        [Display(Name = "Account Totalling")]
        AccountTotalling = 1,
        [Display(Name = " Job Totalling")]
        JobTotalling = 2,
        [Display(Name = "Location Totalling")]
        LocationTotalling = 3,
        [Display(Name = "Set Totalling")]
        SetTotalling = 4,
        [Display(Name = "Insurance Totalling")]
        InsuranceTotalling = 5,
        [Display(Name = "Vendor Totalling")]
        VendorTotalling = 6,
        [Display(Name = "Customer Totalling")]
        CustomerTotalling = 7,
        [Display(Name = "PC Totalling")]
        PCTotalling = 8,
        [Display(Name = "AICP Internal Use")]
        AICPInternalUse = 9,
        [Display(Name = "Production Totals")]
        ProductionTotals = 10,
    }



    /// <summary>
    /// GroupTotal is the table name in Lajit
    /// </summary>
    [Table("CAPS_GroupTotal")]
    public class GroupTotalUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with GroupTotalId field. </summary>
        [Column("GroupTotalId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryId field. </summary>
        public virtual short TypeOfCategoryId { get; set; }

        [ForeignKey("TypeOfCategoryId")]
        public virtual TypeOfCategoryUnit TypeOfCategoryUnit { get; set; }

        /// <summary>Gets or sets the TypeOfGroupId field. </summary>
        public virtual TypeOfGroup TypeOfGroupId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual long? EntityId { get; set; }
       
        /// <summary>Gets or sets the ObjectID field. </summary>
        public virtual long? ObjectId { get; set; }
      
        /// <summary>Gets or sets the LinkChartOfAccountID field. </summary>
        public virtual int? LinkChartOfAccountId { get; set; }
        [ForeignKey("LinkChartOfAccountId")]
        public virtual CoaUnit LinkChartOfAccount { get; set; }

        /// <summary>Gets or sets the IsDefaultFormatUsed field. </summary>
        public virtual bool? IsDefaultFormatUsed { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
