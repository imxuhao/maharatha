using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Attachments
{
    public enum TypeOfAttachedObject {      

        [Display(Name ="Excel Document")]
        ExcelDocument=1,
        [Display(Name ="Word Document")]
        WordDocument=2,
        [Display(Name ="PDF Document")]
        PDFDocument=3,
        [Display(Name ="Web Page")]
        WebPage=4,
        [Display(Name ="Text Document")]
        TextDocument=5,
        [Display(Name ="W9 Image File")]
        W9ImageFile=6,
        [Display(Name ="Image File")]
        ImageFile=7,
        [Display(Name ="A/R Logo")]
         ARLogo=8,
        [Display(Name ="Micr Check - Company Logo")]
        MicrCheckCompanyLogo=9,
        [Display(Name = "Dashboard Logo Image")]
        DashboardLogoImage=10,       
        [Display(Name ="Report Logo(White)")]
        ReportLogoWhite=11,
        [Display(Name ="Report Logo(Grey)")]
        ReportLogoGrey=12,
        [Display(Name ="MICR Logo")]
        MICRLogo=13,
        [Display(Name ="Miscellaneous")]
        Miscellaneous=14,
        [Display(Name ="Signature Image")]
        SignatureImage=15,
        [Display(Name ="Invoices")]
        Invoices=16,
        [Display(Name ="Purchase Orders")]
        PurchaseOrders=17,
        [Display(Name ="Credit Application")]
        CreditApplication=18,
        [Display(Name ="Insurance Certificate")]
        InsuranceCertificate=19,
        [Display(Name ="Contract")]
        Contract=20,
        [Display(Name ="Agency PO")]
        AgencyPO=21,
        [Display(Name ="Agency Insurance")]
        AgencyInsurance=22,
        [Display(Name = "Card Holder Signature")]
        CardHolderSignature=23
    }
  
    /// <summary>
    /// AttachedObject is the Table name in Lajit
    /// </summary>
    [Table("CAPS_AttachedObject")]
    public class AttachedObjectUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum Length of FileExtensionLength  
        /// </summary>
        public const int MaxFileExtensionLength = 20;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with AttachedObjectId</summary>
        [Column("AttachedObjectId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

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
        [Column(TypeName ="smalldatetime")]
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

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion
        public AttachedObjectUnit()
        {
            AttachedDate = DateTime.Now;
        }
    }
}
