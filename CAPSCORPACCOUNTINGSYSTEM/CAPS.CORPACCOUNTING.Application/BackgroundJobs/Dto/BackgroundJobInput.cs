using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.BackgroundJobs.Dto
{
    public class BackgroundJobInput : BackgroundJobInput<int>
    {
        public int Id { get; set; }
    }

    public class BackgroundJobInput<T>
    {
        public T Id { get; set; }
        public long? OrganizationUnitId { get; set; }
        public int tenantId { get; set; }

    }
}
