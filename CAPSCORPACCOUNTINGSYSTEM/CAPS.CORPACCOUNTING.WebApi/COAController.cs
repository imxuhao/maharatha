
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.WebApi.Authorization;
using Abp.WebApi.OData.Controllers;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING
{
    [AbpApiAuthorize()]
    public class COAController : AbpODataEntityController<CoaUnit>
    {
        
        public COAController(IRepository<CoaUnit> repository)
        : base(repository)
    {
        }
    }
}
