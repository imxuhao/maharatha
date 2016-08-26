using CAPS.CORPACCOUNTING.Accounting.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.CreditCard.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditCardEntryDetailInputUnit:AccountingItemInputUnit
    {
        /// <summary>Gets or sets the PurchaseOrderItemId field. </summary>   
        public virtual long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the POHistoryItemID field. </summary>   
        public virtual long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>   
        public virtual int? VendorId { get; set; }

        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public virtual bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public virtual long? BankAccountId { get; set; }

        /// <summary>Gets or sets the TypeOfChargeID field. </summary>   
        public virtual short? TypeOfChargeId { get; set; }

        /// <summary>Gets or sets the ChargeReferenceNumber field. </summary>   
        public virtual string ChargeReferenceNumber { get; set; }

        /// <summary>Gets or sets the ChargeDate field. </summary>   
        public virtual DateTime? ChargeDate { get; set; }

        /// <summary>Gets or sets the ChargeSICCode field. </summary>   
        public virtual string ChargeSicCode { get; set; }

        /// <summary>Gets or sets the ChargeSENumber field. </summary>   
        public virtual string ChargeSeNumber { get; set; }

        /// <summary>Gets or sets the ChargeOtherInfo field. </summary>   
        public virtual string ChargeOtherInfo { get; set; }

        /// <summary>Gets or sets the ChargeOriginalAmount field. </summary>   
        public virtual decimal? ChargeOriginalAmount { get; set; }

        /// <summary>Gets or sets the ChargeRetiredAmount field. </summary>   
        public virtual decimal? ChargeRetiredAmount { get; set; }

        /// <summary>Gets or sets the CustomerID field. </summary>   
        public virtual bool IsChargeDisputed { get; set; } // IsChargeDisputed

    }
}
