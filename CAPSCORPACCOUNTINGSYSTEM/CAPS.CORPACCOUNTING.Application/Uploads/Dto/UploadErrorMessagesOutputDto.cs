
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Uploads.Dto
{
    public class UploadErrorMessagesOutputDto
    {
        /// <summary>
        /// Gets or Sets RowNumber
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// Gets or Sets ErrorMessage
        /// </summary>
        public List<NameValueDto> ErrorMessage { get; set; }
    }
}
