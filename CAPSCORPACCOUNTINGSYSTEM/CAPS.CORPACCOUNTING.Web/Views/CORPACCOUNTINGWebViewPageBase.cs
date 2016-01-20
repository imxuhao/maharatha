using Abp.Web.Mvc.Views;

namespace CAPS.CORPACCOUNTING.Web.Views
{
    public abstract class CORPACCOUNTINGWebViewPageBase : CORPACCOUNTINGWebViewPageBase<dynamic>
    {

    }

    public abstract class CORPACCOUNTINGWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected CORPACCOUNTINGWebViewPageBase()
        {
            LocalizationSourceName = CORPACCOUNTINGConsts.LocalizationSourceName;
        }
    }
}