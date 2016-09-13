using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.JobCosting.CustomRepository;

namespace CAPS.CORPACCOUNTING.EntityFramework.Repositories
{
    public class CustomJobAccountRepository : CORPACCOUNTINGRepositoryBase<JobAccountUnit,long>, ICustomJobAccountRepository
    {
        public CustomJobAccountRepository(IDbContextProvider<CORPACCOUNTINGDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

      
        public async Task BulkInsertJobAccountUnits(List<JobAccountUnit> jobList)
        {
            var context = GetDbContext();
            await context.BulkInsertAsync(jobList);
        }
    }
}
