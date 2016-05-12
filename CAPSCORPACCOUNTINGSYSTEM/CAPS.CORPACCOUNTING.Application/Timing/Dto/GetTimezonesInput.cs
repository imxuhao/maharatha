using Abp.Application.Services.Dto;
using Abp.Configuration;

namespace CAPS.CORPACCOUNTING.Timing.Dto
{
    public class GetTimezonesInput : IInputDto
    {
        public SettingScopes DefaultTimezoneScope;
    }
}
