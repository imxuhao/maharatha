using Abp.Application.Services;
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
        /// Create noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateNotedObjectUnit(NotedObjectUnitInput input);
    }
}
