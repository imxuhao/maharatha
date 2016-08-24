using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// TypeOfCurrency is the table name in lajit
    /// </summary>
    [Table("CAPS_TypeOfCurrency")]
    public class TypeOfCurrencyUnit : FullAuditedEntity<short>, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 100;
        public const int MaxCodeLength = 20;
        public const int MaxCaptionLength = 10;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfCurrencyId</summary>
        [Column("TypeOfCurrencyId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the ISOCurrencyCode field. </summary>
        [Required]
        [StringLength(MaxCodeLength)]
        public virtual string ISOCurrencyCode { get; set; }

        /// <summary>Gets or sets the CurrencySymbol field. </summary>
        [StringLength(MaxCodeLength)]
        public virtual string CurrencySymbol { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        //public int? TenantId { get; set; }
        ///// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TypeOfCountryId field. </summary>
        public virtual int? CountryID { get; set; }
      

        #endregion

        public TypeOfCurrencyUnit() { }
        public TypeOfCurrencyUnit(string description, string caption, string isocurrencycode, string currencysymbol, long? organizationunitid) {
            Description = description;
            Caption = caption;
            ISOCurrencyCode = isocurrencycode;
            CurrencySymbol = currencysymbol;
          OrganizationUnitId = organizationunitid;
        }

    }
}
