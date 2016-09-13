using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetSalesRepInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the LastName to Search the SalesRepresentatives based on CompanyId. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets the LastName to Search the SalesRepresentatives based on LastName. </summary>
        public string LastName { get; set; } = null;

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "LastName ASC";
            }
            if (Sorting.IndexOf("LastName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "SalesRep." + Sorting;
            }
            else if (Sorting.IndexOf("FirstName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "SalesRep." + Sorting;
            }
            else if (Sorting.IndexOf("Region", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "SalesRep." + Sorting;
            }
            else
            {
                Sorting = "SalesRep." + Sorting;
            }
        }
    }
}