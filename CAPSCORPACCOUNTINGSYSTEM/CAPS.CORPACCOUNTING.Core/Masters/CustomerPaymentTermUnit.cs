using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_CustomerPaymentTerms")]
    public sealed class CustomerPaymentTermUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum size of Description.
        /// </summary>
        public const int MaxDesc = 500;

        /// <summary>
        ///     Maximum size of String.
        /// </summary>
        public const int MaxStringLength = 4000;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerPaymentTermUnit" /> class  with no parameter.
        /// </summary>
        public CustomerPaymentTermUnit()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerPaymentTermUnit" /> class.
        /// </summary>
        public CustomerPaymentTermUnit(string description,int duedays,string paymentinstruction=null,decimal? discountpercent=null,int? discountdays=null,
            string overnightinstructions=null,string wiringinstructions=null,string footermessage=null,string logocaption=null,bool isdefault=true,
            long? organizationid=null)
        {
            Description = description;
            DueDays = duedays;
            PaymentInstruction = paymentinstruction;
            DiscountPercent = discountpercent;
            DiscountDays = discountdays;
            OvernightInstructions = overnightinstructions;
            WiringInstructions = wiringinstructions;
            FooterMessage = footermessage;
            LogoCaption = logocaption;

        }

        #region Class Property Declarations

        /// <summary>Overriding the ID column with CustomerPayTermsId</summary>
        [Column("CustomerPayTemrsId")]
        public override int Id { get; set; }
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDesc)]
        public string Description { get; set; }

        /// <summary>Gets or sets the PaymentInstruction. </summary>
        [StringLength(MaxStringLength)]
        public string PaymentInstruction { get; set; }

        /// <summary>Gets or sets the DueDays. </summary>
        [Range(1,Int32.MaxValue)]
        public int DueDays { get; set; }

        /// <summary>Gets or sets the DiscountPercent. </summary>
        public decimal? DiscountPercent { get; set; }
        /// <summary>Gets or sets the DiscountDays. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the OvernightInstructions. </summary>
        [StringLength(MaxStringLength)]
        public string OvernightInstructions { get; set; }

        /// <summary>Gets or sets the WiringInstructions. </summary>
        [StringLength(MaxStringLength)]
        public string WiringInstructions { get; set; }
        /// <summary>Gets or sets the FooterMessage. </summary>
        [StringLength(MaxStringLength)]
        public string FooterMessage { get; set; }
        /// <summary>Gets or sets the LogoCaption. </summary>
        [StringLength(MaxStringLength)]
        public string LogoCaption { get; set; }
        /// <summary>Gets or sets the Is IsDefault field. </summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion
    }
}