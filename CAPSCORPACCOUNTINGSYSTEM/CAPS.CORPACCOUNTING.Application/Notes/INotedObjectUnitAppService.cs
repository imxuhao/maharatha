using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Attachments.Dto;
using CAPS.CORPACCOUNTING.Notes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Notes
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotedObjectUnitAppService : IApplicationService
    {

        /// <summary>
        /// Create Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateNotedObjectUnit(NotedObjectUnitInput input);

        /// <summary>
        /// Update the Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateNotedObjectUnit(UpdateNotedObjectUnitInput input);

        /// <summary>
        /// Delete the Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteNotedObjectUnit(IdInput<long> input);

        /// <summary>
        /// Get the Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<NotedObjectUnitDto> GetNotedObjectUnit(GetNotedObjectUnitInput input);
    }
}
