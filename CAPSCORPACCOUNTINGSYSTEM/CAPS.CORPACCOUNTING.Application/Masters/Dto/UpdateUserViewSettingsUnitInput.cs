using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(UserViewSettingsUnit))]
    public class UpdateUserViewSettingsUnitInput : IInputDto
    {

        /// <summary>Gets or sets the UserViewId field. </summary>
        public virtual int UserViewId { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public virtual int GridId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual long UserId { get; set; }

        /// <summary>Gets or sets the ViewSettingName field. </summary>
        [Required]
        [StringLength(UserViewSettingsUnit.ViewSettingNameLength)]
        public virtual string ViewSettingName { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        [Required]
        public virtual string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }
    }
}
