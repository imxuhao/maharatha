Ext.define('Chaching.store.utilities.autofill.LinesListStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    model: 'Chaching.model.financials.accounts.AccountsModel',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        extraParams: {
            value: false
        },
        // url: abp.appPath + 'api/services/app/list/GetAccountsList',
        urlToGetRecordById: abp.appPath + 'api/services/app/accountUnit/GetAccountUnitsById',
        api: {
            create: abp.appPath + 'api/services/app/linesUnit/CreateLineUnit',
            read: abp.appPath + 'api/services/app/vendorUnit/GetAccountsList',
            update: abp.appPath + 'api/services/app/linesUnit/UpdateLineUnit',
            destroy: abp.appPath + 'api/services/app/linesUnit/DeleteLineUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'accountId'//important to set for add/update of records
});
