using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.CreditCard
{
   public interface ICreditCardCompanyAppService: IApplicationService
    {
        /// <summary>
        ///  Create the CreditCard Company.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreateCCCompanyDocumentUnit(CreateBankAccountUnitInput input);

        /// <summary>
        /// Update the CreditCard Company based on Id.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateCCCompanyDocumentUnit(UpdateBankAccountUnitInput input);

        /// <summary>
        ///  Delete the CreditCard Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCCCompanyDocumentUnit(IdInput<long> input);

        /// <summary>
        /// Get BatchList By TypeOfBatch
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetBatchList(BatchSearchInput input);
        
    }
}
