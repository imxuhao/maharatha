using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetCustomerPaymentTermInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets LastName to Search the CustomerPaymentTermGrid with  OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Description ASC";
            }
            if (Sorting.IndexOf("Description", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "CustomerPayTerms." + Sorting;
            }
            else if (Sorting.IndexOf("DueDays", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "CustomerPayTerms." + Sorting;
            }
            else if (Sorting.IndexOf("Discount", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "CustomerPayTerms." + Sorting;
            }
            else if (Sorting.IndexOf("DiscountDays", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "CustomerPayTerms." + Sorting;
            }
            else
            {
                Sorting = "CustomerPayTerms." + Sorting;
            }
        }
    }
}