using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(UserViewSettingsUnit))]
    public class CreateUserViewSettingsUnitInput: IInputDto
    {
        /// <summary>Gets or sets the GridId field. </summary>
        public int ViewId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public long UserId { get; set; }

        /// <summary>Gets or sets the ViewSettingName field. </summary>
        [Required]
        [StringLength(UserViewSettingsUnit.ViewSettingNameLength)]
        public string ViewName { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        [Required]
        public string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool? IsDefault { get; set; }

        public long? OrganizationUnitId { get; set; }

    }
}
