using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Notes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Attachments.Dto
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(AttachedObjectUnit))]
    public class AttachedObjectUnitDto
    {
        /// <summary>
        ///     Maximum Length of FileExtensionLength  
        /// </summary>
        public const int MaxFileExtensionLength = 20;


        #region Declaration of Properties

        /// <summary>Gets or sets the AttachedObjectId field. </summary>
        public long AttachedObjectId { get; set; }

        /// <summary>Gets or sets the TypeOfAttachedObjectID field. </summary>
        public virtual TypeOfAttachedObject TypeOfAttachedObjectId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectID field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectID field. </summary>
        public virtual long ObjectId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the FileName field. </summary>
        public virtual string FileName { get; set; }

        /// <summary>Gets or sets the AttachedDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime AttachedDate { get; set; }

        /// <summary>Gets or sets the FileSize field. </summary>      
        public virtual int? FileSize { get; set; }

        /// <summary>Gets or sets the FileExtension field. </summary>
        [StringLength(MaxFileExtensionLength)]
        public virtual string FileExtension { get; set; }

        /// <summary>Gets or sets the UserAttachmentFilesID field. </summary>
        public virtual Guid? UserAttachmentFilesId { get; set; }

        /// <summary>Gets or sets the IsSystemGenerated field. </summary>
        public virtual bool? IsSystemGenerated { get; set; }


        //TODO: If you want to return bytes to UI then uncomment and also uncomment in service.
        /// <summary>Gets or sets the ByteArray field. </summary>
        public byte[] Bytes { get; set; }

        /// <summary>Gets or sets the CreatedUser field. </summary>
        public string CreatedUser { get; set; }

        /// <summary>Gets or sets the CreateDateTime field. </summary>
        public DateTime CreateDateTime { get; set; }

        #endregion

    }
}
