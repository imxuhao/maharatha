Ext.define('Chaching.store.utilities.autofill.BankAccountListStore', {
    extend: 'Chaching.store.base.BaseStore',
    requires: ['Chaching.model.utilities.autofill.BankAccountModel'],
    model: 'Chaching.model.utilities.autofill.BankAccountModel',

    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/bankAccountUnit/GetBankAccountUnitsById',
        api: {
            read: abp.appPath + 'api/services/app/list/GetBankAccountList',
            create: abp.appPath + 'api/services/app/bankAccountUnit/CreateBankAccountUnit',
            update: abp.appPath + 'api/services/app/bankAccountUnit/UpdateBankAccountUnit',
            destroy: abp.appPath + 'api/services/app/bankAccountUnit/DeleteBankAccountUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'bankAccountId'//important to set for add/update of records
});


