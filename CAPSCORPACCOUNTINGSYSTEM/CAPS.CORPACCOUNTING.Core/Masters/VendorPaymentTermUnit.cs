using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// VendorPaymentTerm is the table name in lajit
    /// </summary>
    [Table("CAPS_VendorPaymentTerm")]
    public sealed class VendorPaymentTermUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Maximum size of Description. </summary>
        public const int MaxDesc = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with PaymentTermsID</summary>
        [Column("PaymentTermsId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description</summary>
        [Required]
        [StringLength(MaxDesc)]
        public string Description { get; set; }
        
        /// <summary>Gets or sets the DueDays. </summary>
        [Range(1,Int32.MaxValue)]
        public int  DueDays { get; set; }

        /// <summary>Gets or sets the DiscountDays field. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public VendorPaymentTermUnit()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorPaymentTermUnit" /> class.
        /// </summary>
        public VendorPaymentTermUnit(string description, int duedays, int? discountdays,bool isactive,long? organizationid = null)
        {
            DueDays = duedays;
            DiscountDays = discountdays;
            IsActive = isactive;
            OrganizationUnitId = organizationid;
            Description = description;
        }
    }
}