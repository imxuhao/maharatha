using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// CurrencyRate is the table name in lajit
    /// </summary>
    [Table("CAPS_CurrencyRate")]
    public class CurrencyRateUnit : CreationAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {        

        #region Class Property Declarations

        /// <summary>Overriding the ID column with CurrencyRateId</summary>
        [Column("CurrencyRateId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual int TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the EffectiveDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime EffectiveDate { get; set; }

        /// <summary>Gets or sets the CurrencyRate field. </summary>
        public virtual double CurrencyRate { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateID field. </summary>
        public virtual short? TypeOfCurrencyRateId { get; set; } 

        /// <summary>Gets or sets the EffectiveHR field. </summary>
        public virtual short? EffectiveHr { get; set; }

        /// <summary>Gets or sets the EffectiveMin field. </summary>
        public virtual short? EffectiveMin { get; set; } 

        /// <summary>Gets or sets the FromTypeOfCurrencyID field. </summary>
        public virtual int? FromTypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion


    }
}
