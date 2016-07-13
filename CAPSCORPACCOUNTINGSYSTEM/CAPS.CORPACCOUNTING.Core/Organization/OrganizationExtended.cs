using System.ComponentModel.DataAnnotations.Schema;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Organization
{
    [Table("CAPS_OrganizationUnits")]
    public class OrganizationExtended : OrganizationUnit
    {

        public const int MaxLength = 1000;
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

        /// <summary>
        /// Gets or Sets the ConnectionSting
        /// </summary>
        public virtual int? ConnectionStringId { get; set; }

        [ForeignKey("ConnectionStringId")]
        public ConnectionStringUnit ConnectionString { get; set; }

        public OrganizationExtended()
        {

        }

        public OrganizationExtended(int?  connectionstringid)
        {
            ConnectionStringId = connectionstringid;
            DisplayName = "Default";
            Code = "00001";

        }
        public OrganizationExtended(int? tenantid, string displayname, int connectionStringid, long? parentid = default(long?)) : base(tenantid, displayname, parentid)
        {
            ConnectionStringId = connectionStringid;
        }

        public OrganizationExtended(int? tenantid, string displayname, long? parentid = default(long?), string transmittercontactname="", string transmitteremailaddress = "",
            string transmittercode = "", string transmittercontrolcode = "", string federaltaxid = "", byte[] logo=null
            ):base(tenantid, displayname, parentid)
        {
            TransmitterContactName = transmittercontactname;
            TransmitterEmailAddress = transmitteremailaddress;
            TransmitterCode = transmittercode;
            TransmitterControlCode = transmittercontrolcode;
            FederalTaxId = federaltaxid;
            Logo = logo;
        }
    }
}
