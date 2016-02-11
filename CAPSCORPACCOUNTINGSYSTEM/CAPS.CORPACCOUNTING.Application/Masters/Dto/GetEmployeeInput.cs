using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetEmployeeInput : PagedAndSortedInputDto, IShouldNormalize
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