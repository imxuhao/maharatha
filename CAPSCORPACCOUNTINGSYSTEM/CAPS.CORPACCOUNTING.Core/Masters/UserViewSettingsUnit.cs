using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CAPS.CORPACCOUNTING.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_UserViewSettings")]
    public class UserViewSettingsUnit : FullAuditedEntity, IMayHaveTenant
    {
        public const int ViewSettingNameLength= 300;
        #region Class Property Declarations

        /// <summary>Overriding the ID column with UserViewId</summary>
        [Column("UserViewId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public virtual int  GridId{get;set;}
        [ForeignKey("GridId")]
        public virtual GridListUnit GridListUnit { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [StringLength(ViewSettingNameLength)]
        public virtual string ViewSettingName { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        [Required]
        public virtual string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int? TenantId { get; set; }

        #endregion

        public UserViewSettingsUnit()
        {
            
        }

        public UserViewSettingsUnit(int gridId, long? userId, string viewSettingName, string viewSettings)
        {
            GridId = gridId;
            UserId = userId;
            ViewSettings= viewSettingName;
            ViewSettings = viewSettings;
            IsDefault = false;
            TenantId = null;
        }

    }
}
