using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class GetUserForEditOutput : IOutputDto
    {
        public Guid? ProfilePictureId { get; set; }

        public UserEditDto User { get; set; }

        public UserRoleDto[] Roles { get; set; }

        public List<TenantwithRoleDto> TenantwithRoles { get; set; }
    }
}