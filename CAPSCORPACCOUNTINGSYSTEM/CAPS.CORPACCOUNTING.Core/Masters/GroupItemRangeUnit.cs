using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.JobCosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// GroupItemRange is the table name in Lajit
    /// </summary>
    [Table("CAPS_GroupItemRange")]
    public class GroupItemRangeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxStartingRange = 40;
        public const int MinEndingRange = 40;

        /// <summary> Overriding the ID column with CustomerPayTermsId field. </summary>
        [Column("GroupItemRangeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the GroupItemId field. </summary>
        public int GroupItemId { get; set; }

        [ForeignKey("GroupItemId")]
        public virtual GroupItemUnit GroupItemUnit { get; set; }

        /// <summary>Gets or sets the StartingRange field. </summary>
        [MaxLength(MaxStartingRange)]
        public string StartingRange { get; set; }

        /// <summary>Gets or sets the EndingRange field. </summary>
        [MaxLength(MinEndingRange)]
        public string EndingRange { get; set; }

        /// <summary>Gets or sets the IsNegative field. </summary>
        public bool IsNegative { get; set; }

        /// <summary>Gets or sets the DivisionName field. </summary>
        public string DivisionName { get; set; }

        /// <summary>Gets or sets the SelectDivisionJobID field. </summary>
        public int? SelectDivisionJobId { get; set; }

        /// <summary>Gets or sets the SelectControlCenterID field. </summary>
        public int? SelectControlCenterId { get; set; }
     
        /// <summary>Gets or sets the SelectStartRange field. </summary>
        public string SelectStartRange { get; set; }

        /// <summary>Gets or sets the SelectEndRange field. </summary>
        public string SelectEndRange { get; set; }

        /// <summary>Gets or sets the TypeOfAccountID field. </summary>
        public int? TypeOfAccountId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
