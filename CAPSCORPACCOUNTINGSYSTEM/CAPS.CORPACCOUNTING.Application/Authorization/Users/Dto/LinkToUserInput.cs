using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class LinkToUserInput : IInputDto
    {
        public string TenancyName { get; set; }

        [Required(ErrorMessage = "The Username Or Email Address field is required.")]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}