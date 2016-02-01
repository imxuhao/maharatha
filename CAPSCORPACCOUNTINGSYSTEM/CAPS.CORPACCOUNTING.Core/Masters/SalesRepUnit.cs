using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_SalesRep")]
    public sealed class SalesRepUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum size of Name fields.
        /// </summary>
        public const int MaxName = 100;

        /// <summary>
        ///     Maximum size of RegionLength.
        /// </summary>
        public const int MaxRegionLength = 50;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SalesRepUnit" /> class  with no parameter.
        /// </summary>
        public SalesRepUnit()
        {
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="SalesRepUnit" /> class.
        /// </summary>
        public SalesRepUnit(string lastname,string firstname=null,string region=null,bool isactive=true,long? organizationunitid=null)
        {
            LastName = lastname;
            FirstName = firstname;
            Region = region;
            IsActive = isactive;
            OrganizationUnitId = organizationunitid;
        }
        #region Class Property Declarations

        /// <summary>Overriding the ID column with SalesRepId</summary>
        [Column("SalesRepId")]
        public override int Id { get; set; }
        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(MaxName)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(MaxName)]
        public string FirstName { get; set; }
        
        /// <summary>Gets or sets the Region field. </summary>
        [StringLength(MaxRegionLength)]
        public string Region { get; set; }

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion
    }
}