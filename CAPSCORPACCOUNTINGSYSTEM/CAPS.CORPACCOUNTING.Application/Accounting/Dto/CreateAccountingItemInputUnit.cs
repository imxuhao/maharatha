﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using AutoMapper;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
   
    public class CreateAccountingItemInputUnit : IInputDto
    {
        /// <summary>Gets or sets the AccountingDocumentID field. </summary>
        public long? AccountingDocumentId { get; set; }

        /// <summary>Gets or sets the TypeOfTransactionID field. </summary>
        [EnumDataType(typeof(TypeofTransaction))]
        public TypeofTransaction TypeOfTransactionId { get; set; }

        /// <summary>Gets or sets the TypeOfAmountID field. </summary>
        [EnumDataType(typeof(TypeOfAmount))]
        public TypeOfAmount TypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ItemLinkID field. </summary>
        public long? ItemLinkId { get; set; }

        /// <summary>Gets or sets the LedgerReference field. </summary>       
        public string LedgerReference { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LedgerDate { get; set; }

        /// <summary>Gets or sets the LedgerYYYYMM field. </summary>
        public int? LedgerYyyymm { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        //[Range(1, Int32.MaxValue)]
        public long AccountId { get; set; }

        /// <summary>Gets or sets the Account field. </summary>
     
        public string AccountDesc { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        //[Range(1, Int32.MaxValue)]
        public int JobId { get; set; }


        /// <summary>Gets or sets the Job field. </summary>
       
        public string JobDesc { get; set; }


        /// <summary>Gets or sets the CreditAccountId field. </summary>
        //[Range(1, Int32.MaxValue)]
        public long CreditAccountId { get; set; }

        /// <summary>Gets or sets the Account field. </summary>
        public string CreditAccountDesc { get; set; }

        /// <summary>Gets or sets the CreditJobId field. </summary>
        //[Range(1, Int32.MaxValue)]
        public int CreditJobId { get; set; }
       

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

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public long? SubAccountId2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public long? SubAccountId3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public long? SubAccountId4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public long? SubAccountId5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public long? SubAccountId6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public long? SubAccountId7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public long? SubAccountId8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public long? SubAccountId9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public long? SubAccountId10 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID1 field. </summary>
        public long? CreditSubAccountId1 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID2 field. </summary>
        public long? CreditSubAccountId2 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID3 field. </summary>
        public long? CreditSubAccountId3 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID4 field. </summary>
        public long? CreditSubAccountId4 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID5 field. </summary>
        public long? CreditSubAccountId5 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID6 field. </summary>
        public long? CreditSubAccountId6 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID7 field. </summary>
        public long? CreditSubAccountId7 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID8 field. </summary>
        public long? CreditSubAccountId8 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID9 field. </summary>
        public long? CreditSubAccountId9 { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID10 field. </summary>
        public long? CreditSubAccountId10 { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4ID field. </summary>
        public Typeof1099T4? TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        [Range(Double.MinValue, Double.MaxValue)]
        public decimal? Amount { get; set; }

        /// <summary>Gets or sets the CompanyCurrencyAmount field. </summary>
        public decimal? CompanyCurrencyAmount { get; set; }

        /// <summary>Gets or sets the CurrencyAdjustmentAmount field. </summary>
        public decimal? CurrencyAdjustmentAmount { get; set; }

        /// <summary>Gets or sets the OriginalItemID field. </summary>
        public long? OriginalItemId { get; set; }

        /// <summary>Gets or sets the AccountingItemIDLink field. </summary>
        public long? AccountingItemIdLink { get; set; }

        /// <summary>Gets or sets the IsChanged field. </summary>
        public bool IsChanged { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the ReconciliationID field. </summary>
        public int? ReconciliationId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool IsEnterable { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigAmount field. </summary>
        public decimal? AccountingItemOrigAmount { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModificationID field. </summary>
        public int? AccountingItemTypeOfModificationId { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigID field. </summary>
        public long? AccountingItemOrigId { get; set; }

        /// <summary>Gets or sets the ICTJobID field. </summary>
        public int? IctJobId { get; set; }

        /// <summary>Gets or sets the ICTAccountingItemID field. </summary>
        public long? IctAccountingItemId { get; set; }


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

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the HomeCurAmount field. </summary>
        public decimal? HomeCurAmount { get; set; }

        /// <summary>Gets or sets the CustomForexRate field. </summary>
        public decimal? CustomForexRate { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        [Range(1, Int64.MaxValue)]
        public long OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsAccountingItemSplit field. </summary>
        public  bool IsAccountingItemSplit { get; set; }

        /// <summary>Gets or sets the SubAccountId1Desc field. </summary>
        public string SubAccountId1Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID2Desc field. </summary>
        public string SubAccountId2Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID3Desc field. </summary>
        public string SubAccountId3Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID4Desc field. </summary>
        public string SubAccountId4Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID5Desc field. </summary>
        public string SubAccountId5Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID6Desc field. </summary>
        public string SubAccountId6Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID7Desc field. </summary>
        public string SubAccountId7Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID8Desc field. </summary>
        public string SubAccountId8Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID9Desc field. </summary>
        public string SubAccountId9Desc { get; set; }

        /// <summary>Gets or sets the SubAccountID10Desc field. </summary>
        public string SubAccountId10Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID1Desc field. </summary>
        public string CreditSubAccountId1Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID2Desc field. </summary>
        public string CreditSubAccountId2Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID3Desc field. </summary>
        public string CreditSubAccountId3Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID4Desc field. </summary>
        public string CreditSubAccountId4Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID5Desc field. </summary>
        public string CreditSubAccountId5Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID6Desc field. </summary>
        public string CreditSubAccountId6Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID7Desc field. </summary>
        public string CreditSubAccountId7Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID8Desc field. </summary>
        public string CreditSubAccountId8Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID9Desc field. </summary>
        public string CreditSubAccountId9Desc { get; set; }

        /// <summary>Gets or sets the CreditSubAccountID10Desc field. </summary>
        public string CreditSubAccountId10Desc { get; set; }
    }
}
