using System.Collections.Generic;
using Abp.Runtime.Validation;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Helpers;

namespace CAPS.CORPACCOUNTING.GenericSearch.Dto
{
    public class SearchInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public List<Filters> Filters { get; set; }
        public List<Sort> SortList { get; set; }

        public virtual long? OrganizationUnitId { get; set; }
        public void Normalize()
        {
            if (!ReferenceEquals(SortList, null))
            {
                Sorting = Helper.GetSortOrderAsString(SortList);
            }
        }
    }
}