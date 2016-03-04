
using Abp.Domain.Repositories;
using Abp.WebApi.OData.Controllers;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING
{
   public class COAController : AbpODataEntityController<CoaUnit>
    {
        public COAController(IRepository<CoaUnit> repository)
        : base(repository)
    {
        }
    }
}
