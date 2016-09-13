using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// TypeOfAccount is the table name in lajit
    /// </summary>
    [Table("CAPS_TypeOfAccount")]
    public class TypeOfAccountUnit : FullAuditedEntity, IMayHaveTenant
    {
        public const int MaxDescLength = 100;
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfAccountID</summary>
        [Column("TypeOfAccountId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; } 

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfAccountClassificationID field. </summary>
        public virtual short? TypeOfAccountClassificationId { get; set; }

        [ForeignKey("TypeOfAccountClassificationId")]
        public virtual TypeOfAccountClassificationUnit TypeOfAccountClassification { get; set; }

        /// <summary>Gets or sets the IsCurrencyCodeRequired field. </summary>
        public virtual bool IsCurrencyCodeRequired { get; set; } 

        /// <summary>Gets or sets the IsPaymentType field. </summary>
        public virtual bool IsPaymentType { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int? TenantId { get; set; }


        /// <summary>Gets or sets the isEditable field. </summary>
        public virtual bool IsEditable { get; set; }
        #endregion

        public TypeOfAccountUnit()
        {
            IsCurrencyCodeRequired = false;
            IsPaymentType = false;
        }



      
        public TypeOfAccountUnit(string description, string caption, short typeOfAccountClassificationId, bool isCurrencyCodeRequired, bool isPaymentType)
        {
            Description = description;
            Caption = caption;
            TypeOfAccountClassificationId = typeOfAccountClassificationId;
            IsCurrencyCodeRequired = isCurrencyCodeRequired;
            IsPaymentType = isPaymentType;
            //TenantId = tenantId;
            CreationTime = System.DateTime.Now;
        }

    }
}
