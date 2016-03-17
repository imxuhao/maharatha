using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Accounting;
using System;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// AccountTotal is the table name in lajit
    /// </summary>
    [Table("CAPS_AccountTotal")]
    public class AccountTotalUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {      

        #region Class Property Declarations

        /// <summary>Overriding the ID column with AccountTotalId</summary>
        [Column("AccountTotalId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryID field. </summary>
        public virtual short TypeOfCategoryId { get; set; } 

        [ForeignKey("TypeOfCategoryId")]
        public TypeOfCategoryUnit TypeOfCategory { get; set; }

        /// <summary>Gets or sets the FiscalPeriodID field. </summary>
        public virtual int FiscalPeriodId { get; set; }
        
        [ForeignKey("FiscalPeriodId")]
        public FiscalPeriodUnit FiscalPeriod { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long AccountId { get; set; } 

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the ConsolCompanyID field. </summary>
        public virtual int? ConsolCompanyId { get; set; }

        /// <summary>Gets or sets the ToCompanyID field. </summary>
        public virtual int? ToCompanyId { get; set; } 

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public virtual long? SubAccountId1 { get; set; } 

        [ForeignKey("SubAccountId1")]
        public SubAccountUnit SubAccount1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public virtual long? SubAccountId2 { get; set; }

        [ForeignKey("SubAccountId2")]
        public SubAccountUnit SubAccount2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public virtual long? SubAccountId3 { get; set; }

        [ForeignKey("SubAccountId3")]
        public SubAccountUnit SubAccount3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public virtual long? SubAccountId4 { get; set; }
        [ForeignKey("SubAccountId4")]
        public SubAccountUnit SubAccount4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public virtual long? SubAccountId5 { get; set; }
        [ForeignKey("SubAccountId5")]
        public SubAccountUnit SubAccount5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public virtual long? SubAccountId6 { get; set; }
        [ForeignKey("SubAccountId6")]
        public SubAccountUnit SubAccount6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public virtual long? SubAccountId7 { get; set; }
        [ForeignKey("SubAccountId7")]
        public SubAccountUnit SubAccount7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public virtual long? SubAccountId8 { get; set; }
        [ForeignKey("SubAccountId8")]
        public SubAccountUnit SubAccount8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public virtual long? SubAccountId9 { get; set; }
        [ForeignKey("SubAccountId9")]
        public SubAccountUnit SubAccount9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public virtual long? SubAccountId10 { get; set; }
        [ForeignKey("SubAccountId10")]
        public SubAccountUnit SubAccount10 { get; set; }

        /// <summary>Gets or sets the Amount1 field. </summary>
        public virtual decimal? Amount1 { get; set; } 

        /// <summary>Gets or sets the Amount2 field. </summary>
        public virtual decimal? Amount2 { get; set; } 

        /// <summary>Gets or sets the Amount3 field. </summary>
        public virtual decimal? Amount3 { get; set; } 

        /// <summary>Gets or sets the Amount4 field. </summary>
        public virtual decimal? Amount4 { get; set; } 

        /// <summary>Gets or sets the Amount5 field. </summary>
        public virtual decimal? Amount5 { get; set; }

        /// <summary>Gets or sets the Amount6 field. </summary>
        public virtual decimal? Amount6 { get; set; }

        /// <summary>Gets or sets the Amount7 field. </summary>
        public virtual decimal? Amount7 { get; set; } 

        /// <summary>Gets or sets the Amount8 field. </summary>
        public virtual decimal? Amount8 { get; set; }

        /// <summary>Gets or sets the Amount9 field. </summary>
        public virtual decimal? Amount9 { get; set; }

        /// <summary>Gets or sets the Amount10 field. </summary>
        public virtual decimal? Amount10 { get; set; } 

        /// <summary>Gets or sets the Amount11 field. </summary>
        public virtual decimal? Amount11 { get; set; } 

        /// <summary>Gets or sets the Amount12 field. </summary>
        public virtual decimal? Amount12 { get; set; } 

        /// <summary>Gets or sets the Amount13 field. </summary>
        public virtual decimal? Amount13 { get; set; }

        /// <summary>Gets or sets the Amount14 field. </summary>
        public virtual decimal? Amount14 { get; set; }

        /// <summary>Gets or sets the Amount15 field. </summary>
        public virtual decimal? Amount15 { get; set; } 

        /// <summary>Gets or sets the Amount16 field. </summary>
        public virtual decimal? Amount16 { get; set; } 

        /// <summary>Gets or sets the Amount17 field. </summary>
        public virtual decimal? Amount17 { get; set; } 

        /// <summary>Gets or sets the Amount18 field. </summary>
        public virtual decimal? Amount18 { get; set; }

        /// <summary>Gets or sets the Amount19 field. </summary>
        public virtual decimal? Amount19 { get; set; }

        /// <summary>Gets or sets the Amount20 field. </summary>
        public virtual decimal? Amount20 { get; set; } 

        /// <summary>Gets or sets the Amount21 field. </summary>
        public virtual decimal? Amount21 { get; set; } 

        /// <summary>Gets or sets the Amount22 field. </summary>
        public virtual decimal? Amount22 { get; set; }

        /// <summary>Gets or sets the Amount23 field. </summary>
        public virtual decimal? Amount23 { get; set; } 

        /// <summary>Gets or sets the Amount24 field. </summary>
        public virtual decimal? Amount24 { get; set; }

        /// <summary>Gets or sets the Amount25 field. </summary>
        public virtual decimal? Amount25 { get; set; }

        /// <summary>Gets or sets the Amount26 field. </summary>
        public virtual decimal? Amount26 { get; set; } 

        /// <summary>Gets or sets the Amount27 field. </summary>
        public virtual decimal? Amount27 { get; set; }

        /// <summary>Gets or sets the Amount28 field. </summary>
        public virtual decimal? Amount28 { get; set; } 

        /// <summary>Gets or sets the Amount29 field. </summary>
        public virtual decimal? Amount29 { get; set; } 

        /// <summary>Gets or sets the Amount30 field. </summary>
        public virtual decimal? Amount30 { get; set; } 

        /// <summary>Gets or sets the TypeOfCurrencyID1 field. </summary>
        public virtual int? TypeOfCurrencyId1 { get; set; } 

        /// <summary>Gets or sets the TypeOfCurrencyID2 field. </summary>
        public virtual int? TypeOfCurrencyId2 { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID3 field. </summary>
        public virtual int? TypeOfCurrencyId3 { get; set; } 

        /// <summary>Gets or sets the TypeOfCurrencyID4 field. </summary>
        public virtual int? TypeOfCurrencyId4 { get; set; }

        /// <summary>Gets or sets the CurrencyRate1 field. </summary>
        public virtual double? CurrencyRate1 { get; set; } 

        /// <summary>Gets or sets the CurrencyRate2 field. </summary>
        public virtual double? CurrencyRate2 { get; set; } 

        /// <summary>Gets or sets the CurrencyRate3 field. </summary>
        public virtual double? CurrencyRate3 { get; set; } 

        /// <summary>Gets or sets the CurrencyRate4 field. </summary>
        public virtual double? CurrencyRate4 { get; set; }

        /// <summary>Gets or sets the Date1 field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? Date1 { get; set; }

        /// <summary>Gets or sets the Date2 field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? Date2 { get; set; }

        /// <summary>Gets or sets the Date3 field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? Date3 { get; set; }

        /// <summary>Gets or sets the Date4 field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? Date4 { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

    }
}
