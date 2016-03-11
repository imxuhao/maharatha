using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///  NewCOATrim is the table name in Lajit
    /// </summary>
    [Table("CAPS_NewCOATrim")]
  public class NewCOATrimUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxCoaDescLength = 50;

        /// <summary> Overriding the ID column with CoaTrimId field. </summary>
        [Column("CoaTrimId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the CoaDesc field. </summary>
        [StringLength(MaxCoaDescLength)]
        public virtual string CoaDesc { get; set; }

        /// <summary>Gets or sets the Coaid field. </summary>
        public virtual int? Coaid { get; set; }

        /// <summary>Gets or sets the LineNumber field. </summary>
        public virtual string LineNumber { get; set; }

        /// <summary>Gets or sets the LineId field. </summary>
        public virtual int? LineId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
