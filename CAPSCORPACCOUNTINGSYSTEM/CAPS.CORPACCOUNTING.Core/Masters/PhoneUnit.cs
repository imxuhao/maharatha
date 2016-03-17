using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    public enum TypeOfPhone
    {
        [Display(Name = "Business")]
        Business = 1,
        [Display(Name = "Home")]
        Home = 2,
        [Display(Name = "Mobile")]
        Mobile = 3,
        [Display(Name = "Other")]
        Other = 4,
        [Display(Name = "Business Fax")]
        BusinessFax = 5,
        [Display(Name = "Home Fax")]
        HomeFax = 6,
        [Display(Name = "Other Fax")]
        OtherFax = 7,
    }

    /// <summary>
    /// Phone is the table name in Lajit
    /// </summary>

    [Table("CAPS_Phone")]
    public class PhoneUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxPhoneNumberLength = 50;

        /// <summary> Overriding the ID column with PhoneId field. </summary>
        [Column("PhoneId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryId field. </summary>
        public virtual short TypeOfCategoryId { get; set; }
        [ForeignKey("TypeOfCategoryId")]
        public virtual TypeOfCategoryUnit TypeOfCategoryUnit { get; set; }

        /// <summary>Gets or sets the TypeOfPhoneId field. </summary>
        public virtual TypeOfPhone TypeOfPhoneId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual int ObjectId { get; set; }

        /// <summary>Gets or sets the ContactInfo field. </summary>
        public virtual string ContactInfo { get; set; }

        /// <summary>Gets or sets the TelephoneNumber field. </summary>
        [MaxLength(MaxPhoneNumberLength)]
        public virtual string TelephoneNumber { get; set; }

        /// <summary>Gets or sets the IsPrimary field. </summary>
        public virtual bool IsPrimary { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }


        public PhoneUnit()
        {
            IsPrimary = false;
        }
    }
}
