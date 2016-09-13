using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{
    public enum TypeOfApproval
    {
       [Display(Name = "Secure Access")]
        SecureAccess=1,
        [Display(Name = "Instant Check")]
        InstantCheck = 2,
        [Display(Name = "Invoice Amount")]
        InvoiceAmount = 3,
        [Display(Name = "Delete Vendor")]
        DeleteVendor = 4,
        [Display(Name = "Purchase Orders")]
        PurchaseOrders = 5,
        [Display(Name = "Journals")]
        Journals = 6,
        [Display(Name = "Deposit")]
        Deposit = 7,
        [Display(Name = "Petty Cash")]
        PettyCash = 8,
        [Display(Name = "Payroll")]
        Payroll = 9,
        [Display(Name = "DB/CR Card")]
        DebitORCreditCard = 10,
        [Display(Name = "Customer Invoice")]
        CustomerInvoice = 11,
        [Display(Name = "Shipping")]
        Shipping = 12,
        [Display(Name = "Check Amount")]
        CheckAmount = 13,
        [Display(Name = "Total Checks Processed")]
        TotalChecksProcessed = 14,
        [Display(Name = "Default Approval")]
        DefaultApproval = 15

    }

    /// <summary>
    /// Approval is the Table name in Lajit
    /// </summary>
    [Table("CAPS_Approval")]
    public class ApprovalUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length of Description
        /// </summary>
        public const int MaxDescLength = 100;

        // <summary>
        ///     Maximum length of Caption
        /// </summary>
        public const int MaxCaptionLength = 20;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with ApprovalId</summary>
        [Column("ApprovalId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfApprovalID field. </summary>
        public virtual TypeOfApproval TypeOfApprovalId { get; set; }

        /// <summary>Gets or sets the IsRequestUserSpecific field. </summary>
        public virtual bool IsRequestUserSpecific { get; set; }

        /// <summary>Gets or sets the IsApprovalUserSpecific field. </summary>
        public virtual bool IsApprovalUserSpecific { get; set; }

        /// <summary>Gets or sets the NumberOfApprovalsRequired field. </summary>
        public virtual short? NumberOfApprovalsRequired { get; set; }

        /// <summary>Gets or sets the RerouteTimeoutDDHHMM field. </summary>
        public virtual short? RerouteTimeoutDdhhmm { get; set; }

        /// <summary>Gets or sets the RerouteApprovalID field. </summary>
        public virtual int? RerouteApprovalId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool? IsActive { get; set; } // 

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion

        public ApprovalUnit()
        {
            IsRequestUserSpecific = false;
            IsApprovalUserSpecific = false;
            IsActive = true;
        }
    }
}
