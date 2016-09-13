using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Banking
{
   public class BatchUnitManager : DomainService
    {

        protected IRepository<BatchUnit, int> BatchUnitRepository { get; }

        public BatchUnitManager(IRepository<BatchUnit, int> batchUnitRepository)
        {
            BatchUnitRepository = batchUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(BatchUnit batchUnit)
        {
            await BatchUnitRepository.InsertAsync(batchUnit);
        }

        public virtual async Task UpdateAsync(BatchUnit batchUnit)
        {
            await BatchUnitRepository.UpdateAsync(batchUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await BatchUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

    }
}
