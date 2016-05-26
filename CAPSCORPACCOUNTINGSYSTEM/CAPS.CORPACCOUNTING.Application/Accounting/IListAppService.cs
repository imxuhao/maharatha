using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Accounting
{
   public interface IListAppService : IApplicationService
    {

        /// <summary>
        /// Get Jobs or Divisions List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetJobOrDivisionList(AutoSearchInput input);

        /// <summary>
        /// Get accounts List based on JobId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GeAccountsList(AutoSearchInput input);

        /// <summary>
        ///  Get SubAccounts List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetSubAccountList(AutoSearchInput input);
        /// <summary>
        ///  Get VendorList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetVendorList(AutoSearchInput input);

       /// <summary>
       ///  Get TaxCreditList
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
       Task<List<NameValueDto>> GetTaxCreditList(AutoSearchInput input);
        /// <summary>
        /// GetTypeof1099List
        /// </summary>
        /// <returns></returns>
       List<NameValueDto> GetTypeof1099T4List();
    }
}
