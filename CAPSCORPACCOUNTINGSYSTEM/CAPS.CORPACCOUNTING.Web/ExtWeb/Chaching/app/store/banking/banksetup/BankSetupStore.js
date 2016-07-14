/**
 * DataStore to perform CRUD operation on Banks.
 */
Ext.define('Chaching.store.banking.banksetup.BankSetupStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.banking.banksetup.BankSetupModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/bankAccountUnit/CreateBankAccountUnit',
            read: abp.appPath + 'api/services/app/bankAccountUnit/GetBankAccountUnits',
            update: abp.appPath + 'api/services/app/bankAccountUnit/UpdateBankAccountUnit',
            destroy: abp.appPath + 'api/services/app/bankAccountUnit/DeleteBankAccountUnit'
        }
    },
    idPropertyField: 'bankAccountId'//important to set for add/update of records
});
