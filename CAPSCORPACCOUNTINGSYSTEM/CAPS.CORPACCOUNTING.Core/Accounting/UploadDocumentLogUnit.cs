using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System;

namespace CAPS.CORPACCOUNTING.Accounting
{

    /// <summary>
    /// UploadDocumentLog is the Table name in Lajit
    /// </summary>
    [Table("CAPS_UploadDocumentLog")]
    public class UploadDocumentLogUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxLength = 50;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with UploadDocumentLogId</summary>
        [Column("UploadDocumentLogId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the Description field.</summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TypeOfAccountingDocumentId field.</summary>
        public virtual TypeOfAccountingDocument TypeOfAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileID field.</summary> 
        public virtual int? TypeOfUploadFileId { get; set; } 

        [ForeignKey("TypeOfUploadFileId")]
        public TypeOfUploadFileUnit TypeOfUploadFile { get; set; }

        /// <summary>Gets or sets the InvoiceInfo field.</summary> 
        public virtual string InvoiceInfo { get; set; }

        /// <summary>Gets or sets the InvoiceDate field.</summary> 
        public virtual DateTime? InvoiceDate { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field.</summary> 
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the ControlAmount1 field.</summary> 
        public virtual decimal? ControlAmount1 { get; set; }

        /// <summary>Gets or sets the ControlAmount2 field.</summary> 
        public virtual decimal? ControlAmount2 { get; set; }

        /// <summary>Gets or sets the ControlAmount3 field.</summary> 
        public virtual decimal? ControlAmount3 { get; set; }

        /// <summary>Gets or sets the ControlAmount4 field.</summary> 
        public virtual decimal? ControlAmount4 { get; set; }

        /// <summary>Gets or sets the ControlAmount5 field.</summary> 
        public virtual decimal? ControlAmount5 { get; set; }
       
        /// <summary>Gets or sets the DateImported field.</summary> 
        public virtual DateTime? DateImported { get; set; }
       
        /// <summary>Gets or sets the ImportedByUserId field.</summary> 
        public virtual int? ImportedByUserId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion
    }
}
