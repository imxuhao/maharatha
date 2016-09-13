using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.CustomRepository;

namespace CAPS.CORPACCOUNTING.EntityFramework.Repositories
{
    public class CustomAccountRepository : CORPACCOUNTINGRepositoryBase<AccountUnit, long>, ICustomAccountRepository
    {
        public CustomAccountRepository(IDbContextProvider<CORPACCOUNTINGDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

      
        public async Task BulkInsertAccountUnits(List<AccountUnit> accountList)
        {
            var context = GetDbContext();
            await context.BulkInsertAsync(accountList);
        }
    }
}
