using System.ComponentModel.DataAnnotations.Schema;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Organization
{
    public enum EntityClassification
    {
        [Display(Name = "Project")]
        Project = 1,
        [Display(Name = "Account")]
        Account = 2,
        [Display(Name = "Bank Account")]
        BankAccount = 3,
        [Display(Name = "Credit Card")]
        CreditCard = 4,
        [Display(Name = "Line")]
        Line = 5,
        [Display(Name = "Division")]
        Division = 6
    }

    [Table("CAPS_OrganizationUnits")]
    public class OrganizationExtended : OrganizationUnit
    {

        public virtual int? LajitId { get; set; }

        /// <summary>
        /// Gets or Sets the ConnectionSting
        /// </summary>
        public virtual int? ConnectionStringId { get; set; }

        public virtual EntityClassification? EntityClassificationId { get; set; }


        [ForeignKey("ConnectionStringId")]
        public ConnectionStringUnit ConnectionString { get; set; }

        public OrganizationExtended()
        {

        }

        public OrganizationExtended(int? connectionstringid)
        {
            ConnectionStringId = connectionstringid;
            DisplayName = "Default";
            Code = "00001";

        }
        public OrganizationExtended(int? tenantid, string displayname, int? connectionStringid, long? parentid = default(long?)) : base(tenantid, displayname, parentid)
        {
            ConnectionStringId = connectionStringid;
        }

        public OrganizationExtended(int? tenantid, string displayname, long? parentid = default(long?), long? objectid = null, EntityClassification? entityClassificationId = null) : base(tenantid, displayname, parentid)
        {
            EntityClassificationId = entityClassificationId;
        }




    }
}
