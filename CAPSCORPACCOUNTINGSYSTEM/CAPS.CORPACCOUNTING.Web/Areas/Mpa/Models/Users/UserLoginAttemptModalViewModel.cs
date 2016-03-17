using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Authorization.Users.Dto;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}