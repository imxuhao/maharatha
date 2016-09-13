using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetVendorPayTermsInput :  PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary> Gets or Sets the CompanyId to Search the CustomerPaymentTerms based on  CompanyId. </summary>
        public long? OrganizationUnitId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Description ASC";
            }
            if (Sorting.IndexOf("Description", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "VendorPayTerms." + Sorting;
            }
            else if (Sorting.IndexOf("DueDays", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "VendorPayTerms." + Sorting;
            }
            else if (Sorting.IndexOf("Discount", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "VendorPayTerms." + Sorting;
            }
            else if (Sorting.IndexOf("DiscountDays", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "VendorPayTerms." + Sorting;
            }
            else
            {
                Sorting = "VendorPayTerms." + Sorting;
            }
        }
    }
}