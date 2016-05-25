using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///  Region is the table name in Lajit
    /// </summary>
    [Table("CAPS_Region")]
    public class RegionUnit: FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxDescriptionLength = 200;
        private const int MaxRegionAbbreviationLength = 10;
        private const int MaxStateCodeLength = 2;

        /// <summary> Overriding the ID column with RegionId field. </summary>
        [Column("RegionId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the RegionAbbreviation field. </summary>
        [MaxLength(MaxRegionAbbreviationLength)]
        public virtual string RegionAbbreviation { get; set; }

        /// <summary>Gets or sets the TypeOfCountryId field. </summary>
        public virtual short? TypeOfCountryId { get; set; }
        [ForeignKey("TypeOfCountryId")]
        public virtual TypeOfCountryUnit TypeOfCountryUnit { get; set; }

        /// <summary>Gets or sets the StateCode field. </summary>
        [MaxLength(MaxStateCodeLength)]
        public virtual string StateCode { get; set; }
        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public RegionUnit()
        { }

        public RegionUnit(string description,string regionAbbreviation,string stateCode,int tenantId)
        {
            Description = description;
            RegionAbbreviation = regionAbbreviation;
            TypeOfCountryId = null;
            StateCode = StateCode;
            TenantId = tenantId;
        }
    }
}
