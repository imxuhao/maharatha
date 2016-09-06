using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Notes
{
    public class NotedObjectUnitManager : DomainService
    {
        private readonly IRepository<NotedObjectUnit, long> _notedObjectUnitRepository;

        public NotedObjectUnitManager(IRepository<NotedObjectUnit, long> notedObjectUnitRepository)
        {
            _notedObjectUnitRepository = notedObjectUnitRepository;
        }


        [UnitOfWork]
        public virtual async Task<long> CreateAsync(NotedObjectUnit notedObjectUnit)
        {
            return await _notedObjectUnitRepository.InsertAndGetIdAsync(notedObjectUnit);
        }
    }
}
