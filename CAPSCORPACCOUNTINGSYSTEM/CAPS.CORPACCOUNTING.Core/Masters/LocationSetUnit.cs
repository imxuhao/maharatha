using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    public enum LocationSets
    {
        [Display(Name = "Location")]
        Location = 1,
        [Display(Name = "Set")]
        Set = 2,
    }

    [Table("CAPS_LocationSet")]
    public class LocationSetUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxNumberLength = 100;
        public const int MaxDescriptionLength = 500;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with LocationSetId</summary>
        [Column("LocationSetId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfLocationSetId field. </summary>
        public virtual LocationSets TypeOfLocationSetId { get; set; }

        /// <summary>Gets or sets the Number field. </summary>
        [StringLength(MaxNumberLength)]
        public virtual string Number { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public LocationSetUnit()
        { }

        public LocationSetUnit(LocationSets typeoflocationsetid, string number, string description,long? organizationunitid)
        {
            TypeOfLocationSetId = typeoflocationsetid;
            Number = number;
            Description = description;
            OrganizationUnitId = organizationunitid;
        }


    }
}
