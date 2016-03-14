using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  UploadName is the table name in Lajit
    /// </summary>
    [Table("CAPS_UploadName")]
    public class UploadNameUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        #region constants
        public const int MaxLastNameLength = 400;
        public const int MaxFirstNameLength = 100;
        public const int MaxPayToNameLength = 400;
        public const int MaxDBANameLength = 400;
        public const int MaxWireInforLength = 50;
        public const int MaxUploadIdNumberLength = 50;
        public const int MaxVendorAccountLength = 50;
        public const int MaxStudioVendorInfoLength = 50;
        public const int MaxFederalTaxIdLength = 50;
        public const int MaxSSNTaxIdLength = 50;
        #endregion

        /// <summary> Overriding the ID column with UploadNameId field. </summary>
        [Column("UploadNameId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the SelectedVendorId field. </summary>        
        public virtual int? SelectedVendorId { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileId field. </summary>
        public virtual int TypeOfUploadFileId { get; set; }
        [ForeignKey("TypeOfUploadFileId")]
        public TypeOfUploadFileUnit TypeOfUploadFile { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(MaxFirstNameLength)]
        public virtual string FirstName { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(MaxLastNameLength)]
        public virtual string LastName { get; set; }

        /// <summary>Gets or sets the PayToName field. </summary>
        [StringLength(MaxPayToNameLength)]
        public virtual string PayToName { get; set; }

        /// <summary>Gets or sets the DbaName field. </summary>
        [StringLength(MaxDBANameLength)]
        public virtual string DbaName { get; set; }

        /// <summary>Gets or sets the TypeOfVendorId field. </summary>
        public virtual TypeofVendor TypeOfVendorId { get; set; }

        /// <summary>Gets or sets the WireInfo field. </summary>
        [StringLength(MaxWireInforLength)]
        public virtual string WireInfo { get; set; }

        /// <summary>Gets or sets the UploadIdNumber field. </summary>
        [StringLength(MaxUploadIdNumberLength)]
        public virtual string UploadIdNumber { get; set; }

        /// <summary>Gets or sets the VendorAccount field. </summary>
        [StringLength(MaxVendorAccountLength)]
        public virtual string VendorAccount { get; set; }

        /// <summary>Gets or sets the StudioVendorInfo field. </summary>
        [StringLength(MaxStudioVendorInfoLength)]
        public virtual string StudioVendorInfo { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyId field. </summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the CreditLimit field. </summary>
        public virtual decimal? CreditLimit { get; set; }

        /// <summary>Gets or sets the PaymentTermId field. </summary>
        public virtual int? PaymentTermId { get; set; }

        /// <summary>Gets or sets the TypeOfPaymentMethodId field. </summary>
        public virtual TypeofPaymentMethod? TypeOfPaymentMethodId { get; set; }

        /// <summary>Gets or sets the FederalTaxId field. </summary>
        [StringLength(MaxFederalTaxIdLength)]
        public virtual string FederalTaxId { get; set; }

        /// <summary>Gets or sets the SsnTaxId field. </summary>
        [StringLength(MaxSSNTaxIdLength)]
        public virtual string SsnTaxId { get; set; }

        /// <summary>Gets or sets the IsCorporation field. </summary>
        public virtual bool IsCorporation { get; set; }

        /// <summary>Gets or sets the Is1099 field. </summary>
        public virtual bool Is1099 { get; set; }

        /// <summary>Gets or sets the IsIndependantContractor field. </summary>
        public virtual bool IsIndependantContractor { get; set; }

        /// <summary>Gets or sets the IsW9OnFIle field. </summary>
        public virtual bool IsW9OnFIle { get; set; }

        /// <summary>Gets or sets the EddContractStartDate field. </summary>

        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EddContractStartDate { get; set; }

        /// <summary>Gets or sets the EddContractStopDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EddContractStopDate { get; set; }

        /// <summary>Gets or sets the EddConctractAmount field. </summary>
        public virtual decimal? EddConctractAmount { get; set; }

        /// <summary>Gets or sets the IsEddContractOnGoing field. </summary>
        public virtual bool IsEddContractOnGoing { get; set; }

        /// <summary>Gets or sets the WorkRegionId field. </summary>
        public virtual int? WorkRegionId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4Id field. </summary>
        public virtual Typeof1099T4? TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int? EntityId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInActiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInActiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public UploadNameUnit()
        {
            IsCorporation = false;
            Is1099 = false;
            IsIndependantContractor = false;
            IsW9OnFIle = false;
            IsEddContractOnGoing = false;
            IsEnterable = true;
            EntityId = -1;
            OrganizationUnitId = -1;
            IsApproved = false;
            IsActive = true;
            TypeOfInActiveStatusId = 0;
        }
    }
}
