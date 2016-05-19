using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapTo(typeof(BatchUnit))]
    public class CreateBatchUnitInput : IInputDto
    {
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(BatchUnit.MaxLength)]
        public  string Description { get; set; }

        /// <summary>Gets or sets the TypeOfBatchId field. </summary>
        [EnumDataType(typeof(TypeOfBatch))]
        public  TypeOfBatch TypeOfBatchId { get; set; }

        /// <summary>Gets or sets the DefaultTransactionDate field. </summary>
        public  DateTime? DefaultTransactionDate { get; set; }

        /// <summary>Gets or sets the DefaultCheckDate field. </summary>
     
        public  DateTime? DefaultCheckDate { get; set; }

        /// <summary>Gets or sets the PostingDate field. </summary>
      
        public  DateTime? PostingDate { get; set; }

        /// <summary>Gets or sets the ControlTotal field. </summary>
        public  decimal? ControlTotal { get; set; }

        /// <summary>Gets or sets the RecurMonthIncrement field. </summary>
        public  int? RecurMonthIncrement { get; set; }

        /// <summary>Gets or sets the IsRetained field. </summary>
        public  bool IsRetained { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public  bool IsDefault { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public  bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public  TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsBatchFinalized field. </summary>
        public  bool? IsBatchFinalized { get; set; }

        /// <summary>Gets or sets the IsUniversal field. </summary>
        public  bool? IsUniversal { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public  long? OrganizationUnitId { get; set; }
    }
}
