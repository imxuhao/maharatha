/**
 * DataStore to perform CRUD operation on Credit Card History.
 */
Ext.define('Chaching.store.creditcard.entry.CreditCardHistoryStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.creditcard.entry.CreditCardHistoryModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            //create: abp.appPath + 'api/services/app/bankAccountUnit/CreateBankAccountUnit'//,
            read: abp.appPath + 'api/services/app/bankAccountUnit/GetBankAccountUnits',
            //update: abp.appPath + 'api/services/app/bankAccountUnit/UpdateBankAccountUnit',
            destroy: abp.appPath + 'api/services/app/bankAccountUnit/DeleteBankAccountUnit'
        }
    }
    //,
    //idPropertyField: 'bankAccountId'//important to set for add/update of records
});