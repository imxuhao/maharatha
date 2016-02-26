using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetEmployeeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the LastName to Search the Employees based on LastName. </summary>
        public string LastName { get; set; } = null;

        /// <summary> Gets or Sets the FirstName to Search the Employees based on FirstName. </summary>
        public string FirstName { get; set; } = null;

        /// <summary>Gets or sets the FedralTaxId to Search the Employees based on FedralTaxId. </summary>
        public string FedralTaxId { get; set; } = null;

        /// <summary>Gets or sets the SSNTaxId to Search the Employees based on SSNTaxId. </summary>
        public string SSNTaxId { get; set; } = null;

        /// <summary> Gets or Sets the LastName to Search the Employees based on CompanyId </summary>
        public long? OrganizationUnitId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "LastName ASC";
            }
            if (Sorting.IndexOf("FirstName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Employee." + Sorting;
            }
            else if (Sorting.IndexOf("LastName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Employee." + Sorting;
            }
            else if (Sorting.IndexOf("FedralTaxId", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Employee." + Sorting;
            }
            else if (Sorting.IndexOf("SSNTaxId", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Employee." + Sorting;
            }
            else
            {
                Sorting = "Employee." + Sorting;
            }
        }
    }
}