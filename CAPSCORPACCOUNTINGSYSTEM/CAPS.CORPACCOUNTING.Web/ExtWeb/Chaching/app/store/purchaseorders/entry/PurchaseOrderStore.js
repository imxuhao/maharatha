/**
 * DataStore to perform CRUD operation on Purchase Order Header.
 */
Ext.define('Chaching.store.purchaseorders.entry.PurchaseOrderStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.purchaseorders.entry.PurchaseOrderModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/CreatePurchaseOrderEntryDocumentUnit',
            read: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/GetPurchaseOrderEntryDocumentUnits',
            update: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/UpdatePurchaseOrderEntryDocumentUnit',
            destroy: abp.appPath + 'api/services/app/purchaseOrderEntryDocument/DeletePurchaseOrderEntryDocumentUnit'
        }
    },
    idPropertyField: 'accountingDocumentId'//important to set for add/update of records
});