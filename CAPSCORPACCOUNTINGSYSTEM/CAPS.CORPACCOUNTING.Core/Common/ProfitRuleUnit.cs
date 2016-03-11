using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// Enum for TypeOfFrequency
    /// </summary>
    public enum TypeOfFrequency
    {
        [Display(Name = "Month")]
        Month = 1,
        [Display(Name = "Quarter")]
        Quarter = 2,
        [Display(Name = "Semi-Annual")]
        SemiAnnual = 3,
        [Display(Name = "Year")]
        Year = 4
    }
 

    /// <summary>
    ///  ProfitRule is the table name in Lajit
    /// </summary>
    [Table("CAPS_ProfitRule")]
    public class ProfitRuleUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescriptionLength = 400;

        /// <summary> Overriding the ID column with ProfitRuleId field. </summary>
        [Column("ProfitRuleId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual int ObjectId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the TypeOfProfitCalcId field. </summary>
        public virtual int? TypeOfProfitCalcId { get; set; }
        [ForeignKey("TypeOfProfitCalcId")]
        public virtual TypeOfProfitCalcUnit TypeOfProfitCalcUnit { get; set; }

        /// <summary>Gets or sets the JobTypeId field. </summary>
        public virtual int? JobTypeId { get; set; }

        /// <summary>Gets or sets the EffectiveDate field. </summary>
        public virtual DateTime? EffectiveDate { get; set; }

        /// <summary>Gets or sets the ContractAniversaryDate field. </summary>
        public virtual DateTime? ContractAniversaryDate { get; set; }

        /// <summary>Gets or sets the FlatProfitAmount field. </summary>
        public virtual decimal? FlatProfitAmount { get; set; }

        /// <summary>Gets or sets the DefaultOverHeadPercent field. </summary>
        public virtual decimal? DefaultOverHeadPercent { get; set; }

        /// <summary>Gets or sets the DefaultProfitPercent1 field. </summary>
        public virtual decimal? DefaultProfitPercent1 { get; set; }

        /// <summary>Gets or sets the DefaultProfitLimit1 field. </summary>
        public virtual decimal? DefaultProfitLimit1 { get; set; }

        /// <summary>Gets or sets the UnderBudgetPercent1 field. </summary>
        public virtual decimal? UnderBudgetPercent1 { get; set; }

        /// <summary>Gets or sets the DefaultProfitPercent2 field. </summary>
        public virtual decimal? DefaultProfitPercent2 { get; set; }

        /// <summary>Gets or sets the DefaultProfitLimit2 field. </summary>
        public virtual decimal? DefaultProfitLimit2 { get; set; }

        /// <summary>Gets or sets the UnderBudgetPercent2 field. </summary>
        public virtual decimal? UnderBudgetPercent2 { get; set; }

        /// <summary>Gets or sets the DefaultProfitPercent3 field. </summary>
        public virtual decimal? DefaultProfitPercent3 { get; set; }

        /// <summary>Gets or sets the DefaultProfitLimit3 field. </summary>
        public virtual decimal? DefaultProfitLimit3 { get; set; }

        /// <summary>Gets or sets the UnderBudgetPercent3 field. </summary>
        public virtual decimal? UnderBudgetPercent3 { get; set; }

        /// <summary>Gets or sets the DefaultProfitPercent4 field. </summary>
        public virtual decimal? DefaultProfitPercent4 { get; set; }

        /// <summary>Gets or sets the DefaultProfitLimit4 field. </summary>
        public virtual decimal? DefaultProfitLimit4 { get; set; }

        /// <summary>Gets or sets the UnderBudgetPercent4 field. </summary>
        public virtual decimal? UnderBudgetPercent4 { get; set; }

        /// <summary>Gets or sets the ProdExpTypeOfAmountId field. </summary>
        public virtual short? ProdExpTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ProdRevTypeOfAmountId field. </summary>
        public virtual short? ProdRevTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the AtoKOvrTypeOfAmountId field. </summary>
        public virtual short? AtoKOvrTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ProdFeeOvrTypeOfAmountId field. </summary>
        public virtual short? ProdFeeOvrTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ProdFeeTypeOfAmountId field. </summary>
        public virtual short? ProdFeeTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the DirectorFeeTypeOfAmountId field. </summary>
        public virtual short? DirectorFeeTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the AtoKTypeOfAmountId field. </summary>
        public virtual short? AtoKTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the AtoKExpense field. </summary>
        public virtual decimal? AtoKExpense { get; set; }

        /// <summary>Gets or sets the AtoKOverage field. </summary>
        public virtual decimal? AtoKOverage { get; set; }

        /// <summary>Gets or sets the ProductionFee field. </summary>
        public virtual decimal? ProductionFee { get; set; }

        /// <summary>Gets or sets the ProductionFeeOverage field. </summary>
        public virtual decimal? ProductionFeeOverage { get; set; }

        /// <summary>Gets or sets the ProductionExpense field. </summary>
        public virtual decimal? ProductionExpense { get; set; }

        /// <summary>Gets or sets the ProductionRevenue field. </summary>
        public virtual decimal? ProductionRevenue { get; set; }

        /// <summary>Gets or sets the DirectorFee field. </summary>
        public virtual decimal? DirectorFee { get; set; }

        /// <summary>Gets or sets the OverheadExpense field. </summary>
        public virtual decimal? OverheadExpense { get; set; }

        /// <summary>Gets or sets the OverheadTypeOfAmountId field. </summary>
        public virtual short? OverheadTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ProfitThresholdPercent field. </summary>
        public virtual decimal? ProfitThresholdPercent { get; set; }

        /// <summary>Gets or sets the ProfitTypeOfAmountId field. </summary>
        public virtual short? ProfitTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the IsNonProfitBypassed field. </summary>
        public virtual bool IsNonProfitBypassed { get; set; }

        /// <summary>Gets or sets the SubAccountId field. </summary>
        public virtual long? SubAccountId { get; set; }
        [ForeignKey("")]
        public virtual SubAccountUnit SubAccountUnit { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual VendorUnit VendorUnit { get; set; }

        /// <summary>Gets or sets the TypeOfFrequencyId field. </summary>
        public virtual TypeOfFrequency? TypeOfFrequencyId { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int? EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual EntityUnit EntityUnit { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeofInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeofInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public ProfitRuleUnit()
        {
            IsNonProfitBypassed = false;
            IsApproved = false;
            IsActive = true;
        }
    }
}
