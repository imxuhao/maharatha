using Abp;

namespace CAPS.CORPACCOUNTING
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="CORPACCOUNTINGDomainServiceBase"/>.
    /// For application services inherit CORPACCOUNTINGAppServiceBase.
    /// </summary>
    public abstract class CORPACCOUNTINGServiceBase : AbpServiceBase
    {
        protected CORPACCOUNTINGServiceBase()
        {
            LocalizationSourceName = CORPACCOUNTINGConsts.LocalizationSourceName;
        }
    }
}