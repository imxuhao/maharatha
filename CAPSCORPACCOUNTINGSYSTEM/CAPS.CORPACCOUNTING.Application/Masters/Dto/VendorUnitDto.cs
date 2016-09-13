using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(VendorUnit))]
    public class VendorUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the VendorId field. </summary>
        public int VendorId { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        
        public string FirstName { get; set; }

        /// <summary>Gets or sets the PayToName field. </summary>
        public string PayToName { get; set; }

        /// <summary>Gets or sets the DBAName field. </summary>
        public string DbaName { get; set; }

        /// <summary>Gets or sets the VendorNumber field. </summary>
        public string VendorNumber { get; set; }

        /// <summary>Gets or sets the VendorAccountInfo field. </summary>
        public string VendorAccountInfo { get; set; }

        /// <summary>Gets or sets the FedralTaxId field. </summary>
        public string FedralTaxId { get; set; }

        /// <summary>Gets or sets the SSNTaxId field. </summary>
        public string SSNTaxId { get; set; }

        /// <summary>Gets or sets the CreditLimit field. </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary>Gets or sets the TypeofPaymentMethodId field. </summary>
        public TypeofPaymentMethod? TypeofPaymentMethodId { get; set; }

        /// <summary>Gets or sets the TypeofPaymentMethod field. </summary>
        public string TypeofPaymentMethod { get; set; }

        /// <summary>Gets or sets the PaymentTermsId field. </summary>
        public int? PaymentTermsId { get; set; }

        /// <summary>Gets or sets the PaymentTerms field. </summary>
        public string PaymentTermName{ get; set; }

        /// <summary>Gets or sets the TypeofCurrency field. </summary>
        public string TypeofCurrency { get; set; }

        /// <summary>Gets or sets the TypeofCurrencyId field. </summary>
        public int? TypeofCurrencyId { get; set; }

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

        /// <summary>Gets or sets the TypeofVendor field. </summary>
        public string TypeofVendor { get; set; }

        /// <summary>Gets or sets the Typeof1099Box field. </summary>
        public Typeof1099T4? Typeof1099BoxId { get; set; }

        /// <summary>Gets or sets the Typeof1099Box field. </summary>
        public string Typeof1099Box { get; set; }

        /// <summary>Gets or sets the EDDContractStartDate field. </summary>
        public DateTime? EDDContractStartDate { get; set; }

        /// <summary>Gets or sets the EDDContractStopDate field. </summary>
        public DateTime? EDDContractStopDate { get; set; }

        /// <summary>Gets or sets the EDDConctractAmount field. </summary>
        public decimal? EDDConctractAmount { get; set; }

        /// <summary>Gets or sets the WorkRegion field. </summary>
        public string WorkRegion { get; set; }

        /// <summary>Gets or sets the IsEDDContractOnGoing field. </summary>
        public bool IsEDDContractOnGoing { get; set; }

        /// <summary>Gets or sets the ACHBankName field. </summary>
        public string ACHBankName { get; set; }

        /// <summary>Gets or sets the ACHRoutingNumber field. </summary>
        public string ACHRoutingNumber { get; set; }

        /// <summary>Gets or sets the ACHAccountNumber field. </summary>
        public string ACHAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireFromBankName field. </summary>
        public string ACHWireFromBankName { get; set; }

        /// <summary>Gets or sets the ACHWireFromBankAddress field. </summary>
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

        /// <summary>Gets or sets the CompanyId field. </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>Gets or sets the BillingAccount field. </summary>
       
        public string BillingAccount { get; set; }

        /// <summary>Gets or sets the TypeofTaxId field. </summary>
        public TypeofTax? TypeofTaxId { get; set; }

        /// <summary>Gets or sets the TypeofTax field. </summary>
        public string TypeofTax { get; set; }

        /// <summary>Gets or sets the TaxCreditId field. </summary>
        public int? TaxCreditId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int? JobId { get; set; }

        /// <summary>Gets or sets the GLAccountId field. </summary>
        public long? GLAccountId { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public long? AccountId { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public string Notes { get; set; }
        /// <summary>Gets or sets the Addresses of the vendor. </summary>
        public Collection<AddressUnitDto> Address { get; set; }
        /// <summary>Gets or sets the VendorAlias of the vendor. </summary>
        public Collection<VendorAliasUnitDto> VendorAlias { get; set; }
    }
}
