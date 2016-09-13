using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// TypeOfAccountClassification is the table name in lajit
    /// </summary>
    [Table("CAPS_TypeOfAccountClassification")]
    public class TypeOfAccountClassificationUnit :Entity<short>
    { 
        public const int MaxDescLength = 100;
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfAccountClassificationId</summary>
        [Column("TypeOfAccountClassificationId")]
        public short Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual short? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the IsAccountSignPositive field. </summary>
        public virtual bool IsAccountSignPositive { get; set; } 

        /// <summary>Gets or sets the IsBalanceSheetAccount field. </summary>
        public virtual bool IsBalanceSheetAccount { get; set; }


        #endregion


        public TypeOfAccountClassificationUnit() { }
        public TypeOfAccountClassificationUnit(string description, string caption, bool isAccountSignPositive, bool isBalanceSheetAccount)
        {
            Description = description;
            Caption = caption;
            IsAccountSignPositive = isAccountSignPositive;
            IsBalanceSheetAccount = isBalanceSheetAccount;
        }

    }
}
