using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  Code1099T4Rate is the table name in Lajit
    /// </summary>
    [Table("CAPS_Code1099T4Rate")]
   public class Code1099T4RateUnit : FullAuditedEntity<short>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with Code1099T4RateId field. </summary>
        [Column("Code1099T4RateId")]
        public override short Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual short? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4ID field. </summary>
        public virtual Typeof1099T4 TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the EffectiveDate field. </summary>
        [Column(TypeName ="smalldatetime")]
        public virtual DateTime? EffectiveDate { get; set; }

        /// <summary>Gets or sets the BoxLocation field. </summary>
        public virtual short? BoxLocation { get; set; }

        /// <summary>Gets or sets the Threshold field. </summary>
        public virtual decimal? Threshold { get; set; }

        /// <summary>Gets or sets the IsThresholdCombined field. </summary>
        public virtual bool IsThresholdCombined { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public Code1099T4RateUnit()
        {
            IsThresholdCombined = false;
        }
    }
}
