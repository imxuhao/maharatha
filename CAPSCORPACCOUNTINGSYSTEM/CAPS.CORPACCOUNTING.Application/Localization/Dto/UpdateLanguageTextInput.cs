using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Localization;

namespace CAPS.CORPACCOUNTING.Localization.Dto
{
    public class UpdateLanguageTextInput : IInputDto
    {
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public string LanguageName { get; set; }

        [Required]
        [StringLength(ApplicationLanguageText.MaxSourceNameLength)]
        public string SourceName { get; set; }

        [Required]
        [StringLength(ApplicationLanguageText.MaxKeyLength)]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(ApplicationLanguageText.MaxValueLength)]
        public string Value { get; set; }
        public virtual string RegularExpression { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsMandatory { get; set; }
        public virtual long? OrganizationUnitId { get; set; }


    }
}