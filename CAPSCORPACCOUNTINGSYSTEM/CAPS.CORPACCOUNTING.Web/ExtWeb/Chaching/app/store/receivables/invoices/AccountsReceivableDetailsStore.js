/**
 * DataStore to perform Read/Delete operation on Accounts Receivables Details.
 */
Ext.define('Chaching.store.receivables.invoices.AccountsReceivableDetailsStore', {
    extend: 'Chaching.store.base.TransactionDetailsStore',
    model: 'Chaching.model.receivables.invoices.AccountsReceivableDetailsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            accountingDocumentId: null
        },
        api: {
            read: abp.appPath + 'api/services/app/arInvoiceEntryDocument/GetArInvoiceByAccountingDocumentId',
            destroy: abp.appPath + 'api/services/app/arInvoiceEntryDocument/DeleteArInvoiceDetailUnit'
        }
    },
    serverKeyName: 'invoiceEntryDocumentDetailList',
    idPropertyField: 'accountingItemId'//important to set for add/update of records
});