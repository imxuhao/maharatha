using Abp.Application.Services;
using Abp.Application.Services.Dto;
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
        Task CreateAttachedObjectUnit(CreateAttachedObjectInputUnit input);

        /// <summary>
        /// Get list of available attachment object types.
        /// </summary>
        /// <returns>Returns NameValueDto Collection.</returns>
        List<NameValueDto> GetTypeofAttachedObjectList();

        /// <summary>
        /// Update Attached Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAttachedOjectUnit(UpdateAttachedObjectInputUnit input);

        /// <summary>
        /// Delete Attached Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAttachedObjectUnit(IdInput<long> input);

        /// <summary>
        /// GET all  Attached Attachments
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListAttachedObjectUnitInput> GetAllAttachedObjectUnit(GetAttachedObjectInputUnit input);

        /// <summary>
        /// Downloads the attachment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AttachedObjectUnitDto> GetFileAttachedObjecUnit(GetFileAttachedObjectInputUnit input);

    }
}
