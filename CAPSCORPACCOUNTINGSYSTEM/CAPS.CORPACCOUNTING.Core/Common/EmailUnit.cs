using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;


namespace CAPS.CORPACCOUNTING.Common
{
    public enum TypeOfEmail
    {

        [Display(Name = "Primary Email")]
        PrimaryEmail = 1,
        [Display(Name = "2nd Email")]
        SecondEmail = 2,
        [Display(Name = "3rd Email")]
        ThirdEmail = 3,
        [Display(Name = "4th Email")]
        FourthEmail = 4,
        [Display(Name = "5th Email")]
        FifthEmail = 5,
        [Display(Name = "Alert")]
        Alert = 6,
        [Display(Name = "2nd Addtl Alert")]
        SecondAddtlAlert = 7,
        [Display(Name = "3rd Addtl Alert")]
        ThirdAddtlAlert = 8,
        [Display(Name = "4th Addtl Alert")]
        FourthAddtlAlert = 9,
        [Display(Name = "5th Addtl Alert")]
        FifthAddtlAlert = 10,
        [Display(Name = "Approval Verification Alert")]
        ApprovalVerificationAlert = 11
    }

    /// <summary>
    /// Email is the Table name in Lajit
    /// </summary>
    [Table("CAPS_Email")]
    public class EmailUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescLength = 50;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with EmailId</summary>
        [Column("EmailId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryID field. </summary>
        public TypeOfCategoryUnit TypeOfCategoryId { get; set; }

        /// <summary>Gets or sets the TypeOfEmailID field. </summary>
        public TypeOfEmail TypeOfEmailId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectID field. </summary>
        public TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectID field. </summary>
        public int ObjectId { get; set; }

        /// <summary>Gets or sets the ContactInfo field. </summary>
        public string ContactInfo { get; set; }

        /// <summary>Gets or sets the EmailAddress field. </summary>
        public string EmailAddress { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public bool IsPrimary { get; set; } // IsPrimary

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion

        public EmailUnit()
        {
            IsPrimary = false;
        }
    }
}
