Ext.define('Chaching.store.utilities.autofill.DivisionListStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    requires: ['Chaching.model.financials.accounts.DivisionsModel'],
    model: 'Chaching.model.financials.accounts.DivisionsModel',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/jobUnit/GetJobUnitById',
        api: {
            //read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
            read: abp.appPath + 'api/services/app/jobUnit/GetDivisionList',
            create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
            update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
            destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
        },
        // url: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'jobId'//important to set for add/update of records
});
