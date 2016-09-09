using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Attachments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Attachments
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAttachedObjectUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create Attached Object Unit 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAttachedObjectUnit(AttachedObjectUnitInput input);

        /// <summary>
        /// Get list of available attachment object types.
        /// </summary>
        /// <returns>Returns NameValueDto Collection.</returns>
        List<NameValueDto> GetTypeofAttachedObjectList();

    }
}
