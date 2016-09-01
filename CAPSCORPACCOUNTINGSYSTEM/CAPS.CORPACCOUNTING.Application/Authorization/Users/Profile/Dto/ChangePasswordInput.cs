using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Profile.Dto
{
    public class ChangePasswordInput : IInputDto
    {
        [Required(ErrorMessage = "The Current Password field is required.")]
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "The field New Password is required.")]
        [StringLength(User.MaxPlainPasswordLength, MinimumLength = User.MinPlainPasswordLength, ErrorMessage = "The field New Password must be a string with a minimum length of 6 and a maximum length of 32.")]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}