using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("Caps_Employee")]
    public class EmployeeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum size of Description.
        /// </summary>
        public const int MaxName = 100;

        /// <summary>
        ///     Maximum size of RegionLength.
        /// </summary>
        public const int MaxTaxIdLength = 15;
        public const int MaxRegionLength = 10;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SalesRepUnit" /> class  with no parameter.
        /// </summary>
        public EmployeeUnit()
        {
        }

        public EmployeeUnit(string lastname, string ssntaxid, string firstname=null,string employeeregion = null, string federaltaxid = null, bool is1099=false,
            bool isw9Onfile = false,bool isindependantcontractor = false, bool iscorporation = false, bool isproducer = false, bool isdirector = false, bool isdirphoto = false,
            bool issetdesigner = false, bool iseditor = false,bool isartdirector = false, bool isactive = true, bool isapproved = true, long? organizationunitid = null)
        {
            LastName = lastname;
            SSNTaxId = ssntaxid;
            FirstName = firstname;
            EmployeeRegion = employeeregion;
            FederalTaxId = federaltaxid;
            Is1099 = is1099;
            IsW9OnFile= isw9Onfile;
            IsIndependantContractor = isindependantcontractor;
            IsCorporation = iscorporation;
            IsProducer = isproducer;
            IsDirector = isdirector;
            IsDirPhoto = isdirphoto;
            IsSetDesigner = issetdesigner;
            IsEditor = iseditor;
            IsArtDirector = IsArtDirector;
            IsActive = isactive;
            IsApproved = isapproved;
            OrganizationUnitId = organizationunitid;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="SalesRepUnit" /> class.
        /// </summary>
        #region Class Property Declarations

        /// <summary>Overriding the ID column with EmployeeId</summary>
        [Column("EmployeeId")]
        public override int Id { get; set; }
        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(MaxName)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(MaxName)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the [EmployeeRegion] field. </summary>
        [StringLength(MaxRegionLength)]
        public string EmployeeRegion { get; set; }
        /// <summary>Gets or sets the [SSNTaxID] field. </summary>
        [StringLength(MaxTaxIdLength)]
        public string SSNTaxId { get; set; }
        /// <summary>Gets or sets the [FederalTaxID] field. </summary>
        [StringLength(MaxTaxIdLength)]
        public string FederalTaxId { get; set; }
        /// <summary>Gets or sets the Is Is1099 field. </summary>
        public bool Is1099 { get; set; }
        /// <summary>Gets or sets the Is IsW9onFile field. </summary>
        public bool IsW9OnFile { get; set; }
        /// <summary>Gets or sets the Is IsIndependantContractor field. </summary>
        public bool IsIndependantContractor { get; set; }
        /// <summary>Gets or sets the Is IsCorporation field. </summary>
        public bool IsCorporation { get; set; }
        /// <summary>Gets or sets the Is IsProducer field. </summary>
        public bool IsProducer { get; set; }
        /// <summary>Gets or sets the Is IsDirector field. </summary>
        public bool IsDirector { get; set; }
        /// <summary>Gets or sets the Is IsDirPhoto field. </summary>
        public bool IsDirPhoto { get; set; }
        /// <summary>Gets or sets the Is IsSetDesigner field. </summary>
        public bool IsSetDesigner { get; set; }
        /// <summary>Gets or sets the Is IsEditor field. </summary>
        public bool IsEditor { get; set; }
        /// <summary>Gets or sets the Is IsArtDirector field. </summary>
        public bool IsArtDirector { get; set; }
        /// <summary>Gets or sets the Is IsApproved field. </summary>
        public bool IsApproved { get; set; }
        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; }
        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        public virtual ICollection<AddressUnit> Address { get; set; }
        #endregion
    }
}