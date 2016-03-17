using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// CurrencyType is the Table name in Lajit
    /// </summary>
    [Table("CAPS_CurrencyType")]
    public class CurrencyTypeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        #region Declaration of Properties
        /// <summary>Overriding the ID column with CurrencyTypeOfRateId</summary>
        [Column("CurrencyTypeId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual int TypeOfCurrencyId { get; set; } 

        /// <summary>Gets or sets the GainAccountID field. </summary>
        public virtual int? GainAccountId { get; set; }

        /// <summary>Gets or sets the GainJobID field. </summary>
        public virtual int? GainJobId { get; set; } 

        /// <summary>Gets or sets the LossAccountID field. </summary>
        public virtual int? LossAccountId { get; set; } 

        /// <summary>Gets or sets the LossJobID field. </summary>
        public virtual int? LossJobId { get; set; } 

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int EntityId { get; set; }

        [ForeignKey("EntityId")]
        public EntityUnit EntityUnit { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }  

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion

        public CurrencyTypeUnit()
        {
            IsActive = true;
            IsApproved = false;
        }
    }
}
