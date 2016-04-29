using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on Account.
    /// </summary>
    public interface IProjectCoaUnitAppService : IApplicationService
    {
      

        /// <summary>
        /// Create the New ProjectCoa.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CoaUnitDto> CreateProjectCoaUnit(CreateCoaUnitInput input);

        /// <summary>
        /// Update the ProjectCoa based on CoaId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CoaUnitDto> UpdateProjectCoaUnit(UpdateCoaUnitInput input);



        /// <summary>
        /// Delete the ProjectCoa based on coaId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteProjectCoaUnit(IdInput input);

        /// <summary>
        /// Get the list of all ProjectCoas and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CoaUnitDto>> GetProjectCoaList(SearchInputDto input);


    }
}
