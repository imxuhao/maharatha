using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users;

namespace CAPS.CORPACCOUNTING.Configuration.Host.Dto
{
    public class SendTestEmailInput : IInputDto
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}