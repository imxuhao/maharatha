using Abp.Domain.Services;

namespace CAPS.CORPACCOUNTING
{
    public abstract class CORPACCOUNTINGDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected CORPACCOUNTINGDomainServiceBase()
        {
            LocalizationSourceName = CORPACCOUNTINGConsts.LocalizationSourceName;
        }
    }
}
