using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// CurrencyTypeOfRate is the Table name in Lajit
    /// </summary>
    [Table("CAPS_CurrencyTypeOfRate")]
    public class CurrencyTypeOfRateUnit : CreationAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {       

        #region Declaration of Properties
        /// <summary>Overriding the ID column with CurrencyTypeOfRateId</summary>
        [Column("CurrencyTypeOfRateId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateID field. </summary>
        public virtual short TypeOfCurrencyRateId { get; set; }

        [ForeignKey("TypeOfCurrencyRateId")]
        public TypeOfCurrencyRateUnit TypeOfCurrencyRate { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

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
        public CurrencyTypeOfRateUnit()
        {
            IsActive = true;
            IsApproved = false;
        }
    }
}
