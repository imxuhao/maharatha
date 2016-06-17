using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{
  public  class PurchaseOrderDetailUnitManager:DomainService
    {
        protected IRepository<PurchaseOrderEntryDocumentDetailUnit, long> PurchaseOrderDetailUnitRepository { get; }
        public PurchaseOrderDetailUnitManager(IRepository<PurchaseOrderEntryDocumentDetailUnit, long> purchaseOrderDetailUnitRepository)
        {
            PurchaseOrderDetailUnitRepository = purchaseOrderDetailUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(PurchaseOrderEntryDocumentDetailUnit input)
        {

            return await PurchaseOrderDetailUnitRepository.InsertAndGetIdAsync(input);
        }


        public virtual async Task UpdateAsync(PurchaseOrderEntryDocumentDetailUnit input)
        {
            await PurchaseOrderDetailUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await PurchaseOrderDetailUnitRepository.DeleteAsync(input.Id);
        }

    }
}
