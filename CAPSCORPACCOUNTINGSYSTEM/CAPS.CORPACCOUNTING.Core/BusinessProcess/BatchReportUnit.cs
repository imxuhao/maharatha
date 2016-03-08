using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    /// <summary>
    ///  BatchReport is the table name in Lajit
    /// </summary>
    [Table("CAPS_BatchReport")]
    public class BatchReportUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        private const int MaxBatchNameLength = 200;

        /// <summary> Overriding the ID column with BatchReportId field. </summary>
        [Column("BatchReportId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the BatchName field. </summary>
        [Required]
        [MaxLength(MaxBatchNameLength)]
        public virtual string BatchName { get; set; }

        /// <summary>Gets or sets the BatchSDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? BatchSDate { get; set; }

        /// <summary>Gets or sets the BatchEDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? BatchEDate { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsrptConsol field. </summary>
        public virtual bool? IsrptConsol { get; set; }

        /// <summary>Gets or sets the IsPublic field. </summary>
        public virtual bool? IsPublic { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
