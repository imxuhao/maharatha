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
using Abp.Application.Services.Dto;
using AutoMapper;
using System.Data.Entity;

namespace CAPS.CORPACCOUNTING.Notes
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class NotedObjectUnitAppService : CORPACCOUNTINGAppServiceBase, INotedObjectUnitAppService
    {
        private readonly NotedObjectUnitManager _notedObjectUnitManager;
        private readonly IRepository<NotedObjectUnit, long> _notedObjectUnitRepository;
        /// <summary>
        /// 
        /// </summary>
        public NotedObjectUnitAppService(NotedObjectUnitManager notedObjectUnitManager, IRepository<NotedObjectUnit, long> notedObjectUnitRepository)
        {
            _notedObjectUnitManager = notedObjectUnitManager;
            _notedObjectUnitRepository = notedObjectUnitRepository;
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

        /// <summary>
        /// Update the Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateNotedObjectUnit(UpdateNotedObjectUnitInput input)
        {
            var notedObjectUnit = await _notedObjectUnitRepository.GetAsync(input.NotedObjectId);
            Mapper.Map(input, notedObjectUnit);
            await _notedObjectUnitManager.UpdateAsync(notedObjectUnit);
        }

        /// <summary>
        /// Delete the Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteNotedObjectUnit(IdInput<long> input)
        {
            await _notedObjectUnitManager.DeleteAsync(input);
        }

        /// <summary>
        /// GET the Noted Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<NotedObjectUnitDto> GetNotedObjectUnit(GetNotedObjectUnitInput input)
        {
            var notedObjectUnit = await _notedObjectUnitRepository.GetAll().Where(r => r.TypeOfObjectId == input.TypeOfObjectId && r.ObjectId == input.ObjectId).FirstOrDefaultAsync();
            NotedObjectUnitDto notedObjectUnitDto = notedObjectUnit.MapTo<NotedObjectUnitDto>();

            return notedObjectUnitDto;
        }
    }
}
