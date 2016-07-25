using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Configuration.Host.Dto
{
    public class HostUserManagementSettingsEditDto 
    {
        public bool IsEmailConfirmationRequiredForLogin { get; set; }
    }
}