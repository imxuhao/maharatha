﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_RollupCenter")]
    public class RollupCenterUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxCaptionLenth = 500;

        /// <summary>Overriding the ID column with RollupCenterId</summary>
        #region Declaration of Properties
        [Column("RollupCenterId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLenth)]
        [Required]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public virtual int? JobId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        #endregion

        public RollupCenterUnit()
        {
        }
        public RollupCenterUnit(string caption, long? accountid, int? jobid, bool isactive, bool isapproved, long? organizationunitid)
        {
            Caption = caption;
            AccountId = accountid;
            JobId = jobid;
            IsActive = isactive;
            IsApproved = isapproved;
            OrganizationUnitId = organizationunitid;
        }
    }
}