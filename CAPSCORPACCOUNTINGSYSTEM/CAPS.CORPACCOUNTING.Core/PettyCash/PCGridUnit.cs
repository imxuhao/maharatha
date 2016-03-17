using Abp.Domain.Entities;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace CAPS.CORPACCOUNTING.PettyCash
{

    /// <summary>
    ///  PCGrid is the table name in Lajit
    /// </summary>
    [Table("CAPS_PCGrid")]
   public class PCGridUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>Gets or sets the JobID field. </summary>
        [Column("PCGridId")]
        public override long Id { get; set; }
        
        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public virtual long? JobID { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long? AccountID { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>
        public virtual long?  VendorID { get; set; }

        /// <summary>Gets or sets the AccountName field. </summary>
        public virtual string AccountName { get; set; }

        /// <summary>Gets or sets the AccountNumber field. </summary>
        public virtual string AccountNumber { get; set; }

        /// <summary>Gets or sets the JobName field. </summary>
        public virtual string JobName { get; set; }

        /// <summary>Gets or sets the JobNumber field. </summary>
        public virtual string JobNumber { get; set; }

        /// <summary>Gets or sets the ChartOfAccountID field. </summary>
        public virtual int? ChartOfAccountID { get; set; }

        /// <summary>Gets or sets the VendorName field. </summary>
        public virtual string VendorName { get; set; }

        /// <summary>Gets or sets the JobDescription field. </summary>
        public virtual string JobDescription { get; set; }

        /// <summary>Gets or sets the AccountDescription field. </summary>
        public virtual string AccountDescription { get; set; }

        /// <summary>Gets or sets the ProjectDescription field. </summary>
        public virtual string ProjectDescription { get; set; }

        /// <summary>Gets or sets the PCAccountID field. </summary>
        public virtual int? PCAccountID { get; set; }

        /// <summary>Gets or sets the PCPettyCashAccountID field. </summary>
        public virtual int? PCPettyCashAccountID { get; set; }

        /// <summary>Gets or sets the Advances field. </summary>
        public virtual decimal? Advances { get; set; }

        /// <summary>Gets or sets the AdvancesCount field. </summary>
        public virtual int? AdvancesCount { get; set; }

        /// <summary>Gets or sets the Expenses field. </summary>
        public virtual decimal? Expenses { get; set; }

        /// <summary>Gets or sets the ExpensesCount field. </summary>
        public virtual int? ExpensesCount { get; set; }

        /// <summary>Gets or sets the RETURNS field. </summary>
        public virtual decimal? RETURNS { get; set; }

        /// <summary>Gets or sets the RETURNsCount field. </summary>
        public virtual int? RETURNsCount { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
