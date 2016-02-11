using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;
namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetCustomerInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets LastName  to Search the CustomerGrid with  LastName </summary>
        public string LastName { get; set; } = null;

        /// <summary> Gets or Sets FirstName to Search the CustomerGrid with  FirstName </summary>
        public string FirstName { get; set; } = null;

        /// <summary> Gets or Sets CustomerNumber to Search the CustomerGrid with  CustomerNumber </summary>
        public string CustomerNumber { get; set; } = null;

        /// <summary> Gets or Sets LastName to Search the CustomerGrid with  OrganizationUnitId </summary>
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