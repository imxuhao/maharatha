using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class GetRollupCenterInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets LastName to Search the SalesRepGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets LastName to Search the SalesRepGrid with LastName </summary>
        public string Caption { get; set; } = null;

        public RollupType? RollupTypeId { get; set; } = null;

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Caption ASC";
            }
            if (Sorting.IndexOf("Caption", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "RollupCenter." + Sorting;
            }
            else if (Sorting.IndexOf("RollupTypeId", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "RollupCenter." + Sorting;
            }           
            else
            {
                Sorting = "RollupCenter." + Sorting;
            }
        }
    }
}