using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Accounting
{

    /// <summary>
    /// 
    /// </summary>
   public interface IClassificationUnitAppService: IApplicationService
    {
        /// <summary>
        /// Create the Classification.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> CreateClassificationUnit(CreateTypeOfAccountInputUnit input);

        /// <summary>
        /// Update the Classification based on TypeOfAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateClassificationUnit(UpdateTypeOfAccountInputUnit input);


        /// <summary>
        /// Delete the Classification based on TypeOfAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteClassificationUnit(IdInput input);


        /// <summary>
        /// Get the list of all Classification and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<TypeOfAccountUnitDto>> GetClassificationUnits(SearchInputDto input);



        /// <summary>
        /// Get the Classification based on TypeOfAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TypeOfAccountUnitDto> GetClassificationUnitById(IdInput input);

        /// <summary>
        /// Get TypeOfAccountClassification List
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetTypeOfAccountClassificationList(AutoSearchInput input);

    }
}
