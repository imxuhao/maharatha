using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace CAPS.CORPACCOUNTING.Masters.CustomRepository
{
    public interface ICustomAccountRepository:IRepository<AccountUnit,long>
    {
        Task BulkInsertAccountUnits(List<AccountUnit> accountList);
    }
}
