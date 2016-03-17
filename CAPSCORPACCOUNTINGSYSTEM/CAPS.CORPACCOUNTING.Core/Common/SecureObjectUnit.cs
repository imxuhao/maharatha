using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// Enum for TypeOfSecureObject
    /// </summary>
    public enum TypeOfSecureObject
    {
        [Display(Name = "Company Access")]
        CompanyAccess = 1,
        [Display(Name = "Entity Access")]
        EntityAccess = 2,
        [Display(Name = "Document Access")]
        DocumentAccess = 3,
        [Display(Name = "Account Access")]
        AccountAccess = 4,
        [Display(Name = "Job Access")]
        JobAccess = 5,
        [Display(Name = "Row Access")]
        RowAccess = 6,
        [Display(Name = "Column Access")]
        ColumnAccess = 7
    }


    /// <summary>
    ///  SecureObject is the table name in Lajit
    /// </summary>
    [Table("CAPS_SecureObject")]
    public class SecureObjectUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with SecureObjectId field. </summary>
        [Column("SecureObjectId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the SecureAccessCategoryIdAssignedByUser field. </summary>
        public virtual int SecureAccessCategoryIdAssignedByUser { get; set; }

        /// <summary>Gets or sets the TypeOfSecureObjectId field. </summary>
        public virtual TypeOfSecureObject TypeOfSecureObjectId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual int ObjectId { get; set; }

        /// <summary>Gets or sets the ColumnIdProtectedControlId field. </summary>
        public virtual int? ColumnIdProtectedControlId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
