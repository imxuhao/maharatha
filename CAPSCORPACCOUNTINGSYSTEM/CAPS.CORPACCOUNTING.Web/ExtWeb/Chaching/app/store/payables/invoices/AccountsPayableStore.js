/**
 * DataStore to perform CRUD operation on Accounts Payable Header.
 */
Ext.define('Chaching.store.payables.invoices.AccountsPayableStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.payables.invoices.AccountsPayableModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/aPHeaderTransactions/CreateAPHeaderTransactionUnit',
            read: abp.appPath + 'api/services/app/aPHeaderTransactions/GetAPHeaderTransactionUnits',
            update: abp.appPath + 'api/services/app/aPHeaderTransactions/UpdateAPHeaderTransactionUnit',
            destroy: abp.appPath + 'api/services/app/aPHeaderTransactions/DeleteAPHeaderTransactionUnit'
        }
    },
    idPropertyField: 'accountingDocumentId'//important to set for add/update of records
});