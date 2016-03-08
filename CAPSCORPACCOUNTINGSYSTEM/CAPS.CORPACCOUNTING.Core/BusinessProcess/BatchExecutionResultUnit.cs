using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    /// <summary>
    ///  BatchExecutionResult is the table name in Lajit
    /// </summary>
    [Table("CAPS_BatchExecutionResult")]
    public class BatchExecutionResultUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with BatchExecutionResultId field. </summary>
        [Column("BatchExecutionResultId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the BatchReportId field. </summary>
        public virtual int BatchReportId { get; set; }
        [ForeignKey("BatchReportId")]
        public virtual BatchReportUnit BatchReportUnit { get; set; }

        /// <summary>Gets or sets the ExecutionDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime ExecutionDate { get; set; }

        /// <summary>Gets or sets the ExecutedBy field. </summary>
        public virtual int ExecutedBy { get; set; }

        /// <summary>Gets or sets the IsSuccess field. </summary>
        public virtual bool IsSuccess { get; set; }

        /// <summary>Gets or sets the ExecutionResult field. </summary>
        [Required]
        public virtual string ExecutionResult { get; set; }

        /// <summary>Gets or sets the ReportsPath field. </summary>
        [Required]
        public virtual string ReportsPath { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
