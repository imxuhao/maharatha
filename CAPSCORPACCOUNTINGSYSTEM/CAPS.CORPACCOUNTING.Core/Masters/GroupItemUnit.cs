using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
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
    /// GroupItem is the table name in Lajit
    /// </summary>
    [Table("CAPS_GroupItem")]
    public class GroupItemUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        private const int MaxCaptionLength = 20;
        private const int MaxLinkCaptionLength = 50;
        private const int MaxGroupLayoutStyleLength = 50;
        private const int MaxFontLength = 100;
        private const int MaxBorderLength = 50;
        private const int MaxFormulaLength = 500;
        private const int MaxFormatLength = 50;
       
        /// <summary> Overriding the ID column with GroupItemId field. </summary>
        [Column("GroupItemId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the GroupTotalId field. </summary>
        public virtual int GroupTotalId { get; set; }
        [ForeignKey("GroupTotalId")]
        public virtual GroupTotalUnit GroupTotalUnit { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the Sequence field. </summary>
        public virtual short Sequence { get; set; }

        /// <summary>Gets or sets the IncludeInSequenceNumber field. </summary>
        public virtual short? IncludeInSequenceNumber { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsSubTotal field. </summary>
        public virtual bool IsSubTotal { get; set; }

        /// <summary>Gets or sets the LinkCaption field. </summary>
        [MaxLength(MaxLinkCaptionLength)]
        public virtual string LinkCaption { get; set; }

        /// <summary>Gets or sets the GroupLayoutStyle field. </summary>
        [MaxLength(MaxGroupLayoutStyleLength)]
        public virtual string GroupLayoutStyle { get; set; }

        /// <summary>Gets or sets the MaintenanceBpgid field. </summary>
        public virtual int? MaintenanceBpgid { get; set; }

        /// <summary>Gets or sets the IsNegative field. </summary>
        public virtual bool IsNegative { get; set; }

        /// <summary>Gets or sets the FontName field. </summary>
        [MaxLength(MaxFontLength)]
        public virtual string FontName { get; set; }

        /// <summary>Gets or sets the FontSize field. </summary>
        [MaxLength(MaxFontLength)]
        public virtual string FontSize { get; set; }

        /// <summary>Gets or sets the RowColor field. </summary>
        [MaxLength(MaxFontLength)]
        public virtual string RowColor { get; set; }

        /// <summary>Gets or sets the BorderLineStyle field. </summary>
        [MaxLength(MaxBorderLength)]
         public virtual string BorderLineStyle { get; set; }

        /// <summary>Gets or sets the BorderWeight field. </summary>
        [MaxLength(MaxBorderLength)]
        public virtual string BorderWeight { get; set; }

        /// <summary>Gets or sets the BorderSide field. </summary>
        [MaxLength(MaxBorderLength)]
        public virtual string BorderSide { get; set; }

        /// <summary>Gets or sets the BorderColor field. </summary>
        [MaxLength(MaxBorderLength)]
        public virtual string BorderColor { get; set; }

        /// <summary>Gets or sets the IsItalicized field. </summary>
        public virtual bool? IsItalicized { get; set; }

        /// <summary>Gets or sets the IsBold field. </summary>
        public virtual bool? IsBold { get; set; }

        /// <summary>Gets or sets the CaptionIndent field. </summary>
        public virtual int? CaptionIndent { get; set; }

        /// <summary>Gets or sets the LineGap field. </summary>
        public virtual int? LineGap { get; set; }

        /// <summary>Gets or sets the XFormula field. </summary>
        [StringLength(MaxFormulaLength)]
        public virtual string XFormula { get; set; }

        /// <summary>Gets or sets the XFormat field. </summary>
        [StringLength(MaxFormatLength)]
        public virtual string XFormat { get; set; }

        /// <summary>Gets or sets the IsRowMandatory field. </summary>
        public virtual bool? IsRowMandatory { get; set; }

        /// <summary>Gets or sets the BorderLineStyleId field. </summary>
        public virtual int? BorderLineStyleId { get; set; }

        /// <summary>Gets or sets the BorderWeightId field. </summary>
        public virtual int? BorderWeightId { get; set; }

        /// <summary>Gets or sets the BorderSideId field. </summary>
        public virtual int? BorderSideId { get; set; }

        /// <summary>Gets or sets the IsJobBreakdown field. </summary>
        public virtual bool? IsJobBreakdown { get; set; }

        /// <summary>Gets or sets the IsSignReversed field. </summary>
        public virtual bool? IsSignReversed { get; set; }

        /// <summary>Gets or sets the IsLineConstant field. </summary>
        public virtual bool? IsLineConstant { get; set; }

        /// <summary>Gets or sets the IsSubTotalHidden field. </summary>
        public virtual bool? IsSubTotalHidden { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        public GroupItemUnit()
        {
            IsSubTotal = false;
            IsNegative = false;            
        }

    }
}
