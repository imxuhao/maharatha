using System.ComponentModel.DataAnnotations.Schema;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Organization
{
    [Table("CAPS_OrganizationUnits")]
    public class OrganizationExtended : OrganizationUnit
    {

        public virtual int? LajitId { get; set; }

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
        public OrganizationExtended(int? tenantid, string displayname, int? connectionStringid, long? parentid = default(long?)) : base(tenantid, displayname, parentid)
        {
            ConnectionStringId = connectionStringid;
        }

        
    }
}
