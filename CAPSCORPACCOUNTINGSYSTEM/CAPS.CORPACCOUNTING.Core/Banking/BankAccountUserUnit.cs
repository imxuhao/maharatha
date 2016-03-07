using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Banking
{
    /// <summary>
    /// BankAccountUser is the table name in lajit
    /// </summary>
    [Table("CAPS_BankAccountUser")]
    public class BankAccountUserUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the Id column with BankAccountUserId</summary>
        [Column("BankAccountUserId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual long BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int UserId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion

        /// <summary>Default constructor  </summary>
        public BankAccountUserUnit() { }

        /// <summary>Parameterized constructor to initialize the properties  </summary>
        public BankAccountUserUnit(long bankaccountid, int userid, long? organizationunitid)
        {
            BankAccountId = bankaccountid;
            UserId = userid;
            OrganizationUnitId = organizationunitid;
        }
    }
}
