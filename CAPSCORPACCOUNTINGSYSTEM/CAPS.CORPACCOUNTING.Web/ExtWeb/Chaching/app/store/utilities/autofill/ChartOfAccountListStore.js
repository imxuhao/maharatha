Ext.define('Chaching.store.utilities.autofill.ChartOfAccountListStore', {
    extend: 'Chaching.store.base.BaseStore',
    requires: ['Chaching.model.financials.accounts.ChartOfAccountsModel'],
    model: 'Chaching.model.financials.accounts.ChartOfAccountsModel',

    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/coaUnit/GetCoaUnitById',
        api: {
            read: abp.appPath + 'api/services/app/bankAccountUnit/GetCorporateAccountList',
            create: abp.appPath + 'api/services/app/coaUnit/CreateCoaUnit',
            update: abp.appPath + 'api/services/app/coaUnit/UpdateCoaUnit',
            destroy: abp.appPath + 'api/services/app/coaUnit/DeleteCoaUnit'
        },
        // url: abp.appPath + 'api/services/app/jobUnit/GetCustomersList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'coaId'//important to set for add/update of records
});


