using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Attachments
{
    public class AttachedObjectUnitManager : DomainService
    {
        private readonly IRepository<AttachedObjectUnit, long> _attachedObjectUnitRepository;

        public AttachedObjectUnitManager(IRepository<AttachedObjectUnit, long> attachedObjectUnitRepository)
        {
            _attachedObjectUnitRepository = attachedObjectUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(AttachedObjectUnit attachedObjectUnit)
        {
            return await _attachedObjectUnitRepository.InsertAndGetIdAsync(attachedObjectUnit);
        }
    }
}
