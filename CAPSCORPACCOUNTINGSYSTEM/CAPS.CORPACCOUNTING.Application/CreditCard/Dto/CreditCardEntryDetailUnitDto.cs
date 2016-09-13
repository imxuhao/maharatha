using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.ChargeEntry;
using System;

namespace CAPS.CORPACCOUNTING.CreditCard.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(ChargeEntryDocumentDetailUnit))]
    public class CreditCardEntryDetailUnitDto : AccountingItemUnitDto
    {
        /// <summary>Gets or sets the PurchaseOrderItemId field. </summary>   
        public  long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the POHistoryItemID field. </summary>   
        public  long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>   
        public  int? VendorId { get; set; }

        public  string Vendor { get; set; }

        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public  bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public  long? BankAccountId { get; set; }

        /// <summary>Gets or sets the BankAccount field. </summary> 
        public string BankAccount { get; set; }

        /// <summary>Gets or sets the TypeOfChargeID field. </summary>   
        public  short? TypeOfChargeId { get; set; }

        /// <summary>Gets or sets the TypeOfCharge field. </summary>   
        public string TypeOfCharge { get; set; }

        /// <summary>Gets or sets the ChargeReferenceNumber field. </summary>   
        public  string ChargeReferenceNumber { get; set; }

        /// <summary>Gets or sets the ChargeDate field. </summary>   
        public  DateTime? ChargeDate { get; set; }

        /// <summary>Gets or sets the ChargeSICCode field. </summary>   
        public  string ChargeSicCode { get; set; }

        /// <summary>Gets or sets the ChargeSENumber field. </summary>   
        public  string ChargeSeNumber { get; set; }

        /// <summary>Gets or sets the ChargeOtherInfo field. </summary>   
        public  string ChargeOtherInfo { get; set; }

        /// <summary>Gets or sets the ChargeOriginalAmount field. </summary>   
        public  decimal? ChargeOriginalAmount { get; set; }

        /// <summary>Gets or sets the ChargeRetiredAmount field. </summary>   
        public  decimal? ChargeRetiredAmount { get; set; }

        /// <summary>Gets or sets the CustomerID field. </summary>   
        public  bool IsChargeDisputed { get; set; } // IsChargeDisputed
    }
}
