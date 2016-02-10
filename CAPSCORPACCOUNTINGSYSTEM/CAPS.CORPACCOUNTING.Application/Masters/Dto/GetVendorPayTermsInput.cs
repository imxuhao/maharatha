using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetVendorPayTermsInput : IInputDto
    {
        /// <summary> Gets or Sets LastName to Search the CustomerPaymentTermGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets PageNumber for CustomerPaymentTermGrid. </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary> Gets or Sets SortOrder to order the CustomerPaymentTermGrid by SearchOrder </summary>
        public string SortOrder { get; set; } = "ASC";

        /// <summary> Gets or Sets SortColumn to sort  the CustomerPaymentTermGrid with SortColumn </summary>
        public string SortColumn { get; set; } = "Description";

        /// <summary> Gets or Sets NumberofColumnsperPage for CustomerPaymentTermGrid. </summary>
        public int NumberofColumnsperPage { get; set; } = 50;
    }
}