using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///  UploadAddress is the table name in Lajit
    /// </summary>
    [Table("CAPS_UploadAddress")]
    public class UploadAddressUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with AddressId field. </summary>
        [Column("AddressId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileId field. </summary>
        public virtual int TypeOfUploadFileId { get; set; }
        [ForeignKey("TypeOfUploadFileId")]
        public virtual TypeOfUploadFileUnit TypeOfUploadFileUnit { get; set; }

        /// <summary>Gets or sets the TypeOfAddressId field. </summary>
        public virtual TypeofAddress TypeOfAddressId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual int ObjectId { get; set; }

        /// <summary>Gets or sets the ContactInfo field. </summary>
        public virtual string ContactInfo { get; set; }

        /// <summary>Gets or sets the FirstAddress field. </summary>
        public virtual string FirstAddress { get; set; }

        /// <summary>Gets or sets the SecondAddress field. </summary>
        public virtual string SecondAddress { get; set; }

        /// <summary>Gets or sets the ThirdAddress field. </summary>
        public virtual string ThirdAddress { get; set; }

        /// <summary>Gets or sets the FourthAddress field. </summary>
        public virtual string FourthAddress { get; set; }

        /// <summary>Gets or sets the City field. </summary>
        public virtual string City { get; set; }

        /// <summary>Gets or sets the Area field. </summary>
        public virtual string Area { get; set; }

        /// <summary>Gets or sets the RegionId field. </summary>
        public virtual int? RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual RegionUnit RegionUnit { get; set; }

        /// <summary>Gets or sets the PostalCode field. </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>Gets or sets the TypeOfCountryId field. </summary>
        public virtual short? TypeOfCountryId { get; set; }
        [ForeignKey("TypeOfCountryId")]
        public virtual TypeOfCountryUnit TypeOfCountryUnit { get; set; }

        /// <summary>Gets or sets the IsPrimary field. </summary>
        public virtual bool IsPrimary { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }


        public UploadAddressUnit()
        {
            IsPrimary = false;
        }
    }
}
