using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetCoaInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the CompanyId to Search the ChartofAccounts with CompanyId. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary> Gets or Sets the Description to Search the ChartofAccounts with Description. </summary>
        public string Description { get; set; }

        /// <summary> Gets or Sets the ChartofAccountsType to Search the ChartofAccounts with ChartofAccountsType. </summary>
        public ChartofAccountsType? ChartofAccountsType { get; set; } = null;
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Description ASC";
            }
            if (Sorting.IndexOf("Description", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Coa." + Sorting;
            }
            else if (Sorting.IndexOf("ChartofAccountsType", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "Coa." + Sorting;
            }
            else
            {
                Sorting = "Coa." + Sorting;
            }
        }
    }
}