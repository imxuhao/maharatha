using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_ARInvoiceSchedule")]
    public class ARInvoiceScheduleUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescriptionLength = 50;


        #region Declaration of Properties
        /// <summary>Overriding the ID column with ARInvoiceScheduleID</summary>
        [Column("ARInvoiceScheduleId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Percentage1 field. </summary>

        [Range(0,Int32.MaxValue)]
        public virtual int Percentage1 { get; set; }

        /// <summary>Gets or sets the Percentage2 field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int Percentage2 { get; set; }

        /// <summary>Gets or sets the Percentage3 field. </summary>
        public virtual int? Percentage3 { get; set; }

        /// <summary>Gets or sets the Percentage4 field. </summary>
        public virtual int? Percentage4 { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public ARInvoiceScheduleUnit()
        {
        }
        public ARInvoiceScheduleUnit(string description, int percentage1, int percentage2, int? percentage3, int? percentage4, bool isactive, long? organizationunitid)
        {
            Description = description;
            Percentage1 = percentage1;
            Percentage2 = percentage2;
            Percentage3 = percentage3;
            Percentage4 = percentage4;
            IsActive = isactive;
            OrganizationUnitId = organizationunitid;
        }
    }
}
