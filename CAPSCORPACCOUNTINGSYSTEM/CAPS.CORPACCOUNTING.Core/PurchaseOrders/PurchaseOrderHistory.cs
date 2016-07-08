using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{
    public enum ModificationType
    {
        [Display(Name = "Created")]
        Created = 1,
        [Display(Name = "Reduced")]
        Reduced = 2,
        [Display(Name = "Increased Amount")]
        IncreasedAmount = 3,
        [Display(Name = "Decreased Amount")]
        DecreasedAmount = 4,
        [Display(Name = "Line # change")]
        Linechange = 5,
        [Display(Name = "New Row Added")]
        NewRowAdded = 6,
        [Display(Name = "Closed")]
        Closed = 7
    }

    /// <summary>
    /// This is the New table to maintain the History of PurchaseOrders
    /// </summary>
    [Table("CAPS_PurchaseOrderHistory")]
    public class PurchaseOrderHistory : FullAuditedEntity<long>, IMustHaveTenant, IMustHaveOrganizationUnit
    {

        #region Class Property Declarations

        /// <summary>Overriding the Id column with AccountingItemId </summary>
        [Column("CAPS_PurchaseOrderHistoryId")]
        public override long Id { get; set; }

        public virtual long AccountingItemId { get; set; }

        [ForeignKey("AccountingItemId")]
        public virtual PurchaseOrderEntryDocumentDetailUnit PurchaseOrderEntryDocumentDetailUnit { get; set; }


        /// <summary>Gets or sets the AccountingDocumentID field. </summary>
        public virtual long? AccountingDocumentId { get; set; }

        [ForeignKey("AccountingDocumentId")]
        public virtual AccountingHeaderTransactionsUnit AccountingHeaderTransaction { get; set; }

        /// <summary>Gets or sets the TypeOfTransactionID field. </summary>
        public virtual TypeofTransaction TypeOfTransactionId { get; set; }

        /// <summary>Gets or sets the TypeOfAmountID field. </summary>
        public virtual TypeOfAmount TypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ItemLinkID field. </summary>
        public virtual long? ItemLinkId { get; set; }

        /// <summary>Gets or sets the LedgerReference field. </summary>       
        public virtual string LedgerReference { get; set; }

        /// <summary>Gets or sets the LedgerDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? LedgerDate { get; set; }

        /// <summary>Gets or sets the LedgerYYYYMM field. </summary>
        public virtual int? LedgerYyyymm { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the ItemMemo field. </summary>   
        public virtual string ItemMemo { get; set; }

        /// <summary>Gets or sets the IsAsset field. </summary>
        public virtual bool IsAsset { get; set; }

        /// <summary>Gets or sets the AccountRef1 field. </summary>        
        public virtual string AccountRef1 { get; set; }

        /// <summary>Gets or sets the AccountRef2 field. </summary>      
        public virtual string AccountRef2 { get; set; }

        /// <summary>Gets or sets the AccountRef3 field. </summary>
        public virtual string AccountRef3 { get; set; }

        /// <summary>Gets or sets the AccountRef4 field. </summary>       
        public virtual string AccountRef4 { get; set; }

        /// <summary>Gets or sets the AccountRef5 field. </summary>       
        public virtual string AccountRef5 { get; set; }

        /// <summary>Gets or sets the AccountRef6 field. </summary>      
        public virtual string AccountRef6 { get; set; }

        /// <summary>Gets or sets the AccountRef7 field. </summary>       
        public virtual string AccountRef7 { get; set; }

        /// <summary>Gets or sets the AccountRef8 field. </summary>      
        public virtual string AccountRef8 { get; set; }

        /// <summary>Gets or sets the AccountRef9 field. </summary>        
        public virtual string AccountRef9 { get; set; }

        /// <summary>Gets or sets the AccountRef10 field. </summary>        
        public virtual string AccountRef10 { get; set; }

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public virtual long? SubAccountId1 { get; set; }

        [ForeignKey("SubAccountId1")]
        public virtual SubAccountUnit SubAccount1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public virtual long? SubAccountId2 { get; set; }

        [ForeignKey("SubAccountId2")]
        public virtual SubAccountUnit SubAccount2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public virtual long? SubAccountId3 { get; set; }

        [ForeignKey("SubAccountId3")]
        public virtual SubAccountUnit SubAccount3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public virtual long? SubAccountId4 { get; set; }

        [ForeignKey("SubAccountId4")]
        public virtual SubAccountUnit SubAccount4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public virtual long? SubAccountId5 { get; set; }

        [ForeignKey("SubAccountId5")]
        public virtual SubAccountUnit SubAccount5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public virtual long? SubAccountId6 { get; set; }

        [ForeignKey("SubAccountId6")]
        public virtual SubAccountUnit SubAccount6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public virtual long? SubAccountId7 { get; set; }

        [ForeignKey("SubAccountId7")]
        public virtual SubAccountUnit SubAccount7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public virtual long? SubAccountId8 { get; set; }

        [ForeignKey("SubAccountId8")]
        public virtual SubAccountUnit SubAccount8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public virtual long? SubAccountId9 { get; set; }

        [ForeignKey("SubAccountId9")]
        public virtual SubAccountUnit SubAccount9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public virtual long? SubAccountId10 { get; set; }

        [ForeignKey("SubAccountId10")]
        public virtual SubAccountUnit SubAccount10 { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4ID field. </summary>
        public virtual Typeof1099T4? TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>Gets or sets the CompanyCurrencyAmount field. </summary>
        public virtual decimal? CompanyCurrencyAmount { get; set; }

        /// <summary>Gets or sets the CurrencyAdjustmentAmount field. </summary>
        public virtual decimal? CurrencyAdjustmentAmount { get; set; }

        /// <summary>Gets or sets the OriginalItemID field. </summary>
        public virtual long? OriginalItemId { get; set; }

        /// <summary>Gets or sets the AccountingItemIDLink field. </summary>
        public virtual long? AccountingItemIdLink { get; set; }

        /// <summary>Gets or sets the IsChanged field. </summary>
        public virtual bool IsChanged { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the ReconciliationID field. </summary>
        public virtual int? ReconciliationId { get; set; }

        [ForeignKey("ReconciliationId")]
        public virtual BankRecControlUnit BankRecControl { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigAmount field. </summary>
        public virtual decimal? AccountingItemOrigAmount { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModificationID field. </summary>
        public virtual int? AccountingItemTypeOfModificationId { get; set; }

        /// <summary>Gets or sets the SplitAccountingItemId field. </summary>
        public virtual long? SplitAccountingItemId { get; set; }

        [ForeignKey("SplitAccountingItemId")]
        public virtual AccountingItemUnit AccountingItem { get; set; }

        /// <summary>Gets or sets the ICTJobID field. </summary>
        public virtual int? IctJobId { get; set; }

        /// <summary>Gets or sets the ICTAccountingItemID field. </summary>
        public virtual long? IctAccountingItemId { get; set; }


        /// <summary>Gets or sets the TaxRebateID field. </summary>
        public virtual int? TaxRebateId { get; set; }

        /// <summary>Gets or sets the CurrencyOverrideRate field. </summary>
        public virtual double? CurrencyOverrideRate { get; set; }

        /// <summary>Gets or sets the FunctionalCurrencyAmount field. </summary>
        public virtual decimal? FunctionalCurrencyAmount { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateID field. </summary>
        public virtual short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the HomeCurAmount field. </summary>
        public virtual decimal? HomeCurAmount { get; set; }

        /// <summary>Gets or sets the CustomForexRate field. </summary>
        public virtual decimal? CustomForexRate { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsAccountingItemSplit field. </summary>
        public virtual bool IsAccountingItemSplit { get; set; }


        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public virtual bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the IsPOPurchase field. </summary>   
        public virtual bool? IsPoPurchase { get; set; }

        /// <summary>Gets or sets the IsPORental field. </summary>   
        public virtual bool? IsPoRental { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        public virtual ModificationType? ModificationTypeId { get; set; }

        /// <summary>Gets or sets the OverRelieveAmount field. </summary>
        public virtual decimal? OverRelieveAmount { get; set; }

        #endregion
    }
}
