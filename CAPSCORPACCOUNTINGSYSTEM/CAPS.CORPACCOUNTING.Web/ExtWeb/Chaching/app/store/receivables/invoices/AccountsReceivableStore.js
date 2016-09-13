/**
 * DataStore to perform CRUD operation on Accounts Payable Header.
 */
Ext.define('Chaching.store.receivables.invoices.AccountsReceivableStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.receivables.invoices.AccountsReceivableModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/arInvoiceEntryDocument/CreateArInvoiceEntryDocumentUnit',
            read: abp.appPath + 'api/services/app/arInvoiceEntryDocument/GetArInvoiceEntryDocumentUnits',
            update: abp.appPath + 'api/services/app/arInvoiceEntryDocument/UpdateArInvoiceEntryDocumentUnit',
            destroy: abp.appPath + 'api/services/app/arInvoiceEntryDocument/DeleteArInvoiceEntryDocumentUnit'
        }
    },
    idPropertyField: 'accountingDocumentId'//important to set for add/update of records
});