Ext.define('Chaching.store.utilities.autofill.AccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    model: 'Chaching.model.financials.accounts.AccountsModel',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        extraParams: {
            jobId:null
        },
        url: abp.appPath + 'api/services/app/list/GetAccountsList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
