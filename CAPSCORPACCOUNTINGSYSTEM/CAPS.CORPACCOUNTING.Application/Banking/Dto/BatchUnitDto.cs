using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;


namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapFrom(typeof(BatchUnit))]
    public class BatchUnitDto : IOutputDto
    {

        /// <summary>Gets or sets the BatchId field</summary>

        public int BatchId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public  string Description { get; set; }

        /// <summary>Gets or sets the TypeOfBatchId field. </summary>
        public  TypeOfBatch TypeOfBatchId { get; set; }

        /// <summary>Gets or sets the TypeOfBatch field. </summary>
        public string TypeOfBatch { get; set; }

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

        /// <summary>Gets or sets the TypeOfInactiveStatus field. </summary>
        public string TypeOfInactiveStatus { get; set; }

        /// <summary>Gets or sets the IsBatchFinalized field. </summary>
        public  bool? IsBatchFinalized { get; set; }

        /// <summary>Gets or sets the IsUniversal field. </summary>
        public  bool? IsUniversal { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public  long? OrganizationUnitId { get; set; }

    }
}
