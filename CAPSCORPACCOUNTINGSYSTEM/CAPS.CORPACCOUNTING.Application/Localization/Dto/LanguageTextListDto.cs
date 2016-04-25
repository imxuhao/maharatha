using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Localization.Dto
{
    public class LanguageTextListDto : IDto
    {
        public string Key { get; set; }
        
        public string BaseValue { get; set; }
        
        public string TargetValue { get; set; }
        public virtual string RegularExpression { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsMandatory { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
    }
}