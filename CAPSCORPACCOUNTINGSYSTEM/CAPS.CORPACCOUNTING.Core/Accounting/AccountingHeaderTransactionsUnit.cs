using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Accounting
{
    [Table("CAPS_AccountingHeaderTransactions")]
    public  class AccountingHeaderTransactionsUnit : FullAuditedEntity, IMustHaveTenant, IMustHaveOrganizationUnit
    {

        /// <summary> Maximum length of the  Description property.</summary>
        public const int MaxLength = 100;
        #region Class Property Declarations


        /// <summary>Overriding the Id column with BatchId </summary>
        [Column("AHTID")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxLength)]
        public virtual string Description { get; set; }

        ///<summary>Get Sets the Posting Date</summary>
        public virtual DateTime PostingDate { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long OrganizationUnitId { get; set; }

        #endregion

    }
}
