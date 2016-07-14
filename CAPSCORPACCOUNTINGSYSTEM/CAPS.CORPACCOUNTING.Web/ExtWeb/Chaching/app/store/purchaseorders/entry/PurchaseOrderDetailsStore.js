/**
 * DataStore to perform Read/Delete operation on Purchase Order Details.
 */
Ext.define('Chaching.store.purchaseorders.entry.PurchaseOrderDetailsStore', {
    extend: 'Chaching.store.base.TransactionDetailsStore',
    model: 'Chaching.model.purchaseorders.entry.PurchaseOrderDetailModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            accountingDocumentId: null
        },
        api: {
            read: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/GetPurchaseOrdersByAccountingDocumentId',
            destroy: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/DeletePurchaseOrderDetailUnit'
        }
    },
    serverKeyName: 'purchaseOrderDetailList',
    idPropertyField: 'accountingItemId'//important to set for add/update of records
});