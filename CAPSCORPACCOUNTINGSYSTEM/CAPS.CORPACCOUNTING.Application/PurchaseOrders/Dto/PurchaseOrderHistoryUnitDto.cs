using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.PurchaseOrders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.PurchaseOrders.Dto
{
    [AutoMapFrom(typeof(PurchaseOrderHistoryUnit))]
    public class PurchaseOrderHistoryUnitDto : IOutputDto
    {
        /// <summary>Overriding the Id column with AccountingItemId </summary>
        public long PurchaseOrderHistoryId { get; set; }


        /// <summary>Gets or sets the AccountingItemId field. </summary>
        public long AccountingItemId { get; set; }

        /// <summary>Gets or sets the AccountingDocumentID field. </summary>
        public long? AccountingDocumentId { get; set; }

        /// <summary>Gets or sets the TypeOfTransactionID field. </summary>
        public TypeofTransaction TypeOfTransactionId { get; set; }

        /// <summary>Gets or sets the TypeOfTransaction field. </summary>
        public string TypeOfTransaction { get; set; }

        /// <summary>Gets or sets the TypeOfAmountID field. </summary>
        public TypeOfAmount TypeOfAmountId { get; set; }

        /// <summary>Gets or sets the TypeOfAmount  field. </summary>
        public string TypeOfAmount { get; set; }

        /// <summary>Gets or sets the ItemLinkID field. </summary>
        public long? ItemLinkId { get; set; }

        /// <summary>Gets or sets the ItemLink field. </summary>
        public string ItemLink { get; set; }

        /// <summary>Gets or sets the LedgerReference field. </summary>       
        public string LedgerReference { get; set; }

        public DateTime? LedgerDate { get; set; }

        /// <summary>Gets or sets the LedgerYYYYMM field. </summary>
        public int? LedgerYyyymm { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the AccountNumber field. </summary>
        public string AccountNumber { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the JobNumber field. </summary>
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the ItemMemo field. </summary>   
        public string ItemMemo { get; set; }

        /// <summary>Gets or sets the IsAsset field. </summary>
        public bool IsAsset { get; set; }

        /// <summary>Gets or sets the AccountRef1 field. </summary>        
        public string AccountRef1 { get; set; }

        /// <summary>Gets or sets the AccountRef2 field. </summary>      
        public string AccountRef2 { get; set; }

        /// <summary>Gets or sets the AccountRef3 field. </summary>
        public string AccountRef3 { get; set; }

        /// <summary>Gets or sets the AccountRef4 field. </summary>       
        public string AccountRef4 { get; set; }

        /// <summary>Gets or sets the AccountRef5 field. </summary>       
        public string AccountRef5 { get; set; }

        /// <summary>Gets or sets the AccountRef6 field. </summary>      
        public string AccountRef6 { get; set; }

        /// <summary>Gets or sets the AccountRef7 field. </summary>       
        public string AccountRef7 { get; set; }

        /// <summary>Gets or sets the AccountRef8 field. </summary>      
        public string AccountRef8 { get; set; }

        /// <summary>Gets or sets the AccountRef9 field. </summary>        
        public string AccountRef9 { get; set; }

        /// <summary>Gets or sets the AccountRef10 field. </summary>        
        public string AccountRef10 { get; set; }

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public long? SubAccountId1 { get; set; }

        /// <summary>Gets or sets the SubAccountNumber1 field. </summary>
        public string SubAccountNumber1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public long? SubAccountId2 { get; set; }

        /// <summary>Gets or sets the SubAccountNumber2 field. </summary>
        public string SubAccountNumber2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public long? SubAccountId3 { get; set; }

        /// <summary>Gets or sets the SubAccountNumber3 field. </summary>
        public string SubAccountNumber3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public long? SubAccountId4 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber4 field. </summary>
        public string SubAccountNumber4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public long? SubAccountId5 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber5 field. </summary>
        public string SubAccountNumber5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public long? SubAccountId6 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber6 field. </summary>
        public string SubAccountNumber6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public long? SubAccountId7 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber7 field. </summary>
        public string SubAccountNumber7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public long? SubAccountId8 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber8 field. </summary>
        public string SubAccountNumber8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public long? SubAccountId9 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber9 field. </summary>
        public string SubAccountNumber9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public long? SubAccountId10 { get; set; }


        /// <summary>Gets or sets the SubAccountNumber10 field. </summary>
        public string SubAccountNumber10 { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4ID field. </summary>
        public Typeof1099T4? TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4 field. </summary>
        public string TypeOf1099T4 { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        public decimal? Amount { get; set; }

        /// <summary>Gets or sets the CompanyCurrencyAmount field. </summary>
        public decimal? CompanyCurrencyAmount { get; set; }

        /// <summary>Gets or sets the CurrencyAdjustmentAmount field. </summary>
        public decimal? CurrencyAdjustmentAmount { get; set; }

        /// <summary>Gets or sets the OriginalItemID field. </summary>
        public long? OriginalItemId { get; set; }

        /// <summary>Gets or sets the OriginalItem field. </summary>
        public string OriginalItem { get; set; }

        /// <summary>Gets or sets the AccountingItemIDLink field. </summary>
        public long? AccountingItemIdLink { get; set; }

        /// <summary>Gets or sets the AccountingItemLink field. </summary>
        public string AccountingItemLink { get; set; }

        /// <summary>Gets or sets the IsChanged field. </summary>
        public bool IsChanged { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatus field. </summary>
        public string TypeOfInactiveStatus { get; set; }

        /// <summary>Gets or sets the ReconciliationID field. </summary>
        public int? ReconciliationId { get; set; }

        /// <summary>Gets or sets the Reconciliation field. </summary>
        public string Reconciliation { get; set; }


        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool IsEnterable { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigAmount field. </summary>
        public decimal? AccountingItemOrigAmount { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModificationID field. </summary>
        public int? AccountingItemTypeOfModificationId { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModification field. </summary>
        public string AccountingItemTypeOfModification { get; set; }

        /// <summary>Gets or sets the SplitAccountingItemId field. </summary>
        public long? SplitAccountingItemId { get; set; }


        /// <summary>Gets or sets the ICTJobID field. </summary>
        public int? IctJobId { get; set; }

        /// <summary>Gets or sets the IctJob field. </summary>
        public string IctJob { get; set; }

        /// <summary>Gets or sets the ICTAccountingItemID field. </summary>
        public long? IctAccountingItemId { get; set; }

        /// <summary>Gets or sets the IctAccountingItem field. </summary>
        public string IctAccountingItem { get; set; }

        /// <summary>Gets or sets the AccountingItemOrig field. </summary>
        public string AccountingItemOrig { get; set; }

        /// <summary>Gets or sets the TaxRebateID field. </summary>
        public int? TaxRebateId { get; set; }

        /// <summary>Gets or sets the TaxRebateNumber field. </summary>
        public string TaxRebateNumber { get; set; }

        /// <summary>Gets or sets the CurrencyOverrideRate field. </summary>
        public double? CurrencyOverrideRate { get; set; }

        /// <summary>Gets or sets the FunctionalCurrencyAmount field. </summary>
        public decimal? FunctionalCurrencyAmount { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateID field. </summary>
        public short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRate field. </summary>
        public string TypeOfCurrencyRate { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrency field. </summary>
        public string TypeOfCurrency { get; set; }

        /// <summary>Gets or sets the HomeCurAmount field. </summary>
        public decimal? HomeCurAmount { get; set; }

        /// <summary>Gets or sets the CustomForexRate field. </summary>
        public decimal? CustomForexRate { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsAccountingItemSplit field. </summary>
        public bool IsAccountingItemSplit { get; set; }


        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the IsPOPurchase field. </summary>   
        public bool? IsPoPurchase { get; set; }

        /// <summary>Gets or sets the IsPORental field. </summary>   
        public bool? IsPoRental { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        /// <summary>Gets or sets the VendorName field. </summary>   
        public string VendorName { get; set; }

        /// <summary>Gets or sets the ModificationTypeId field. </summary>
        public ModificationType? ModificationTypeId { get; set; }

        /// <summary>Gets or sets the ModificationTypeId field. </summary>
        public string ModificationType { get; set; }

        /// <summary>Gets or sets the OverRelieveAmount field. </summary>
        public decimal? OverRelieveAmount { get; set; }

        /// <summary>Gets or sets the RemainingAmount field. </summary>
        public virtual decimal? RemainingAmount { get; set; }

        /// <summary>Gets or sets the PendingAmount field. </summary>
        public virtual decimal? PendingAmount { get; set; }


        /// <summary>Gets or sets the RowNumber field. </summary>
        public virtual long? RowNumber { get; set; }

        /// <summary>Gets or sets the SourceTypeId field. </summary>
        public virtual SourceType? SourceTypeId { get; set; }

        /// <summary>Gets or sets the ModificationTypeId field. </summary>
        public string SourceType { get; set; }

        /// <summary>Get Sets the DocumentReference field.</summary>
        public virtual string DocumentReference { get; set; }

        /// <summary>
        /// Gets or sets CreatedUser
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets creationTime
        /// </summary>
        public DateTime creationTime { get; set; }
    
    }
}
