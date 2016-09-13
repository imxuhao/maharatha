using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;
using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{
  public class PurchaseOrderEntryDocumentUnitManager:DomainService
    {

        protected IRepository<PurchaseOrderEntryDocumentUnit, long> PurchaseOrderEntryDocumentUnitRepository { get; }
        public PurchaseOrderEntryDocumentUnitManager(IRepository<PurchaseOrderEntryDocumentUnit, long> purchaseOrderEntryDocumentUnitrepository)
        {
            PurchaseOrderEntryDocumentUnitRepository = purchaseOrderEntryDocumentUnitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(PurchaseOrderEntryDocumentUnit purchaseOrder)
        {
            await ValidateJournalUnitAsync(purchaseOrder);
            return await PurchaseOrderEntryDocumentUnitRepository.InsertAndGetIdAsync(purchaseOrder);
        }


        public virtual async Task UpdateAsync(PurchaseOrderEntryDocumentUnit purchaseOrder)
        {
            await ValidateJournalUnitAsync(purchaseOrder);
            await PurchaseOrderEntryDocumentUnitRepository.UpdateAsync(purchaseOrder);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await PurchaseOrderEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }
        protected virtual async Task ValidateJournalUnitAsync(PurchaseOrderEntryDocumentUnit purchaseOrderItem)
        {
            //Validating if Duplicate DocumentReference(Journal#) exists
            if (PurchaseOrderEntryDocumentUnitRepository != null)
            {

                var purchaseOrder = (await PurchaseOrderEntryDocumentUnitRepository.
                    GetAllListAsync(p => p.DocumentReference == purchaseOrderItem.DocumentReference && p.OrganizationUnitId == purchaseOrderItem.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.PurchaseOrders));

                if (purchaseOrderItem.Id == 0)
                {
                    if (purchaseOrder.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Purchase Order#", purchaseOrderItem.DocumentReference));
                    }
                }
                else
                {
                    if (purchaseOrder.FirstOrDefault(p => p.Id != purchaseOrderItem.Id && p.DocumentReference == purchaseOrderItem.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Purchase Order#", purchaseOrderItem.DocumentReference));
                    }
                }

            }
        }




    }
}
