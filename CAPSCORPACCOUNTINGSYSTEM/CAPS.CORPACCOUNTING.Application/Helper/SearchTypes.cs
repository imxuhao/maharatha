using System.Collections.Generic;
using CAPS.CORPACCOUNTING.GenericSearch;

namespace CAPS.CORPACCOUNTING.Helpers
{
    public class SearchTypes
    {
        public IEnumerable<TextSearch> TextSearch { get; set; }
        public IEnumerable<NumericSearch> NumericSearch { get; set; }
        public IEnumerable<EnumSearch> EnumSearch { get; set; }
        public IEnumerable<DateSearch> DateSearch { get; set; }
        public IEnumerable<BooleanSearch> BooleanSearch { get; set; }

        public IEnumerable<DecimalSearch> DecimalSearch { get; set; }


    }
}