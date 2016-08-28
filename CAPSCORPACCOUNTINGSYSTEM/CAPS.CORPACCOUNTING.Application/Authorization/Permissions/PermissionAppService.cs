using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Authorization.Permissions.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Permissions
{
    public class PermissionAppService : CORPACCOUNTINGAppServiceBase, IPermissionAppService
    {
        public ListResultOutput<FlatPermissionDto> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();
            return new ListResultOutput<FlatPermissionDto>
            {
                Items = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList()
            };
        }
    }
}