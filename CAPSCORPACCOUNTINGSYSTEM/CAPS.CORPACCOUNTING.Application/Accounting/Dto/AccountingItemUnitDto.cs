using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    [AutoMapFrom(typeof(AccountingItemUnit))]
    public class AccountingItemUnitDto : IOutputDto
    {
        /// <summary>Overriding the Id column with AccountingItemId </summary>
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
       
        [Column(TypeName = "smalldatetime")]
        public DateTime? LedgerDate { get; set; }

        /// <summary>Gets or sets the LedgerYYYYMM field. </summary>
        public int? LedgerYyyymm { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long? AccountId { get; set; }

        /// <summary>Gets or sets the Account field. </summary>
        public string AccountDesc { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long? CreditAccountId { get; set; }

        /// <summary>Gets or sets the Account field. </summary>
        public string CreditAccountDesc { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public int? JobId { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public string JobDesc { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public int? CreditJobId { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public string CreditJobDesc { get; set; }

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

        /// <summary>Gets or sets the SubAccount1Desc field. </summary>
        public string SubAccount1Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public long? SubAccountId2 { get; set; }

        /// <summary>Gets or sets the SubAccount2Desc field. </summary>
        public string SubAccount2Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public long? SubAccountId3 { get; set; }

        /// <summary>Gets or sets the SubAccount3Desc field. </summary>
        public string SubAccount3Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public long? SubAccountId4 { get; set; }


        /// <summary>Gets or sets the SubAccount4Desc field. </summary>
        public string SubAccount4Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public long? SubAccountId5 { get; set; }


        /// <summary>Gets or sets the SubAccount5Desc field. </summary>
        public string SubAccount5Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public long? SubAccountId6 { get; set; }


        /// <summary>Gets or sets the SubAccount6Desc field. </summary>
        public string SubAccount6Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public long? SubAccountId7 { get; set; }


        /// <summary>Gets or sets the SubAccount7Desc field. </summary>
        public string SubAccount7Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public long? SubAccountId8 { get; set; }


        /// <summary>Gets or sets the SubAccount8Desc field. </summary>
        public string SubAccount8Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public long? SubAccountId9 { get; set; }


        /// <summary>Gets or sets the SubAccount9Desc field. </summary>
        public string SubAccount9Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public long? SubAccountId10 { get; set; }


        /// <summary>Gets or sets the SubAccount10Desc field. </summary>
        public string SubAccount10Desc { get; set; }


        /// <summary>Gets or sets the CreditSubAccountID1 field. </summary>
        public long? CreditSubAccountId1 { get; set; }

        /// <summary>Gets or sets the CreditSubAccount1Desc field. </summary>
        public string CreditSubAccount1Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID2 field. </summary>
        public long? CreditSubAccountId2 { get; set; }

        /// <summary>Gets or sets the CreditSubAccount2Desc field. </summary>
        public string CreditSubAccount2Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID3 field. </summary>
        public long? CreditSubAccountId3 { get; set; }

        /// <summary>Gets or sets the CreditSubAccount3Desc field. </summary>
        public string CreditSubAccount3Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID4 field. </summary>
        public long? CreditSubAccountId4 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount4Desc field. </summary>
        public string CreditSubAccount4Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID5 field. </summary>
        public long? CreditSubAccountId5 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount5Desc field. </summary>
        public string CreditSubAccount5Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID6 field. </summary>
        public long? CreditSubAccountId6 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount6Desc field. </summary>
        public string CreditSubAccount6Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID7 field. </summary>
        public long? CreditSubAccountId7 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount7Desc field. </summary>
        public string CreditSubAccount7Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID8 field. </summary>
        public long? CreditSubAccountId8 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount8Desc field. </summary>
        public string CreditSubAccount8Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID9 field. </summary>
        public long? CreditSubAccountId9 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount9Desc field. </summary>
        public string CreditSubAccount9Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID10 field. </summary>
        public long? CreditSubAccountId10 { get; set; }


        /// <summary>Gets or sets the CreditSubAccount10Desc field. </summary>
        public string CreditSubAccount10Desc { get; set; }

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

        /// <summary>Gets or sets the AccountingItemOrigID field. </summary>
        public long? AccountingItemOrigId { get; set; }

        /// <summary>Gets or sets the AccountingItemOrig field. </summary>
        public string AccountingItemOrig { get; set; }

        /// <summary>Gets or sets the ICTJobID field. </summary>
        public int? IctJobId { get; set; }

        /// <summary>Gets or sets the IctJob field. </summary>
        public string IctJob { get; set; }

        /// <summary>Gets or sets the ICTAccountingItemID field. </summary>
        public long? IctAccountingItemId { get; set; }

        /// <summary>Gets or sets the IctAccountingItem field. </summary>
        public string IctAccountingItem { get; set; }


        /// <summary>Gets or sets the TaxRebateID field. </summary>
        public int? TaxRebateId { get; set; }

        /// <summary>Gets or sets the TaxRebateDesc field. </summary>
        public string TaxRebateDesc { get; set; }

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

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsAccountingItemSplit field. </summary>
        public bool IsAccountingItemSplit { get; set; }


    }
}
