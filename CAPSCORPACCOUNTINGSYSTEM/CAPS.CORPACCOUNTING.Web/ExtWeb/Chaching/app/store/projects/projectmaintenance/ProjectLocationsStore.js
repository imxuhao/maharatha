/**
 * DataStore to perform CRUD operation on Job/Project Locations.
 */
Ext.define('Chaching.store.projects.projectmaintenance.ProjectLocationsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.Jobcasting.JobLocationsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            jobId: 0
        },
        api: {
            read: abp.appPath + 'api/services/app/jobLocation/GetJobLocationUnitsByJobId',
            destroy: abp.appPath + 'api/services/app/jobLocation/DeleteJobLocationUnit'
        }
    },
    idPropertyField: 'jobLocationId'//important to set for add/update of records
});
