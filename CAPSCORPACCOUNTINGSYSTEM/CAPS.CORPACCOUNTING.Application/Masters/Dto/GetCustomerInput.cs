using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;
namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetCustomerInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the LastName  to Search the Customers based on LastName. </summary>
        public string LastName { get; set; } = null;

        /// <summary> Gets or Sets the FirstName to Search the Customers based on FirstName. </summary>
        public string FirstName { get; set; } = null;

        /// <summary> Gets or Sets the CustomerNumber to Search the Customers based on CustomerNumber. </summary>
        public string CustomerNumber { get; set; } = null;

        /// <summary> Gets or Sets the CompanyId to Search the Customers based on CompanyId. </summary>
        public long? OrganizationUnitId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "LastName ASC";
            }

            if (Sorting.IndexOf("FirstName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Customer." + Sorting;
            }
            else if (Sorting.IndexOf("LastName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Customer." + Sorting;
            }
            else if (Sorting.IndexOf("CustomerNumber", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Customer." + Sorting;
            }
            else
            {
                Sorting = "Customer." + Sorting;
            }
        }
    }
}