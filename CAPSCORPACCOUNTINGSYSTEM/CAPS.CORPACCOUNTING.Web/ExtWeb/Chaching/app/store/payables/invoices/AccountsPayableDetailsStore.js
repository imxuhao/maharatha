Ext.define('Chaching.store.payables.invoices.AccountsPayableDetailsStore', {
    extend: 'Chaching.store.base.TransactionDetailsStore',
    model: 'Chaching.model.payables.invoices.AccountsPayableDetailsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            accountingDocumentId: null
        },
        api: {
            read: abp.appPath + 'api/services/app/aPHeaderTransactions/GetAPHeaderTransactionDetailUnitsByAccountingDocumentId',
            destroy: abp.appPath + 'api/services/app/aPHeaderTransactions/DeleteAPHeaderTransactionUnit'
        }
    },
    serverKeyName: 'invoiceEntryDocumentDetailList',
    idPropertyField: 'accountingItemId'//important to set for add/update of records
});