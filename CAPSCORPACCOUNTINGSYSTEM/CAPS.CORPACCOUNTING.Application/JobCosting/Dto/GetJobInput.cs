using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class GetJobInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets LastName to Search the SalesRepGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets LastName to Search the SalesRepGrid with LastName </summary>
        public string Caption { get; set; }

        public string ProductName { get; set; }

        public string JobNumber { get; set; }
        public string Director { get; set; }
        public string Agency { get; set; }
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