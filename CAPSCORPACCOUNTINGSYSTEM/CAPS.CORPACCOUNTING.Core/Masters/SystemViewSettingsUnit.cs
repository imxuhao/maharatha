using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CAPS.CORPACCOUNTING.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_SystemViewSettings")]
    public class SystemViewSettingsUnit : CreationAuditedEntity 
    {
        public const int ViewSettingNameLength= 300;
        #region Class Property Declarations

        /// <summary>Overriding the ID column with UserViewId</summary>
        [Column("SystemViewId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public virtual int ViewId { get;set;}
        [ForeignKey("ViewId")]
        public virtual GridListUnit GridListUnit { get; set; }

        [Required]
        [StringLength(ViewSettingNameLength)]
        public virtual string ViewName { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        [Required]
        public virtual string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

        #endregion

        public SystemViewSettingsUnit()
        {
            
        }
        public SystemViewSettingsUnit(int viewId, string viewSettingName, string viewSettings)
        {
            ViewId = viewId;
            ViewName= viewSettingName;
            ViewSettings = viewSettings;
            IsDefault = false;
            CreationTime = System.DateTime.Now;

        }

    }
}
