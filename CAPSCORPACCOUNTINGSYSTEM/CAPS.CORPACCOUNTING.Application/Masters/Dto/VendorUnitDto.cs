using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(VendorUnit))]
    public class VendorUnitDto : AuditedEntityDto
    {
        public int VendorId { get; set; }

        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the PayToName. </summary>
        public string PayToName { get; set; }

        /// <summary>Gets or sets the DBAName field. </summary>

        public string DbaName { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public string VendorNumber { get; set; }
        /// <summary>Gets or sets the IsPrivate field. </summary>
        public string VendorAccountInfo { get; set; }

        /// <summary>Gets or sets the FedralTaxId field. </summary>
        public string FedralTaxId { get; set; }

        /// <summary>Gets or sets the SSNTaxId field. </summary>
        public string SSNTaxId { get; set; }

        /// <summary>Gets or sets the CreditLimit field. </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary>Gets or sets the TypeofPaymentMethod field. </summary>
        public TypeofPaymentMethod? TypeofPaymentMethod { get; set; }

        /// <summary>Gets or sets the TypeofPaymentMethod field. </summary>
        public int? PaymentTermsId { get; set; }

        /// <summary>Gets or sets the TypeofCurrency field. </summary>
        public string TypeofCurrency { get; set; }

        /// <summary>Gets or sets the IsCorporation field. </summary>
        public bool IsCorporation { get; set; }

        /// <summary>Gets or sets the Is1099 field. </summary>
        public bool Is1099 { get; set; }
        /// <summary>Gets or sets the IsIndependentContractor field. </summary>
        public bool IsIndependentContractor { get; set; }

        /// <summary>Gets or sets the Isw9OnFile field. </summary>
        public bool Isw9OnFile { get; set; }

        /// <summary>Gets or sets the TypeOfvendorId field. </summary>
        public TypeofVendor? TypeofVendorId { get; set; }

        /// <summary>Gets or sets the Typeof1099T4 field. </summary>
        public Typeof1099T4? Typeof1099Box { get; set; }

        /// <summary>Gets or sets the EDDContractStartDate field. </summary>
        public DateTime? EDDContractStartDate { get; set; }

        /// <summary>Gets or sets the EDDContractStopDate field. </summary>
        public DateTime? EDDContractStopDate { get; set; }

        /// <summary>Gets or sets the EDDContractStopDate field. </summary>
        public decimal? EDDConctractAmount { get; set; }

        /// <summary>Gets or sets the WorkRegion field. </summary>
        public string WorkRegion { get; set; }

        /// <summary>Gets or sets the IsEDDContractOnGoing field. </summary>
        public bool IsEDDContractOnGoing { get; set; }
        /// <summary>Gets or sets the ACHBankName field. </summary>

        public string ACHBankName { get; set; }
        /// <summary>Gets or sets the ACHRoutingNumber field. </summary>
        public string ACHRoutingNumber { get; set; }

        public string ACHAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireFromBankName field. </summary>
        public string ACHWireFromBankName { get; set; }

        /// <summary>Gets or sets the ACHWireFromBankName field. </summary>
        public string ACHWireFromBankAddress { get; set; }

        /// <summary>Gets or sets the ACHWireFromSwiftCode field. </summary>
        public string ACHWireFromSwiftCode { get; set; }

        /// <summary>Gets or sets the ACHWireFromAccountNumber field. </summary>
        public string ACHWireFromAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireToBankName field. </summary>
        public string ACHWireToBankName { get; set; }

        /// <summary>Gets or sets the ACHWireToSwiftCode field. </summary>
        public string ACHWireToSwiftCode { get; set; }

        /// <summary>Gets or sets the ACHWireToBeneficiary field. </summary>
        public string ACHWireToBeneficiary { get; set; }

        /// <summary>Gets or sets the ACHWireToAccountNumber field. </summary>
        public string ACHWireToAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireToIBAN field. </summary>
        public string ACHWireToIBAN { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
