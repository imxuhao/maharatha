Ext.define('Chaching.store.utilities.autofill.RollupAccountListStore', {
    extend: 'Chaching.store.base.BaseStore',
    model : 'Chaching.model.financials.accounts.AccountsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            Id: null
        },
        urlToGetRecordById: abp.appPath + 'api/services/app/accountUnit/GetAccountById',
        api: {
            read: abp.appPath + 'api/services/app/accountUnit/GetRollupAccountsList',
            create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
            update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
            destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'accountId'
});
