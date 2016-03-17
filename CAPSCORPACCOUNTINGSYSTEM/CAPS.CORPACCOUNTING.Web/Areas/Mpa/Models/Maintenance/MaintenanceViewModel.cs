using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Caching.Dto;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}