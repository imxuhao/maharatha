using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Preferencees
{  
    /// <summary>
    /// DefaultPreference is the table name in Lajit
    /// </summary>
    [Table("CAPS_DefaultPreference")]
    public class DefaultPreferenceUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Maximum Length of PreferenceValue
        /// </summary>
        public const int MaxPreferenceLength = 200;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with DefaultPreferenceId</summary>
        [Column("DefaultPreferenceId")]
        public override int Id { get; set; }
       
        /// <summary>Gets or sets the TypeOfPreferenceCategoryID field. </summary>
        public virtual TypeOfPreferenceCategory TypeOfPreferenceCategoryId { get; set; } // 

        /// <summary>Gets or sets the TypeOfPreferenceID field. </summary>
        public virtual short TypeOfPreferenceId { get; set; }

        [ForeignKey("TypeOfPreferenceId")]
        public virtual TypeOfPreferenceUnit TypeOfPreference { get; set; }


        /// <summary>Gets or sets the IsRequired field. </summary>
        public virtual bool IsRequired { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public virtual bool IsPrivate { get; set; }

        /// <summary>Gets or sets the PreferenceValue field. </summary>
        [Required]
        [StringLength(MaxPreferenceLength)]
        public virtual string PreferenceValue { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the TemplateTenantID field. </summary>
        public virtual int? TemplateTenantId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public DefaultPreferenceUnit()
        {
            IsRequired = false;
            IsPrivate = false;
            IsActive = true;
        }
    }
}
