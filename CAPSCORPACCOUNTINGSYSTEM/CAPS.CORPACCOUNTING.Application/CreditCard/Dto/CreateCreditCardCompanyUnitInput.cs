using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.CreditCard.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(BankAccountUnit))]
    public class CreateCreditCardCompanyUnitInput
    {
        /// <summary>Gets or sets the Description field. </summary>
        [Required(ErrorMessage = "Credit Card Company Field is required.")]
        [StringLength(BankAccountUnit.MaxLength,ErrorMessage = "Credit Card Company Field length should not exceed 200 characters.")]
        public string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the TypeOfBankAccountId field. </summary>
        [EnumDataType(typeof(TypeOfBankAccount), ErrorMessage = "Card Type Field is required.")]
        public TypeOfBankAccount TypeOfBankAccountId { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public long? AccountId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int? JobId { get; set; }

        /// <summary>Gets or sets the BankAccountName field. </summary>
        [StringLength(BankAccountUnit.MaxLength, ErrorMessage = "Account Name Field length should not exceed 200 characters.")]
        public string BankAccountName { get; set; }

        /// <summary>Gets or sets the BankAccountNumber field. </summary>
        [StringLength(BankAccountUnit.MaxLength, ErrorMessage = "Account Number Field length should not exceed 200 characters.")]
        public string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the RoutingNumber field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public string RoutingNumber { get; set; }

        /// <summary>Gets or sets the TypeOfCheckStockId field. </summary>
        public int? TypeOfCheckStockId { get; set; }

        /// <summary>Gets or sets the LastCheckNumberGenerated field. </summary>
        public long? LastCheckNumberGenerated { get; set; }

        /// <summary>Gets or sets the ControlAccount field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public string ControlAccount { get; set; }

        /// <summary>Gets or sets the ClearingAccountId field. </summary>
        public long? ClearingAccountId { get; set; }

        /// <summary>Gets or sets the ClearingJobId field. </summary>
        public int? ClearingJobId { get; set; }

        /// <summary>Gets or sets the MaxExpirationLength field. </summary>
        [StringLength(BankAccountUnit.MaxExpirationLength)]
        public string ExpirationMMYYYY { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileId field. </summary>
        public int? TypeOfUploadFileId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public int? VendorId { get; set; }

        /// <summary>Gets or sets the ControllingBankAccountId field. </summary>
        public long? ControllingBankAccountId { get; set; }

        /// <summary>Gets or sets the IsClosed field. </summary>
        public bool IsClosed { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the PositivePayTypeOfUploadFileId field. </summary>
        public int? PositivePayTypeOfUploadFileId { get; set; }

        /// <summary>Gets or sets the PositivePayTransmitterInfo field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public string PositivePayTransmitterInfo { get; set; }

        /// <summary>Gets or sets the PettyCashAccountId field. </summary>
        public long? PettyCashAccountId { get; set; }

        /// <summary>Gets or sets the IsACHEnabled field. </summary>
        public bool? IsACHEnabled { get; set; }

        /// <summary>Gets or sets the ACHDestinationCode field. </summary>
        [StringLength(BankAccountUnit.MaxAccountLength)]
        public string ACHDestinationCode { get; set; }

        /// <summary>Gets or sets the ACHDestinationName field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public string ACHDestinationName { get; set; }

        /// <summary>Gets or sets the ACHOriginCode field. </summary>
        [StringLength(BankAccountUnit.MaxAccountLength)]
        public string ACHOriginCode { get; set; }

        /// <summary>Gets or sets the ACHOriginName field. </summary>
        [StringLength(BankAccountUnit.MaxLength)]
        public string ACHOriginName { get; set; }

        /// <summary>Gets or sets the BatchId field. </summary>
        public int? BatchId { get; set; }

        /// <summary>Gets or sets the CCFullAccountNO field. </summary>
        [StringLength(BankAccountUnit.MaxAccountLength)]
        public string CCFullAccountNO { get; set; }

        /// <summary>Gets or sets the CCFootNote field. </summary>     
        public string CCFootNote { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or Sets Addresses of the Credit Card Company. </summary>
        public List<CreateAddressUnitInput> Addresses { get; set; }
    }
}
