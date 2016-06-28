using System.ComponentModel.DataAnnotations.Schema;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Organization
{
    [Table("CAPS_OrganizationUnits")]
    public class OrganizationExtended : OrganizationUnit
    {

        private const int MaxLength = 1000;
        public const int MaxTaxIdLength = 15;
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TransmitterContactName field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterContactName { get; set; }

        /// <summary>Gets or sets the TransmitterEmailAddress field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterEmailAddress { get; set; }

        /// <summary>Gets or sets the TransmitterCode field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterCode { get; set; }

        /// <summary>Gets or sets the TransmitterControlCode field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterControlCode { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        [StringLength(MaxTaxIdLength)]
        public string FederalTaxId { get; set; }

        public virtual byte[] Logo { get; set; }
    }
}
