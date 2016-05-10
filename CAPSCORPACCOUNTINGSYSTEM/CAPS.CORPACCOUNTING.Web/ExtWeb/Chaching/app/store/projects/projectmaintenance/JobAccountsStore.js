Ext.define('Chaching.store.projects.projectmaintenance.JobAccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize:1000,
    model: 'Chaching.model.projects.projectmaintenance.JobAccountsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            chartofAccountId: 0,
            jobId: 0
        },
        api: {
            read: abp.appPath + 'api/services/app/jobUnit/GetLineLIstByProjectCoa'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'jobAccountId'//important to set for add/update of records
});
