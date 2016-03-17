using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Banking
{
    /// <summary>
    /// BankAccountPaymentRange is the table name in lajit
    /// </summary>
    [Table("CAPS_BankAccountPaymentRange")]
    public class BankAccountPaymentRangeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the Id column with BankAccountPaymentRangeId</summary>
        [Column("BankAccountPaymentRangeId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public virtual long BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the StartingPaymentNumber field. </summary>
        [Range(0,Int32.MaxValue)]
        public virtual int StartingPaymentNumber { get; set; }

        /// <summary>Gets or sets the EndingPaymentNumber field. </summary>
         [Range(0,Int32.MaxValue)]
        public virtual int EndingPaymentNumber { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion
       
        /// <summary>Default constructor  </summary>
        public BankAccountPaymentRangeUnit() { }
       
        /// <summary>Parameterized constructor to initialize the properties  </summary>
        public BankAccountPaymentRangeUnit(long bankaccountid, int startingpaymentnumber, int endingpaymentnumber, long? organizationunitid)
        {
            BankAccountId = bankaccountid;
            StartingPaymentNumber = startingpaymentnumber;
            EndingPaymentNumber = endingpaymentnumber;
            OrganizationUnitId = organizationunitid;
        }

    }
}
