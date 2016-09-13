using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Authorization.Roles.Dto;
using CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Common;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode
        {
            get { return Role.Id.HasValue; }
        }

        public CreateOrEditRoleModalViewModel(GetRoleForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}