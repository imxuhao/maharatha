/**
 * DataStore to perform CRUD operation on Projects/Jobs.
 */
Ext.define('Chaching.store.projects.projectmaintenance.ProjectsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.Jobcasting.JobCommercialsModel',
    proxy: {
        type: 'chachingProxy',
        urlToGetRecordById:abp.appPath + 'api/services/app/jobUnit/GetJobUnitById',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            organizationUnitId:null

        },
        api: {
            create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
            read: abp.appPath + 'api/services/app/jobUnit/GetJobUnits',
            update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
            destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
        }
    },
    idPropertyField: 'jobId'//important to set for add/update of records
});
