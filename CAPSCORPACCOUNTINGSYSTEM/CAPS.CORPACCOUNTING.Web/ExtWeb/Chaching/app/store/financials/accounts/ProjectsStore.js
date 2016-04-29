Ext.define('Chaching.store.financials.accounts.ProjectsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.DivisionsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
            read: abp.appPath + 'api/services/app/jobUnit/GetDivisionUnits',
            update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
            destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
        }
    },
    idPropertyField: 'jobId'//important to set for add/update of records
});
