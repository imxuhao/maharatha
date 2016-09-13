Ext.define('Chaching.store.utilities.autofill.AccountListStore', {
    extend: 'Chaching.store.base.BaseStore',
    requires: ['Chaching.model.financials.accounts.AccountsModel'],
    model: 'Chaching.model.financials.accounts.AccountsModel',

    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/accountUnit/GetAccountById',
        api: {
            read: abp.appPath + 'api/services/app/bankAccountUnit/GetCorporateAccountList',
            create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
            update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
            destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'accountId'//important to set for add/update of records
});


