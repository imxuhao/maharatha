using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.ChargeEntry;
using CAPS.CORPACCOUNTING.CreditCard.Dto;
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
    [AutoMapFrom(typeof(ChargeEntryDocumentUnit))]
    public class CreditCardEntryDocumentUnitDto: AccountingHeaderTransactionUnitDto
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public  int? BatchId { get; set; }

        ///<summary>Get Sets the BatchName field.</summary>
        public  string BatchName { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
        public  int? VendorId { get; set; }

        ///<summary>Get Sets the VendorName field.</summary>
        public string VendorName { get; set; }


        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public  TypeOfInvoice TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public  long? BankAccountId { get; set; }

        ///<summary>Get Sets the BankAccount field.</summary>
        public string BankAccount { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public  bool IsEnterable { get; set; }

        ///<summary>Get Sets the ApInvoiceAccountingDocId field.</summary>
        public  long? ApInvoiceAccountingDocId { get; set; }


        ///<summary>Get Sets the UploadDocumentLogId field.</summary>}
        public  long? UploadDocumentLogId { get; set; }

        ///<summary>Get Sets the UploadDocumentLog field.</summary>
        public string UploadDocumentLog { get; set; }

        ///<summary>Get Sets the IsApInvoiceGenSelected field.</summary>
        public  bool? IsApInvoiceGenSelected { get; set; }

        ///<summary>Get Sets the BuildAP field.</summary>
        public string BuildAP { get; set; }

        ///<summary>Get Sets the APGenerated field.</summary>
        public string APGenerated { get; set; }
        
    }
}
