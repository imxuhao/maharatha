using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(UserViewSettingsUnit))]
    public class UpdateUserViewSettingsUnitInput : IInputDto
    {

        /// <summary>Gets or sets the UserViewId field. </summary>
        public int UserViewId { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public int GridId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public long? UserId { get; set; }

        /// <summary>Gets or sets the ViewSettingName field. </summary>
        [Required]
        [StringLength(UserViewSettingsUnit.ViewSettingNameLength)]
        public string ViewSettingName { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        [Required]
        public string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool? IsDefault { get; set; }
    }
}
