using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using Z.EntityFramework.Plus;

namespace CAPS.CORPACCOUNTING.EFAuditLog
{
    [Table("CAPS_AuditEntry")]
    public class AuditEntry:Entity,ICreationAudited,IMayHaveTenant,IMayHaveOrganizationUnit
    {
        /// <summary>Default constructor.</summary>
        /// <remarks>Required by Entity Framework.</remarks>
        public AuditEntry()
        {
        }

        /// <summary>Constructor.</summary>
        /// <param name="parent">The audit parent.</param>
        /// <param name="entry">The object state entry.</param>

        public AuditEntry(Audit parent, ObjectStateEntry entry)
        {
            
            Entry = entry;
            Parent = parent;
            Properties = new List<AuditEntryProperty>();
            EntitySetName = entry.EntitySet.Name;
            if (!entry.IsRelationship)
            {
                EntityTypeName = entry.Entity.GetType().Name;
            }
        }

        /// <summary>Gets or sets the identifier of the audit entry.</summary>
        /// <value>The identifier of the audit entry.</value>
        
        [Column("AuditEntryID",Order = 0)]
        public override int Id { get; set; }


        /// <summary>Gets or sets who created this object.</summary>
        /// <value>Describes who created this object.</value>
        

        /// <summary>Gets or sets the delayed key.</summary>
        /// <value>The delayed key.</value>

        [NotMapped]
        internal object DelayedKey { get; set; }

        /// <summary>Gets or sets the object state entry.</summary>
        /// <value>The object state entry.</value>
        [NotMapped]

        public ObjectStateEntry Entry { get; set; }

        /// <summary>Gets or sets the name of the entity set.</summary>
        /// <value>The name of the entity set.</value>
        [Column(Order = 1)]
        [MaxLength(255)]
        public string EntitySetName { get; set; }

        /// <summary>Gets or sets the name of the entity type.</summary>
        /// <value>The name of the entity type.</value>
        [Column(Order = 2)]
        [MaxLength(255)]
        public string EntityTypeName { get; set; }

        /// <summary>Gets or sets the parent.</summary>
        /// <value>The parent.</value>

        [NotMapped]
        public Audit Parent { get; set; }



        /// <summary>Gets or sets the properties.</summary>
        /// <value>The properties.</value>
        public List<AuditEntryProperty> Properties { get; set; }

        /// <summary>Gets or sets the entry state.</summary>
        /// <value>The entry state.</value>
        [Column(Order = 3)]
        public AuditEntryState State { get; set; }

        /// <summary>Gets or sets the name of the entry state.</summary>
        /// <value>The name of the entry state.</value>
        [Column(Order = 4)]
        [MaxLength(255)]
        public string StateName
        {
            get { return State.ToString(); }
            set { State = (AuditEntryState)Enum.Parse(typeof(AuditEntryState), value); }
        }

        public int? TenantId { get; set; }
        public long? OrganizationUnitId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
