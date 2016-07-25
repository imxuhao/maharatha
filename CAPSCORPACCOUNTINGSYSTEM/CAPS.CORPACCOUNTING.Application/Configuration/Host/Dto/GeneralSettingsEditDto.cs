using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Configuration.Host.Dto
{
    public class GeneralSettingsEditDto : IValidate
    {
        [MaxLength(128)]
        public string WebSiteRootAddress { get; set; }

        /// <summary>
        /// This value is only used for Audit is tracking to Data Base
        /// </summary>
        public bool AuditSaveToDB { get; set; }
        public string Timezone { get; set; }

        /// <summary>
        /// This value is only used for comparing user's timezone to default timezone
        /// </summary>
        public string TimezoneForComparison { get; set; }
    }
}