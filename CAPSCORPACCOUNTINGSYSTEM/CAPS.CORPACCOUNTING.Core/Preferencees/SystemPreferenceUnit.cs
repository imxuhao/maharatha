using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Preferencees
{
    /// <summary>
    /// SystemPreference is the table name in Lajit
    /// </summary>
    [Table("CAPS_SystemPreference")]
    public class SystemPreferenceUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Maximum Length of PreferenceValue
        /// </summary>
        public const int MaxPreferenceLength = 200;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with SystemPreferenceId</summary>
        [Column("SystemPreferenceId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the DefaultPreferenceID field. </summary>
        public virtual int DefaultPreferenceId { get; set; }

        [ForeignKey("DefaultPreferenceId")]
        public virtual DefaultPreferenceUnit DefaultPreference { get; set; }

        /// <summary>Gets or sets the PreferenceValue field. </summary>
        [Required]
        [StringLength(MaxPreferenceLength)]
        public virtual string PreferenceValue { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int? EntityId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public SystemPreferenceUnit()
        {
            IsActive = true;
        }

    }
}
