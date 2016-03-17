using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// ARStatementDetail is the table name in Lajit
    /// </summary>
    [Table("CAPS_ARStatementDetail")]
    public class ArStatementDetailUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with ArStatementDetailId</summary>
        [Column("ARStatementDetailId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the AccountingDocumentID field. </summary>
        public virtual long AccountingDocumentId { get; set; } 
        [ForeignKey("AccountingDocumentId")]
        public AccountingHeaderTransactionsUnit AccountingDocument { get; set; }

        /// <summary>Gets or sets the SequenceNumber field. </summary>
        public virtual int? SequenceNumber { get; set; } 

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }  

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; } 

        /// <summary>Gets or sets the IsShaded field. </summary>
        public virtual bool IsShaded { get; set; } 

        /// <summary>Gets or sets the IsUnderlined field. </summary>
        public virtual bool IsUnderlined { get; set; }  

        /// <summary>Gets or sets the IsLargeFont field. </summary>
        public virtual bool IsLargeFont { get; set; } 

        /// <summary>Gets or sets the IsSmallFont field. </summary>
        public virtual bool IsSmallFont { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual bool TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion
        public ArStatementDetailUnit()
        {
            IsShaded = false;
            IsUnderlined = false;
            IsLargeFont = false;
            IsSmallFont = false;
            IsActive = true;
            TypeOfInactiveStatusId = false;
        }
    }
}
