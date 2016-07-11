using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CAPS.CORPACCOUNTING.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_UserViewSettings")]
    public class UserViewSettingsUnit : FullAuditedEntity, IMayHaveTenant,IMayHaveOrganizationUnit
    {
        public const int ViewSettingNameLength= 300;
        #region Class Property Declarations

        /// <summary>Overriding the ID column with UserViewId</summary>
        [Column("UserViewId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public virtual int ViewId { get; set;}
        [ForeignKey("ViewId")]
        public virtual GridListUnit GridListUnit { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [StringLength(ViewSettingNameLength)]
        public virtual string ViewName { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        [Required]
        public virtual string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int? TenantId { get; set; }

        public long? OrganizationUnitId {get; set; }

        #endregion

        public UserViewSettingsUnit()
        {
            
        }

        public UserViewSettingsUnit(int viewId, string viewSettingName, string viewSettings)
        {
            viewId = viewId;
            ViewSettings= viewSettingName;
            ViewSettings = viewSettings;
            IsDefault = false;
        }

    }
}
