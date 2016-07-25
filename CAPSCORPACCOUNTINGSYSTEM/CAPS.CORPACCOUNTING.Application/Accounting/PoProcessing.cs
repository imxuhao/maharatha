using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using CAPS.CORPACCOUNTING.CoreHelper;
using CAPS.CORPACCOUNTING.Payables;
using CAPS.CORPACCOUNTING.Payables.Dto;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Accounting
{
    //public class ProcessingPO : CORPACCOUNTINGServiceBase, IEventHandler<EntityChangingEventData<InvoiceEntryDocumentDetailUnit>>, IEventHandler<EntityChangedEventData<InvoiceEntryDocumentDetailUnit>>,
    // ITransientDependency
    //{
    //    private readonly IRepository<PurchaseOrderEntryDocumentDetailUnit, long> _purchaseOrderDetailUnitRepository;
    //    private readonly IRepository<InvoiceEntryDocumentDetailUnit, long> _invoiceEntryDocumentDetailUnitRepository;

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="purchaseOrderDetailUnitRepository"></param>
    //    /// <param name="invoiceEntryDocumentDetailUnitRepository"></param>
    //    public ProcessingPO(IRepository<PurchaseOrderEntryDocumentDetailUnit, long> purchaseOrderDetailUnitRepository,
    //         IRepository<InvoiceEntryDocumentDetailUnit, long> invoiceEntryDocumentDetailUnitRepository)
    //    {
    //        _purchaseOrderDetailUnitRepository = purchaseOrderDetailUnitRepository;
    //        _invoiceEntryDocumentDetailUnitRepository = invoiceEntryDocumentDetailUnitRepository;


    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="eventData"></param>
    //    public async void HandleEvent(EntityChangedEventData<InvoiceEntryDocumentDetailUnit> eventData)
    //    {
    //        InvoiceEntryDocumentDetailUnit InvoiceDetails = new InvoiceEntryDocumentDetailUnit();
    //        if (eventData.Entity.Id != 0)
    //            InvoiceDetails = await _invoiceEntryDocumentDetailUnitRepository.GetAsync(eventData.Entity.Id);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="eventData"></param>
    //    public async void HandleEvent(EntityChangingEventData<InvoiceEntryDocumentDetailUnit> eventData)
    //    {
    //        InvoiceEntryDocumentDetailUnit InvoiceDetails = new InvoiceEntryDocumentDetailUnit();
    //        if (eventData.Entity.Id != 0)
    //        {
    //            //InvoiceDetails = await _invoiceEntryDocumentDetailUnitRepository.GetAsync(eventData.Entity.Id);
    //            var InvoiceDetails1 = await _invoiceEntryDocumentDetailUnitRepository.GetAsync(eventData.Entity.Id);
    //            var query = from invoices in _invoiceEntryDocumentDetailUnitRepository.GetAll()
    //                        where invoices.Id == eventData.Entity.Id
    //                        select invoices;
    //            var results = await query.AsNoTracking().ToListAsync();
    //        }
    //    }


        //    //public async Task UpdatePurchaseOrderDetailUnitByPayType<T>(T originalOrderDetails, T newOrderDetails) where T : InvoiceEntryDocumentDetailUnit, new()
        //    //{
        //    //    decimal resultAmount;
        //    //    decimal? amount = 0;
        //    //    decimal? changeInAmount = 0;
        //    //    var oldPoOrder = new PurchaseOrderEntryDocumentDetailUnit();

        //    //    var poAccountingItemId = !ReferenceEquals(newOrderDetails, null) ? newOrderDetails.PoAccountingItemId.Value : originalOrderDetails.PoAccountingItemId.Value;

        //    //    //get poDetails by poAccountingItemId
        //    //    var poDetail = await _purchaseOrderDetailUnitRepository.GetAsync(poAccountingItemId);

        //    //    //Get Source Type based on class name
        //    //    //ex InvoiceEntryDocumentDetailUnit class returns source='AP'
        //    //    var source = CoreHelpers.GetSourceType(typeof(T).Name);

        //    //    if (!ReferenceEquals(newOrderDetails, null))
        //    //    {

        //    //        //create new copy of object values
        //    //        CoreHelpers.CopyPropertyValues(poDetail, oldPoOrder);

        //    //        // PO is first Time releaving if job or account changes in AP/CC/PC/MC/JE
        //    //        //if po amount and AccountingItemOrigAmount is same they allow to change job or account in AP/CC/PC/MC/JE
        //    //        if (poDetail.Amount == poDetail.AccountingItemOrigAmount)
        //    //        {
        //    //            poDetail.JobId = newOrderDetails.JobId;
        //    //            poDetail.AccountId = newOrderDetails.AccountId;
        //    //            poDetail.SubAccountId1 = newOrderDetails.SubAccountId1;
        //    //            poDetail.SubAccountId2 = newOrderDetails.SubAccountId2;
        //    //            poDetail.SubAccountId3 = newOrderDetails.SubAccountId3;
        //    //            poDetail.ItemMemo = newOrderDetails.ItemMemo;
        //    //            poDetail.TaxRebateId = newOrderDetails.TaxRebateId;
        //    //            poDetail.AccountRef3 = newOrderDetails.AccountRef3;
        //    //            poDetail.VendorId = newOrderDetails.VendorId;
        //    //        }

        //    //        //new invoice Entry 
        //    //        if (ReferenceEquals(originalOrderDetails, null) && newOrderDetails.Id == 0)
        //    //        {

        //    //            //1000 - 100=900
        //    //            //POAmount - releavingamount from AP/CC/PC/MC/JE
        //    //            resultAmount = poDetail.Amount.Value - newOrderDetails.Amount.Value;
        //    //            changeInAmount = -newOrderDetails.Amount.Value;
        //    //        }// Update invoice Entry
        //    //        else
        //    //        {
        //    //            //900+100-200=800
        //    //            //POAmount + Existing releavingamount - newly changing releavingamount  from AP/CC/PC/MC/JE
        //    //            resultAmount = poDetail.Amount.Value + originalOrderDetails.Amount.Value - newOrderDetails.Amount.Value;

        //    //            //history tracking purpose
        //    //            oldPoOrder.Amount = originalOrderDetails.Amount.Value;
        //    //            changeInAmount = -newOrderDetails.Amount.Value;// - oldPoOrder.Amount.Value;

        //    //        }

        //    //        //set OverRelieveAmount based on resultAmount
        //    //        if (resultAmount >= 0)
        //    //        {
        //    //            poDetail.OverRelieveAmount = 0;
        //    //            amount = resultAmount;
        //    //        }
        //    //        else
        //    //        {
        //    //            poDetail.OverRelieveAmount = resultAmount;
        //    //            amount = 0;
        //    //        }

        //    //        poDetail.RemainingAmount = resultAmount;
        //    //        //PO AccountingItemOrigAmount-resultAmount(Calculated amount)
        //    //        poDetail.PendingAmount = poDetail.AccountingItemOrigAmount.Value - resultAmount;

        //    //        ////history tracking purpose
        //    //        //poDetail.Amount = newOrderDetails.Amount.Value;

        //    //        poDetail.Amount = amount;
        //    //        //await CreatePurchaseOrderHistory(oldPoOrder, poDetail, newOrderDetails.Id == 0 ? RecordType.Created : RecordType.Updated,
        //    //        //    source, newOrderDetails.AccountRef3, oldPoOrder.Amount, changeInAmount);


        //    //    }//while deleting details from AP/CC/PC/MC/JE
        //    //    else if (!ReferenceEquals(originalOrderDetails, null))
        //    //    {
        //    //        //900+100=1000
        //    //        //POAmount + Existing releavingamount
        //    //        poDetail.Amount = poDetail.Amount + originalOrderDetails.Amount;

        //    //        //set OverRelieveAmount value
        //    //        if (poDetail.OverRelieveAmount.HasValue)
        //    //        {
        //    //            if (poDetail.OverRelieveAmount - originalOrderDetails.Amount > 0)
        //    //                poDetail.OverRelieveAmount = poDetail.OverRelieveAmount - originalOrderDetails.Amount;
        //    //            else
        //    //                poDetail.OverRelieveAmount = 0;
        //    //        }
        //    //       // await CreatePurchaseOrderHistory(null, poDetail, RecordType.Deleted, source, poDetail.AccountRef3, poDetail.Amount);
        //    //    }
        //    //    await _purchaseOrderDetailUnitRepository.UpdateAsync(poDetail);
        //    //}
        //}
    //}
}
