using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapTo(typeof(BankAccountUnit))]
    public class CreateBankAccountUnitInput : IInputDto
    {
      
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the TypeOfBankAccountId field. </summary>
        public virtual TypeOfBankAccount TypeOfBankAccountId { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }

        /// <summary>Gets or sets the BankAccountName field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string BankAccountName { get; set; }

        /// <summary>Gets or sets the BankAccountNumber field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the RoutingNumber field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string RoutingNumber { get; set; }

        /// <summary>Gets or sets the TypeOfCheckStockId field. </summary>
        public virtual int? TypeOfCheckStockId { get; set; }

        /// <summary>Gets or sets the LastCheckNumberGenerated field. </summary>
        public virtual long? LastCheckNumberGenerated { get; set; }

        /// <summary>Gets or sets the ControlAccount field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string ControlAccount { get; set; }

        /// <summary>Gets or sets the ClearingAccountId field. </summary>
        public virtual long? ClearingAccountId { get; set; }

        /// <summary>Gets or sets the ClearingJobId field. </summary>
        public virtual int? ClearingJobId { get; set; }

        /// <summary>Gets or sets the MaxExpirationLength field. </summary>
        [StringLength(BankAccountUnit.MaxExpirationLength)]
        public virtual string ExpirationMMYYYY { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileId field. </summary>
        public virtual int? TypeOfUploadFileId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }

        /// <summary>Gets or sets the ControllingBankAccountId field. </summary>
        public virtual long? ControllingBankAccountId { get; set; }

        /// <summary>Gets or sets the IsClosed field. </summary>
        public virtual bool IsClosed { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the PositivePayTypeOfUploadFileId field. </summary>
        public virtual int? PositivePayTypeOfUploadFileId { get; set; }

        /// <summary>Gets or sets the PositivePayTransmitterInfo field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string PositivePayTransmitterInfo { get; set; }

        /// <summary>Gets or sets the PettyCashAccountId field. </summary>
        public virtual long? PettyCashAccountId { get; set; }

        /// <summary>Gets or sets the IsACHEnabled field. </summary>
        public virtual bool? IsACHEnabled { get; set; }

        /// <summary>Gets or sets the ACHDestinationCode field. </summary>
        [StringLength(BankAccountUnit.MaxAccountLength)]
        public virtual string ACHDestinationCode { get; set; }

        /// <summary>Gets or sets the ACHDestinationName field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string ACHDestinationName { get; set; }

        /// <summary>Gets or sets the ACHOriginCode field. </summary>
        [StringLength(BankAccountUnit.MaxAccountLength)]
        public virtual string ACHOriginCode { get; set; }

        /// <summary>Gets or sets the ACHOriginName field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public virtual string ACHOriginName { get; set; }

        /// <summary>Gets or sets the BatchId field. </summary>
        public virtual int? BatchId { get; set; }

        /// <summary>Gets or sets the CCFullAccountNO field. </summary>
        [StringLength(BankAccountUnit.MaxAccountLength)]
        public virtual string CCFullAccountNO { get; set; }

        /// <summary>Gets or sets the CCFootNote field. </summary>     
        public virtual string CCFootNote { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or Sets Addresses of the Employee. </summary>
        public List<CreateAddressUnitInput> Addresses { get; set; }
    }
}

