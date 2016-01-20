using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Configuration.Host.Dto
{
    public class HostUserManagementSettingsEditDto : IValidate
    {
        public bool IsEmailConfirmationRequiredForLogin { get; set; }
    }
}