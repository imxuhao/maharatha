using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class GetJobInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the ComapnyId to Search the Jobs based on  ComapnyId. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets the Caption to Search the Jobs based on Caption. </summary>
        public string Caption { get; set; }

        /// <summary> Gets or Sets the ProductName to Search the Jobdetails based on ProductName. </summary>
        public string ProductName { get; set; }

        /// <summary> Gets or Sets the JobNumber to Search the Jobs based on JobNumber. </summary>
        public string JobNumber { get; set; }

        /// <summary> Gets or Sets the Director to Search the Employees based on EmployeeLastName. </summary>
        public string Director { get; set; }

        /// <summary> Gets or Sets the Agency to Search the Customers based on CustomerId. </summary>
        public string Agency { get; set; }

        /// <summary> Gets or Sets the TypeOfJobStatusId to Search the Jobs based on TypeOfJobStatusId. </summary>
        public ProjectStatus? TypeOfJobStatusId { get; set; } = null;


        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Caption ASC";
            }
            if (Sorting.IndexOf("Caption", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Job." + Sorting;
            }
            else
            if (Sorting.IndexOf("ProductName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Jobdetail." + Sorting;
            }
            else
            if (Sorting.IndexOf("JobNumber", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Job." + Sorting;
            }
            else
            if (Sorting.IndexOf("Director", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Director." + Sorting;
            }
            else
            if (Sorting.IndexOf("Agency", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Agency." + Sorting;
            }
            else
            {
                Sorting = "Job." + Sorting;
            }
        }
    }
}