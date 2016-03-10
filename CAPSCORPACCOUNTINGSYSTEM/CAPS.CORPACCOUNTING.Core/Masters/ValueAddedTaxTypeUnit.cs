using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    public enum TypeOfValueAddedTax
    {
        [Display(Name = "Value Added Tax")]
        ValueAddedTax = 1,
        [Display(Name = "Goods And Services Tax")]
        GoodsAndServicesTax = 2,
        [Display(Name = "Provincial Sales Tax")]
        ProvincialSalesTax = 3,
        [Display(Name = "Quebec Sales Tax")]
        QuebecSalesTax = 4,
        [Display(Name = "Harmonized Sales Tax")]
        HarmonizedSalesTax = 5,
    }


    /// <summary>
    /// ValueAddedTaxType is the table name in Lajit
    /// </summary>

    [Table("CAPS_ValueAddedTaxType")]
    public class ValueAddedTaxTypeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with ValueAddedTaxTypeId field. </summary>
        [Column("ValueAddedTaxTypeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfCountryId field. </summary>
        public virtual short TypeOfCountryId { get; set; }
        [ForeignKey("TypeOfCountryId")]
        public virtual TypeOfCountryUnit TypeOfCountryUnit { get; set; }

        /// <summary>Gets or sets the TypeOfValueAddedTaxId field. </summary>
        public virtual TypeOfValueAddedTax TypeOfValueAddedTaxId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }

}
