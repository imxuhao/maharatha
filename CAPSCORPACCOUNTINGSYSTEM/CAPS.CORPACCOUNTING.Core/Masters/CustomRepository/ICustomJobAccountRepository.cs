using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Masters.CustomRepository
{
    public interface ICustomJobAccountRepository:IRepository<JobAccountUnit,long>
    {
        Task BulkInsertJobAccountUnits(List<JobAccountUnit> jobAccountList);
    }
}
