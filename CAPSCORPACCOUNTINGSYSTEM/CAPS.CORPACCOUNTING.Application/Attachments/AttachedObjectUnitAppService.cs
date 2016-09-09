using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Attachments.Dto;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Storage;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using System.IO;

using AutoMapper;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Helpers;

namespace CAPS.CORPACCOUNTING.Attachments
{
    /// <summary>
    /// 
    /// </summary>
    public class AttachedObjectUnitAppService : CORPACCOUNTINGAppServiceBase, IAttachedObjectUnitAppService
    {
        private readonly AttachedObjectUnitManager _attachedObjectUnitManager;
        private readonly IRepository<AttachedObjectUnit, long> _attachedObjectUnitRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<BinaryObject, Guid> _binaryObjectRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        /// <summary>
        /// 
        /// </summary>
        public AttachedObjectUnitAppService(AttachedObjectUnitManager attachedObjectUnitManager, IRepository<AttachedObjectUnit, long> attachedObjectUnitRepository,
           IBinaryObjectManager binaryObjectManager, IRepository<BinaryObject, Guid> binaryObject, IUnitOfWork iUnitOfWork)
        {
            _attachedObjectUnitManager = attachedObjectUnitManager;
            _attachedObjectUnitRepository = attachedObjectUnitRepository;
            _binaryObjectManager = binaryObjectManager;
            _binaryObjectRepository = binaryObject;
            _iUnitOfWork = iUnitOfWork;
        }

        /// <summary>
        /// Create Attached Object Unit 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateAttachedObjectUnit(CreateAttachedObjectInputUnit input)
        {
            var storedFile = new BinaryObject(AbpSession.TenantId, input.Bytes);

            var binaryObjectId = await _binaryObjectRepository.InsertAndGetIdAsync(storedFile);

            input.UserAttachmentFilesId = binaryObjectId;

            await _attachedObjectUnitManager.CreateAsync(input.MapTo<AttachedObjectUnit>());
            await _iUnitOfWork.SaveChangesAsync();
        }
        /// <summary>
        /// Get list of available attachment object types.
        /// </summary>
        /// <returns>Returns NameValueDto Collection.</returns>
        public List<NameValueDto> GetTypeofAttachedObjectList()
        {
            return EnumList.GetTypeOfAttachedObjectList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateAttachedOjectUnit(UpdateAttachedObjectInputUnit input)
        {
            var attachedObjectUnit = await _attachedObjectUnitRepository.GetAsync(input.AttachedObjectId);

            Mapper.Map(input, attachedObjectUnit);

            await _attachedObjectUnitManager.UpdateAsync(attachedObjectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task DeleteAttachedObjectUnit(IdInput<long> input)
        {
            var attachedObjectUnit = await _attachedObjectUnitRepository.GetAsync(input.Id);
            await _binaryObjectManager.DeleteAsync(attachedObjectUnit.UserAttachmentFilesId.GetValueOrDefault());

            await _attachedObjectUnitManager.DeleteAsync(input);
        }

        /// <summary>
        /// Get the Attached Object Unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListAttachedObjectUnitInput> GetAllAttachedObjectUnit(GetAttachedObjectInputUnit input)
        {
            ListAttachedObjectUnitInput lstAttachedObjectInput = new ListAttachedObjectUnitInput();
            lstAttachedObjectInput.attachedObjectUnitList = new List<AttachedObjectUnitDto>();

            var attachedObjectUnit = await _attachedObjectUnitRepository.GetAllListAsync(o => o.TypeOfObjectId == input.TypeOfObjectId && o.ObjectId == input.ObjectId);
            foreach (var item in attachedObjectUnit)
            {
                AttachedObjectUnitDto attachedObjectDto = new AttachedObjectUnitDto();
                attachedObjectDto.AttachedObjectId = item.Id;

                Mapper.Map(item, attachedObjectDto);

                //TODO: If you want to return bytes also then uncomment below and also in AttachedObjectUnitDto Byte field.

                //var binaryObject = await _binaryObjectRepository.GetAsync(item.UserAttachmentFilesId.GetValueOrDefault());
                //attachedObjectDto.Bytes = binaryObject.Bytes;

                lstAttachedObjectInput.attachedObjectUnitList.Add(attachedObjectDto);
            }

            return lstAttachedObjectInput;
        }

        /// <summary>
        /// GET the File for Download
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AttachedObjectUnitDto> GetFileAttachedObjecUnit(GetFileAttachedObjectInputUnit input)
        {
            var attachedObjectUnit = await _attachedObjectUnitRepository.GetAsync(input.AttachedObjectId);

            AttachedObjectUnitDto attachedObjectDto = new AttachedObjectUnitDto();

            var binaryObject = await _binaryObjectRepository.GetAsync(attachedObjectUnit.UserAttachmentFilesId.GetValueOrDefault());
            Mapper.Map(attachedObjectUnit, attachedObjectDto);

            attachedObjectDto.Bytes = binaryObject.Bytes;

            return attachedObjectDto;
        }
    }
}
