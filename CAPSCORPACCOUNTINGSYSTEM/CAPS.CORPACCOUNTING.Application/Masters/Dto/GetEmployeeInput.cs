using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetEmployeeInput:IInputDto
    {
        /// <summary> Gets or Sets LastName  to Search the EmployeeGrid with  LastName </summary>
        public string LastName { get; set; } = null;

        /// <summary> Gets or Sets FirstName to Search the EmployeeGrid with  FirstName </summary>
        public string FirstName { get; set; } = null;

        /// <summary>Gets or sets the FedralTaxId to Search the EmployeeGrid with FedralTaxId. </summary>
        public string FedralTaxId { get; set; } = null;

        /// <summary>Gets or sets the SSNTaxId to Search the EmployeeGrid with SSNTaxId. </summary>
        public string SSNTaxId { get; set; } = null;

        /// <summary> Gets or Sets LastName to Search the EmployeeGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets PageNumber for EmployeeGrid. </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary> Gets or Sets SortOrder to order the EmployeeGrid by SearchOrder </summary>
        public string SortOrder { get; set; } = "ASC";

        /// <summary> Gets or Sets SortColumn to sort  the EmployeeGrid with SortColumn </summary>
        public string SortColumn { get; set; } = "LastName";

        /// <summary> Gets or Sets NumberofColumnsperPage for EmployeeGrid. </summary>
        public int NumberofColumnsperPage { get; set; } = 50;

    }
}