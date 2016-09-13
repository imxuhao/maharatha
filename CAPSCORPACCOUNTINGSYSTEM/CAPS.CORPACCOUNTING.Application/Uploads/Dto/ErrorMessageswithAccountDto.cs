using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Uploads.Dto
{
    /// <summary>
    /// Output Dto for BulkAccountsImport
    /// </summary>
    public  class ErrorMessageswithAccountDto
    {
      /// <summary>
        /// Gets or Sets ErrorMessagesList
        /// </summary>
        public List<UploadErrorMessagesOutputDto> ErrorMessagesList { get; set; }

        /// <summary>
        /// Gets or Sets AccountsList
        /// </summary>
        public List<CreateAccountUnitInput> AccountsList { get; set; }

        /// <summary>
        /// Gets or Sets IsAnyRecordSaved
        /// </summary>
        public bool IsAnyRecordSaved { get; set; }
    }
}
