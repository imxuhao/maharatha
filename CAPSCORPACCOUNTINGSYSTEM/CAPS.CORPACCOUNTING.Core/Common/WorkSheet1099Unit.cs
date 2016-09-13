using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  WorkSheet1099 is the table name in Lajit
    /// </summary>
    [Table("CAPS_WorkSheet1099")]
   public class WorkSheet1099Unit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxSSNLength = 50;

        /// <summary> Overriding the ID column with WorkSheet1099Id field. </summary>
        [Column("WorkSheet1099Id")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual VendorUnit VendorUnit { get; set; }

        /// <summary>Gets or sets the TaxYear field. </summary>
        public virtual int? TaxYear { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4Id field. </summary>
        public virtual Typeof1099T4? TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the TypeOfCountryId field. </summary>
        public virtual short? TypeOfCountryId { get; set; }
        [ForeignKey("TypeOfCountryId")]
        public virtual TypeOfCountryUnit TypeOfCountryUnit { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>Gets or sets the IsSelectedToPrint field. </summary>
        public virtual bool IsSelectedToPrint { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsPrimaryAddress field. </summary>
        public virtual bool IsPrimaryAddress { get; set; }

        /// <summary>Gets or sets the SsnTaxId field. </summary>
        [StringLength(MaxSSNLength)]
        public virtual string SsnTaxId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public WorkSheet1099Unit()
        {
            IsSelectedToPrint = false;
            IsActive = true;
            IsPrimaryAddress = false;
        }
    }
}
