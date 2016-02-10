using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetSalesRepInput : IInputDto
    {
        /// <summary> Gets or Sets LastName to Search the SalesRepGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets PageNumber for SalesRepGrid. </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary> Gets or Sets SortOrder to order the SalesRepGrid by SearchOrder </summary>
        public string SortOrder { get; set; } = "ASC";

        /// <summary> Gets or Sets SortColumn to sort  the SalesRepGrid with SortColumn </summary>
        public string SortColumn { get; set; } = "LastName";

        /// <summary> Gets or Sets NumberofColumnsperPage for SalesRepGrid. </summary>
        public int NumberofColumnsperPage { get; set; } = 50;

        /// <summary> Gets or Sets LastName to Search the SalesRepGrid with LastName </summary>
        public string LastName { get; set; } = null;
    }
}