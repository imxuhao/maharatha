﻿using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput : IOutputDto
    {
        public UserLoginInfoDto User { get; set; }

        public TenantLoginInfoDto Tenant { get; set; }

        public long? UserOrganizationId { get; set; }
    }
}