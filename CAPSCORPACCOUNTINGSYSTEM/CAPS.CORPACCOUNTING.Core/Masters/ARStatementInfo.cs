using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    /// ARStatementInfo is the table name in Lajit
    /// </summary>
    [Table("CAPS_ARStatementInfo")]
    public class ARStatementInfo : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxDescriptionLength = 50;

        /// <summary> Overriding the ID column with ArStatementInfoId field. </summary>
        [Column("ArStatementInfoId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the FooterArea1 field. </summary>
        public virtual string FooterArea1 { get; set; }

        /// <summary>Gets or sets the FooterArea2 field. </summary>
        public virtual string FooterArea2 { get; set; }

        /// <summary>Gets or sets the FooterArea3 field. </summary>
        public virtual string FooterArea3 { get; set; }

        /// <summary>Gets or sets the FooterArea4 field. </summary> 
        public virtual string FooterArea4 { get; set; }

        /// <summary>Gets or sets the FooterArea5 field. </summary> 
        public virtual string FooterArea5 { get; set; }

        /// <summary>Gets or sets the FooterArea6 field. </summary> 
        public virtual string FooterArea6 { get; set; }

        /// <summary>Gets or sets the FooterArea7 field. </summary>
        public virtual string FooterArea7 { get; set; }

        /// <summary>Gets or sets the FooterArea8 field. </summary>
        public virtual string FooterArea8 { get; set; }

        /// <summary>Gets or sets the FooterArea9 field. </summary>
        public virtual string FooterArea9 { get; set; }

        /// <summary>Gets or sets the FooterArea10 field. </summary>
        public virtual string FooterArea10 { get; set; }

        /// <summary>Gets or sets the IsLogoPrinted field. </summary> 
        public virtual bool IsLogoPrinted { get; set; }

        /// <summary>Gets or sets the LogoCaption field. </summary>
        public virtual string LogoCaption { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary> 
        public virtual bool IsDefault { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary> 
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public ARStatementInfo()
        {
            IsLogoPrinted = false;
            IsDefault = false;
            IsApproved = false;
            IsActive = true;
        }
    }
}
