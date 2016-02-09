namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetCustomerInput
    {
        /// <summary> Gets or Sets LastName  to Search the CustomerGrid with  LastName </summary>
        public string LastName { get; set; } = null;

        /// <summary> Gets or Sets FirstName to Search the CustomerGrid with  FirstName </summary>
        public string FirstName { get; set; } = null;

        /// <summary> Gets or Sets CustomerNumber to Search the CustomerGrid with  CustomerNumber </summary>
        public string CustomerNumber { get; set; } = null;

        /// <summary> Gets or Sets LastName to Search the CustomerGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets PageNumber for CustomerGrid. </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary> Gets or Sets SortOrder to order the CustomerGrid by SearchOrder </summary>
        public string SortOrder { get; set; } = "ASC";

        /// <summary> Gets or Sets SortColumn to sort  the CustomerGrid with SortColumn </summary>
        public string SortColumn { get; set; } = "LastName";
     
        /// <summary> Gets or Sets NumberofColumnsperPage for CustomerGrid. </summary>
        public int NumberofColumnsperPage { get; set; } = 50;
    }
}