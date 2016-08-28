using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Authorization.Permissions.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultOutput<FlatPermissionDto> GetAllPermissions();
    }
}
