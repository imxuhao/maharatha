using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Authorization.Dto;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}