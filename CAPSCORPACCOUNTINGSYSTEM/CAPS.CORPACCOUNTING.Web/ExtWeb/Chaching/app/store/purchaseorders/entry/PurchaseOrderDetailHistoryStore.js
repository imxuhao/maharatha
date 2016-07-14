/**
 * DataStore to perform Read operation on Purchase Order Details History.
 */
Ext.define('Chaching.store.purchaseorders.entry.PurchaseOrderDetailHistoryStore', {
    extend: 'Chaching.store.base.TransactionDetailsStore',
    model: 'Chaching.model.purchaseorders.entry.PurchaseOrderDetailModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            accountingDocumentId: null
        },
        api: {
            read: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/GetPurchaseOrdersByAccountingDocumentId'///TODO:replace store with new store once history service is ready
        }
    },
    idPropertyField: 'accountingItemId'//important to set for add/update of records
});