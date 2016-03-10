using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    public enum TypeOfVatRecovery
    {
        [Display(Name = "Miscellaneous")]
        Miscellaneous = 1,
        [Display(Name = "Car Rental")]
        CarRental = 2,
        [Display(Name = "Hotel Accommodation")]
        HotelAccommodation = 3,
        [Display(Name = "Meals")]
        Meals = 4,
        [Display(Name = "Fuel")]
        Fuel = 5,
        [Display(Name = "Telephone")]
        Telephone = 6,
        [Display(Name = "Other Business Expense")]
        OtherBusinessExpense = 7,
        [Display(Name = "Business Operating Expense")]
        BusinessOperatingExpense = 8,
        [Display(Name = "Professional Services")]
        ProfessionalServices = 9,
        [Display(Name = "Trade-Fair")]
        TradeFair = 10,
        [Display(Name = "Goods/Machines/Equipment")]
        GoodsOrMachinesOrEquipment = 11,
        [Display(Name = "Export Goods")]
        ExportGoods = 12,
        [Display(Name = "General Sales Tax")]
        GeneralSalesTax = 13,
    }



    /// <summary>
    /// ValueAddedTaxRecovery is the table name in Lajit
    /// </summary>

    [Table("CAPS_ValueAddedTaxRecovery")]
    public class ValueAddedTaxRecoveryUnit: FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with ValueAddedTaxRecoveryId field. </summary>
        [Column("ValueAddedTaxRecoveryId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the ValueAddedTaxTypeId field. </summary>
        public virtual int ValueAddedTaxTypeId { get; set; }
        [ForeignKey("ValueAddedTaxTypeId")]
        public virtual ValueAddedTaxTypeUnit ValueAddedTaxTypeUnit { get; set; }

        /// <summary>Gets or sets the TypeOfVatRecoveryId field. </summary>
        public virtual TypeOfVatRecovery TypeOfVatRecoveryId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
