using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.JobCosting.CustomRepository;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.CustomRepository;

namespace CAPS.CORPACCOUNTING.EntityFramework.Repositories
{
    public class CustomDivisionRepository : CORPACCOUNTINGRepositoryBase<JobUnit>, ICustomDivisionRepository
    {
        public CustomDivisionRepository(IDbContextProvider<CORPACCOUNTINGDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

      
        public async Task BulkInsertDivisionUnits(List<JobUnit> divisionList)
        {
            var context = GetDbContext();
            await context.BulkInsertAsync(divisionList);
        }
    }
}
