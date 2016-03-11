using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;
using CAPS.CORPACCOUNTING.Banking;


namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    ///  Code1099T4Type is the table name in Lajit
    /// </summary>
    [Table("CAPS_Code1099T4Type")]
  public class Code1099T4TypeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with Code1099T4TypeId field. </summary>
        [Column("Code1099T4TypeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4Id field. </summary>
        public virtual Typeof1099T4 TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary> 
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public Code1099T4TypeUnit()
        {
            IsApproved = false;
            IsActive = false;
        }
    }
}
