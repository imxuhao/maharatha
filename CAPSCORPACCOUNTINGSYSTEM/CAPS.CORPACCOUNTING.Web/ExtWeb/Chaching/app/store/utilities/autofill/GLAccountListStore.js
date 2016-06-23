Ext.define('Chaching.store.utilities.autofill.GLAccountListStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    model: 'Chaching.model.financials.accounts.AccountsModel',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        extraParams: {
            value:true
        },
        // url: abp.appPath + 'api/services/app/list/GetAccountsList',
        urlToGetRecordById: abp.appPath + 'api/services/app/accountUnit/GetAccountUnitsById',
        api: {
            read: abp.appPath + 'api/services/app/vendorUnit/GetAccountsList',
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
