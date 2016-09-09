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
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<BinaryObject, Guid> _binaryObject;
        private readonly IUnitOfWork _iUnitOfWork;


        /// <summary>
        /// 
        /// </summary>
        public AttachedObjectUnitAppService(AttachedObjectUnitManager attachedObjectUnitManager,
           IBinaryObjectManager binaryObjectManager, IRepository<BinaryObject, Guid> binaryObject, IUnitOfWork iUnitOfWork)
        {
            _attachedObjectUnitManager = attachedObjectUnitManager;
            _binaryObjectManager = binaryObjectManager;
            _binaryObject = binaryObject;
            _iUnitOfWork = iUnitOfWork;
        }

        /// <summary>
        /// Create Attached Object Unit 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateAttachedObjectUnit(AttachedObjectUnitInput input)
        {
            if (input.CreateAttachedObjectUnit.Any())
            {
                foreach (var item in input.CreateAttachedObjectUnit)
                {
                    var dto = item.MapTo<AttachedObjectUnit>();

                    var storedFile = new BinaryObject(AbpSession.TenantId, item.Bytes);

                    var binaryObjectId = await _binaryObject.InsertAndGetIdAsync(storedFile);

                    item.UserAttachmentFilesId = binaryObjectId;

                    await _attachedObjectUnitManager.CreateAsync(item.MapTo<AttachedObjectUnit>());
                    await _iUnitOfWork.SaveChangesAsync();
                }
            }
        }
        /// <summary>
        /// Get list of available attachment object types.
        /// </summary>
        /// <returns>Returns NameValueDto Collection.</returns>
        public List<NameValueDto> GetTypeofAttachedObjectList()
        {
            return EnumList.GetTypeOfAttachedObjectList();
        }
    }
}
