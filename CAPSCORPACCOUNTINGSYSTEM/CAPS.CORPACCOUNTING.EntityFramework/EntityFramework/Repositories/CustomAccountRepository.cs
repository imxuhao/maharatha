using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.CustomRepository;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Z.EntityFramework.Extensions;

namespace CAPS.CORPACCOUNTING.EntityFramework.Repositories
{
    public abstract class CustomAccountRepository : CORPACCOUNTINGRepositoryBase<AccountUnit, long>, ICustomAccountRepository
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
