using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class GetRollupCenterInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the CompanyId to Search the RollupCenters based on  CompanyId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets the LastName to Search the RollupCenters based on LastName </summary>
        public string Caption { get; set; } = null;

        /// <summary> Gets or Sets the RollupTypeId to Search the RollupCenters based on RollupTypeId </summary>
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