using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Accounting;


namespace CAPS.CORPACCOUNTING.Payables
{
    [Table("CAPS_APHeaderTransactions")]
    public class ApHeaderTransactions : AccountingHeaderTransactionsUnit
    {


       #region Class Property Declarations

        ///<summary>Get Sets the Posting Date</summary>
        public virtual DateTime  CheckDate { get; set; }

        #endregion
        

    }
}
