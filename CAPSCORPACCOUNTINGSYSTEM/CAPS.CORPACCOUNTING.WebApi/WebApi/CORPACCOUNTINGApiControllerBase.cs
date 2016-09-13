using Abp.WebApi.Controllers;

namespace CAPS.CORPACCOUNTING.WebApi
{
    public abstract class CORPACCOUNTINGApiControllerBase : AbpApiController
    {
        protected CORPACCOUNTINGApiControllerBase()
        {
            LocalizationSourceName = CORPACCOUNTINGConsts.LocalizationSourceName;
        }
    }
}