using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.ObjectModel;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapFrom(typeof(BankAccountUnit))]
    public class BankAccountUnitDto : IOutputDto
    {
        /// <summary>Overriding the Id column with BankAccountId</summary>
        public virtual long BankAccountId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
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
        public virtual string BankAccountName { get; set; }

        /// <summary>Gets or sets the BankAccountNumber field. </summary>
        public virtual string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the RoutingNumber field. </summary>
        public virtual string RoutingNumber { get; set; }

        /// <summary>Gets or sets the TypeOfCheckStockId field. </summary>
        public virtual int? TypeOfCheckStockId { get; set; }

        /// <summary>Gets or sets the LastCheckNumberGenerated field. </summary>
        public virtual long? LastCheckNumberGenerated { get; set; }

        /// <summary>Gets or sets the ControlAccount field. </summary>
        public virtual string ControlAccount { get; set; }

        /// <summary>Gets or sets the ClearingAccountId field. </summary>
        public virtual long? ClearingAccountId { get; set; }

        /// <summary>Gets or sets the ClearingJobId field. </summary>
        public virtual int? ClearingJobId { get; set; }

        /// <summary>Gets or sets the MaxExpirationLength field. </summary>
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
        public virtual string PositivePayTransmitterInfo { get; set; }

        /// <summary>Gets or sets the PettyCashAccountId field. </summary>
        public virtual long? PettyCashAccountId { get; set; }

        /// <summary>Gets or sets the IsACHEnabled field. </summary>
        public virtual bool? IsACHEnabled { get; set; }

        /// <summary>Gets or sets the ACHDestinationCode field. </summary>
        public virtual string ACHDestinationCode { get; set; }

        /// <summary>Gets or sets the ACHDestinationName field. </summary>
        public virtual string ACHDestinationName { get; set; }

        /// <summary>Gets or sets the ACHOriginCode field. </summary>
        public virtual string ACHOriginCode { get; set; }

        /// <summary>Gets or sets the ACHOriginName field. </summary>
        public virtual string ACHOriginName { get; set; }

        /// <summary>Gets or sets the BatchId field. </summary>
        public virtual int? BatchId { get; set; }

        /// <summary>Gets or sets the CCFullAccountNO field. </summary>
        public virtual string CCFullAccountNO { get; set; }

        /// <summary>Gets or sets the CCFootNote field. </summary>     
        public virtual string CCFootNote { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the Address field. </summary>
        public Collection<AddressUnitDto> Address { get; set; }

        /// <summary>Gets or sets the TypeOfBankAccountDesc.</summary>
        public string TypeOfBankAccountDesc { get; set; }

        /// <summary>Gets or sets the Account.</summary>
        public string LedgerAccount { get; set; }

        /// <summary>Gets or sets the Job.</summary>
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the TypeofCheckStockDesc.</summary>
        public string TypeofCheckStockDesc { get; set; }

        /// <summary>Gets or sets the ClearingAccount.</summary>
        public string ClearingAccountNumber { get; set; }

        /// <summary>Gets or sets the ClearingJob.</summary>
        public string ClearingJobNumber { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileDesc.</summary>
        public string TypeOfUploadFileDesc { get; set; }

        /// <summary>Gets or sets the Vendor.</summary>
        public string VendorNumber { get; set; }

        /// <summary>Gets or sets the ControllingBankAccounts.</summary>
        public string ControllingBankAccountDesc { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatus.</summary>
        public string TypeOfInactiveStatus { get; set; }

        /// <summary>Gets or sets the PositivePayTypeOfUploadFileDesc.</summary>
        public string PositivePayTypeOfUploadFileDesc { get; set; }

        /// <summary>Gets or sets the PettyCashAccount.</summary>
        public string PettyCashAccount { get; set; }

        /// <summary>Gets or sets the Batch.</summary>
        public string BatchDesc { get; set; }

    }
}
