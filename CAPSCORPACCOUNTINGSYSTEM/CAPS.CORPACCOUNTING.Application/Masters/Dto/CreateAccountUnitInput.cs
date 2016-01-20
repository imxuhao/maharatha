using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CreateAccountUnitInput : IInputDto
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(AccountUnit.MaxAccountSize)]
        
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(AccountUnit.MaxDisplayNameLength)]
        public string Caption { get; set; }
        /// <summary>Gets or sets the ChartOfAccountId field. </summary>

        [Required]
        public int ChartOfAccountId { get; set; }
        /// <summary>Gets or sets the Description field. </summary>


        [MaxLength(AccountUnit.MaxDesc)]
        public string Description { get; set; }
        /// <summary>Gets or sets the DisplaySequence field. </summary>

        public virtual int DisplaySequence { get; set; }
        /// <summary>Gets or sets the IsAccountRevalued field. </summary>


        public bool IsAccountRevalued { get; set; }
        /// <summary>Gets or sets the IsActive field. </summary>


        public bool IsApproved { get; set; } = true;
        /// <summary>Gets or sets the IsDescriptionLocked field. </summary>

        public bool IsDescriptionLocked { get; set; }
        /// <summary>Gets or sets the IsElimination field. </summary>

        public bool IsElimination { get; set; }
        /// <summary>Gets or sets the IsEnterable field. </summary>

        public bool IsEnterable { get; set; } = true;
        /// <summary>Gets or sets the IsRollupAccount field. </summary>

        public bool IsRollupAccount { get; set; }
        /// <summary>Gets or sets the IsRollupOverridable field. </summary>

        public bool IsRollupOverridable { get; set; }
        /// <summary>Gets or sets the LinkAccountId field. </summary>

        public int? LinkAccountId { get; set; } 
        /// <summary>Gets or sets the LinkJobId field. </summary>

        public int? LinkJobId { get; set; }
        /// <summary>Gets or sets the RollupAccountId field. </summary>

        public int? RollupAccountId { get; set; }
        /// <summary>Gets or sets the RollupJobId field. </summary>

        public int? RollupJobId { get; set; }
        /// <summary>Gets or sets the RowStamp field. </summary>

        public int? TypeOfAccountId { get; set; }
        /// <summary>Gets or sets the TypeOfCurrencyId field. </summary>


        public virtual bool IsActive { get; set; } = true;



    }
}
