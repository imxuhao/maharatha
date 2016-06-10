Ext.define('Chaching.store.utilities.autofill.JobDivisionStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    requires: ['Chaching.model.financials.accounts.DivisionsModel'],
    model : 'Chaching.model.financials.accounts.DivisionsModel',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/jobUnit/GetJobUnitById',
        api: {
            read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
            create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
            update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
            destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'jobId'//important to set for add/update of records
});
