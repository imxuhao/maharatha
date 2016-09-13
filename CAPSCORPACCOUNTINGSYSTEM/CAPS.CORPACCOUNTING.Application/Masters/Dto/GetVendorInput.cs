using Abp.Extensions;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetVendorInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public List<Filters> Filters { get; set; }
        public List<Sort> SortList { get; set; }
        public void Normalize()
        {
            if (ReferenceEquals(SortList, null))
            {
                Sorting = "Vendor.LastName ASC";
            }
            else
            {
                Sorting = Helper.GetSortOrderAsString(SortList);
            }
        }
    }
}