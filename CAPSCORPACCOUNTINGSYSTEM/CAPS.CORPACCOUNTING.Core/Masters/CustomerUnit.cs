using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    ///  Enum for Type of TypeofPaymentMethod
    /// </summary>
    public enum TypeofPaymentMethod
    {
        [Display(Name = "Check")]Check = 1,
        [Display(Name = "Wire")]Wire = 2,
        [Display(Name = "Credit Card")]CreditCard = 3,
        [Display(Name = "Debit Card")]DebitCard = 4,
        [Display(Name = "PayPal")]Paypal = 5,
        [Display(Name = "Manual Check")]ManualCheck = 6,
        [Display(Name = "Instant Check")]InstantCheck = 7,
        [Display(Name = "Reversal")]Reversal = 8,
        [Display(Name = "Reissue")]Reissue = 9,
        [Display(Name = "Void")]Void = 10,
        [Display(Name = "Cash Reserve")]CashReserve = 11,
        [Display(Name = "Wire PPD-AMEX (Auto-Post CR Invoice)")]W1 = 12,
        [Display(Name = "Wire PPD-AMEX")]W2 = 13
    }
    /// <summary>
    /// Customer is the table name in lajit
    /// </summary>
    [Table("CAPS_Customer")]
    public sealed class CustomerUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum size of Description.
        /// </summary>
        public const int MaxName = 100;

        /// <summary>
        ///     Maximum size of RegionLength.
        /// </summary>
        public const int MaxNumberLength = 50;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerUnit" /> class  with no parameter.
        /// </summary>
        public CustomerUnit()
        {
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerUnit" /> class  with no parameters.
        /// </summary>
        public CustomerUnit(string lastname,string firstname,string customernumber,decimal? creditlimit, TypeofPaymentMethod? typeofpaymentmethodid,
         int? customerpaymenttermid,int? salesrepid,bool isapproved,bool isactive, long? organizationunitid)
        {
            LastName = lastname;
            FirstName = firstname;
            CustomerNumber = customernumber;
            CreditLimit = creditlimit;
            TypeofPaymentMethodId = typeofpaymentmethodid;
            SalesRepId = salesrepid;
            IsActive = isactive;
            IsApproved = isactive;
            OrganizationUnitId = organizationunitid;
            CustomerPayTermsId = customerpaymenttermid;
        }
       
        #region Class Property Declarations

        /// <summary>Overriding the ID column with CustomerId</summary>
        [Column("CustomerId")]
        public override int Id { get; set; }
        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(MaxName)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(MaxName)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the CustomerNumber field. </summary>
        [StringLength(MaxNumberLength)]
        public string CustomerNumber { get; set; }
        /// <summary>Gets or sets the CreditLimit field. </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary>
        /// Gets or Sets TypeofPaymentMethod
        /// </summary>
        public TypeofPaymentMethod? TypeofPaymentMethodId { get; set; }

        /// <summary>Gets or sets the Is CustomerPayTermsId field. </summary>
        public int? CustomerPayTermsId { get; set; }
        /// <summary>Gets or sets the Is CustomerPayTerms. </summary>
        [ForeignKey("CustomerPayTermsId")]
        public CustomerPaymentTermUnit CustomerPayTerms { get; set; }

        /// <summary>Gets or sets the Is SalesRepId field. </summary>
        public int? SalesRepId { get; set; }
        /// <summary>Gets or sets the  SalesRep. </summary>
        [ForeignKey("SalesRepId")]
        public SalesRepUnit SalesRep { get; set; }
        /// <summary>Gets or sets the Is IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion
    }
}