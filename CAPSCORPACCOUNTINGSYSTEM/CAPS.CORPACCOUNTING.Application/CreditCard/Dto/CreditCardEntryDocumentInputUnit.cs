using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.ChargeEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.CreditCard.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(ChargeEntryDocumentUnit))]
    public class CreditCardEntryDocumentInputUnit
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
        public virtual int? VendorId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public virtual TypeOfInvoice TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public virtual long? BankAccountId { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public virtual bool IsEnterable { get; set; }

        ///<summary>Get Sets the ApInvoiceAccountingDocId field.</summary>
        public virtual long? ApInvoiceAccountingDocId { get; set; }

        ///<summary>Get Sets the UploadDocumentLogId field.</summary>}
        public virtual long? UploadDocumentLogId { get; set; }


        ///<summary>Get Sets the IsApInvoiceGenSelected field.</summary>
        public virtual bool? IsApInvoiceGenSelected { get; set; }

        ///<summary>Gets or sets CreditCardEntryDetailList field.</summary>
        public List<CreditCardEntryDetailInputUnit> CreditCardEntryDetailList { get; set; }

    }
}
