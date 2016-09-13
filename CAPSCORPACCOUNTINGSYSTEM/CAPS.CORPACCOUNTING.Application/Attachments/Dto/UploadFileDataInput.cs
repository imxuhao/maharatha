using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Attachments.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadFileDataInput
    {
        /// <summary>Gets or sets the FileName field. </summary>
        public string FileName { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the TypeOfAttachedObjectId field. </summary>
        public TypeOfAttachedObject TypeOfAttachedObjectId { get; set; }

        /// <summary>Gets or sets the FileExtension field. </summary>
        public string FileExtension { get; set; }

        /// <summary>Gets or sets the FileSize field. </summary>
        public decimal FileSize { get; set; }

        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public long ObjectId { get; set; }
    }
}
