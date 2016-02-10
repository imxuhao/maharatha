using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetVendorInput : IInputDto
    {
        /// <summary> Gets or Sets LastName to Search the VendorGrid with LastName </summary>
        public string LastName { get; set; } = null;

        /// <summary> Gets or Sets FirstName to Search the VendorGrid with FirstName </summary>
        public string FirstName { get; set; } = null;

        /// <summary> Gets or Sets PayToName to Search the VendorGrid with PayToName </summary>
        public string PayToName { get; set; } = null;

        /// <summary> Gets or Sets OrganizationUnitId to Search the VendorGrid with OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the VendorNumber  to Search the VendorGrid with VendorNumber. </summary>
        public string VendorNumber { get; set; } = null;

        /// <summary>Gets or sets the VendorAccountInfo  to Search the VendorGrid with VendorAccountInfo. </summary>
        public string VendorAccountInfo { get; set; } = null;

        /// <summary>Gets or sets the FedralTaxId to Search the VendorGrid with FedralTaxId. </summary>
        public string FedralTaxId { get; set; } = null;

        /// <summary>Gets or sets the SSNTaxId to Search the VendorGrid with SSNTaxId. </summary>
        public string SSNTaxId { get; set; } = null;
        
        /// <summary>Gets or sets the PhoneorEmail to Search the VendorGrid with Phone1 or phone2 or Email. </summary>
        public string PhoneorEmail { get; set; } = null;

        /// <summary>Gets or sets the Typeof1099Box to Search the VendorGrid with Typeof1099Box. </summary>
        public Typeof1099T4? Typeof1099Box { get; set; } = null;

        /// <summary>Gets or sets the TypeofVendorId to Search the VendorGrid with TypeofVendorId. </summary>
        public TypeofVendor? TypeofVendorId { get; set; } = null;

        /// <summary> Gets or Sets PageNumber. </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary> Gets or Sets SortOrder  </summary>
        public string SortOrder { get; set; } = "ASC";

        /// <summary> Gets or Sets SortColumn to Sort VendorGrid By sortcolumn </summary>
        public string SortColumn { get; set; } = "LastName";

        /// <summary> Gets or Sets NumberofColumnsperPage. </summary>
        public int NumberofColumnsperPage { get; set; } = 50;
    }
}