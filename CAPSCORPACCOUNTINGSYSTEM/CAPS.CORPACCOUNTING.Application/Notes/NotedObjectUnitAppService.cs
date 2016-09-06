using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Notes.Dto;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Storage;
using System.IO;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Attachments.Dto;
using CAPS.CORPACCOUNTING.Attachments;

namespace CAPS.CORPACCOUNTING.Notes
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class NotedObjectUnitAppService : CORPACCOUNTINGAppServiceBase, INotedObjectUnitAppService
    {
        private readonly NotedObjectUnitManager _notedObjectUnitManager;

        /// <summary>
        /// 
        /// </summary>
        public NotedObjectUnitAppService(NotedObjectUnitManager notedObjectUnitManager)
        {
            _notedObjectUnitManager = notedObjectUnitManager;
        }

        /// <summary>
        /// Create noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateNotedObjectUnit(NotedObjectUnitInput input)
        {
            if (!string.IsNullOrEmpty(input.Notes))
            {
                var noteObjectUnit = input.MapTo<NotedObjectUnit>();
                await _notedObjectUnitManager.CreateAsync(noteObjectUnit);
            }
        }
    }
}
