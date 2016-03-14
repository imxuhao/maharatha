using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  WorkFlowLog is the table name in Lajit
    /// </summary>
    [Table("CAPS_WorkFlowLog")]
   public class WorkFlowLogUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with WorkFlowLogId field. </summary>
        [Column("WorkFlowLogId")]
        public override long Id { get; set; }
        
        /// <summary>Gets or sets the WorkFlowId field. </summary>
        public virtual long WorkFlowId { get; set; }
        [ForeignKey("WorkFlowId")]
        public virtual WorkFlowUnit WorkFlowUnit { get; set; }

        /// <summary>Gets or sets the WorkFlowStepId field. </summary>
        public virtual int? WorkFlowStepId { get; set; }
        
        /// <summary>Gets or sets the TypeOfWorkFlowStatusId field. </summary>
        public virtual short TypeOfWorkFlowStatusId { get; set; }
        
        /// <summary>Gets or sets the WorkFlowMessageId field. </summary>
        public virtual int WorkFlowMessageId { get; set; }
        
        /// <summary>Gets or sets the ReferenceInformation field. </summary>
        public virtual string ReferenceInformation { get; set; }
        
        /// <summary>Gets or sets the LogDateTime field. </summary>
        public virtual DateTime LogDateTime { get; set; }
        
        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }
        
        /// <summary>Gets or sets the RoleId field. </summary>
        public virtual int RoleId { get; set; }
        
        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public WorkFlowLogUnit()
        {
            LogDateTime = System.DateTime.Now;
        }
    }
}
