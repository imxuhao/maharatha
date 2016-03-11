using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// Enum for TypeOfWebAddress
    /// </summary>
    public enum TypeOfWebAddress
    {
            [Display(Name = "Business Home Page")]
            BusinessHomePage=1,
            [Display(Name = "Personal Home Page")]
            PersonalHomePage=2,
            [Display(Name = "Support Webpage")]
            SupportWebpage=3
    }

    /// <summary>
    /// WebAddress is the table name in lajit
    /// </summary>
    [Table("CAPS_WebAddress")]
    public class WebAddressUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        #region Class Property Declarations

        /// <summary>Overriding the ID column with WebAddressId</summary>
        [Column("WebAddressId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryID field. </summary>
        public virtual short TypeOfCategoryId { get; set; }

        [ForeignKey("TypeOfCategoryId")]
        public TypeOfCategoryUnit TypeOfCategory { get; set; }

        /// <summary>Gets or sets the TypeOfWebAddressID field. </summary>
        public virtual TypeOfWebAddress TypeOfWebAddressId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectID field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; } 

        /// <summary>Gets or sets the ObjectID field. </summary>
        public virtual int ObjectId { get; set; }

        /// <summary>Gets or sets the ContactInfo field. </summary>
        public virtual string ContactInfo { get; set; } 

        /// <summary>Gets or sets the URLAddress field. </summary>
        public virtual string UrlAddress { get; set; } 

        /// <summary>Gets or sets the IsPrimary field. </summary>
        public virtual bool IsPrimary { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public WebAddressUnit()
        {
            IsPrimary = false;
        }
    }
}
