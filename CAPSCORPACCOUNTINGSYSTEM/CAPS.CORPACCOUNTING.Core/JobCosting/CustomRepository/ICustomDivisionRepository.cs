using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.JobCosting.CustomRepository
{
    public interface ICustomDivisionRepository : IRepository<JobUnit>
    {
        Task BulkInsertDivisionUnits(List<JobUnit> divisionList);
    }
}
