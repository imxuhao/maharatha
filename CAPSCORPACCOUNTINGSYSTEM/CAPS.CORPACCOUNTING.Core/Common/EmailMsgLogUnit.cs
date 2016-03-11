using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  EmailMsgLog is the table name in Lajit
    /// </summary>
    [Table("CAPS_EmailMsgLog")]
    public class EmailMsgLogUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxPriorityLength = 10;
        public const int MaxMailSentStatusLength = 10;

        /// <summary> Overriding the ID column with EmailMsgLogId field. </summary>
        [Column("EmailMsgLogId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryId field. </summary>
        public virtual short? TypeOfCategoryId { get; set; }
        [ForeignKey("TypeOfCategoryId")]
        public virtual TypeOfCategoryUnit TypeOfCategoryUnit { get; set; }

        /// <summary>Gets or sets the MailSubject field. </summary>
        public virtual string MailSubject { get; set; }

        /// <summary>Gets or sets the MailBody field. </summary>
        public virtual string MailBody { get; set; }

        /// <summary>Gets or sets the Priority field. </summary>
        [StringLength(MaxPriorityLength)]
        public virtual string Priority { get; set; }

        /// <summary>Gets or sets the MailSentDate field. </summary>
        public virtual DateTime? MailSentDate { get; set; }

        /// <summary>Gets or sets the MailSentStatus field. </summary>
        [StringLength(MaxMailSentStatusLength)]
        public virtual string MailSentStatus { get; set; }

        /// <summary>Gets or sets the ErrorMessage field. </summary>
        public virtual string ErrorMessage { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual long? ObjectId { get; set; }

        /// <summary>Gets or sets the TrxType field. </summary>
        public virtual int? TrxType { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
