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
using System.Linq.Dynamic;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Helpers;

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
        private readonly IRepository<User, long> _userUnitRepository;

        /// <summary>
        /// 
        /// </summary>
        public NotedObjectUnitAppService(NotedObjectUnitManager notedObjectUnitManager, IRepository<NotedObjectUnit, long> notedObjectUnitRepository, IRepository<User, long> userUnitRepository)
        {
            _notedObjectUnitManager = notedObjectUnitManager;
            _notedObjectUnitRepository = notedObjectUnitRepository;
            _userUnitRepository = userUnitRepository;
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
        public async Task<NotedObjectUnitListDto> GetNotedObjectUnit(GetNotedObjectUnitInput input)
        {
            NotedObjectUnitListDto notedObjectUnitListDto = new NotedObjectUnitListDto();
            notedObjectUnitListDto.NotedObjectUnitDto = new List<NotedObjectUnitDto>();

            var notedObjectUnitList = await (from note in _notedObjectUnitRepository.GetAll()
                                                join user in _userUnitRepository.GetAll() on note.CreatorUserId equals user.Id into notes
                                                from noteinner in notes.DefaultIfEmpty()
                                                where note.TypeOfObjectId == input.TypeOfObjectId && note.ObjectId == input.ObjectId
                                                select new { note, noteinner.UserName }).ToListAsync();

            if (notedObjectUnitList.Any())
            {
                foreach (var item in notedObjectUnitList)
                {
                    NotedObjectUnitDto notedObjectUnitDto = new NotedObjectUnitDto();
                    notedObjectUnitDto.NotedObjectId = item.note.Id;
                    notedObjectUnitDto.CreatedUser = item.UserName;
                    notedObjectUnitDto.CreationTime = item.note.CreationTime;
                    
                    Mapper.Map(item.note, notedObjectUnitDto);
                    notedObjectUnitListDto.NotedObjectUnitDto.Add(notedObjectUnitDto);
                }
            }

            return notedObjectUnitListDto;
        }

        public async Task<List<NotedObjectUnitDto>> GetNotedObjectForRavi(GetNotedObjectUnitInput input)
        {
            var notedObjectUnitList = from note in _notedObjectUnitRepository.GetAll()
                join user in _userUnitRepository.GetAll() on note.CreatorUserId equals user.Id into notes
                from noteinner in notes.DefaultIfEmpty()
                where note.TypeOfObjectId == input.TypeOfObjectId && note.ObjectId == input.ObjectId
                select new
                {
                    NotedObjectId = note.Id,
                    Notes=note.Notes,
                    CreatedUser= noteinner.UserName,
                    TypeOfObjectId=note.TypeOfObjectId,
                    ObjectId=note.ObjectId,
                    CreationTime=note.CreationTime,
                    CreatedUserId=note.CreatorUserId
                };
            var result = await notedObjectUnitList.OrderBy(Helper.GetSort("CreationTime DESC", "")).ToListAsync();//
            return new List<NotedObjectUnitDto>(result.Select(item =>
            {
                var dto = item.MapTo<NotedObjectUnitDto>();
                dto.CreationTime = item.CreationTime;
                //TODO:change based on history tracking. Make sure user only can edit/delete his own added notes.
                if (item.CreatedUserId > 0)//TODO:&&item.CreatedUserId===CurrentLogin userId.
                {
                    dto.AllowEdit = true;
                    dto.AllowDelete = true;
                    dto.CreatedUser = item.CreatedUser;
                }
                else
                {
                    dto.AllowEdit = false;
                    dto.AllowDelete = false;
                    dto.CreatedUser = "System Generated.";
                }

                return dto;
            }));
        }
    }
}
