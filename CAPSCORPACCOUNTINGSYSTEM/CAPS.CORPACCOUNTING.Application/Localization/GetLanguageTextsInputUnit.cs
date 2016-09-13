using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Localization
{
    public class GetLanguageTextsInputUnit : SearchInputDto
    {
        [Required]
        [MaxLength(ApplicationLanguageText.MaxSourceNameLength)]
        public string SourceName { get; set; }

        [StringLength(ApplicationLanguage.MaxNameLength)]
        public string BaseLanguageName { get; set; }

        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength, MinimumLength = 2)]
        public string TargetLanguageName { get; set; }

        public string TargetValueFilter { get; set; }
    }
}