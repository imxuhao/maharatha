using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.MultiTenancy.Dto
{
    public class TenantListOutputDto : IDoubleWayDto
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }


    }
}
