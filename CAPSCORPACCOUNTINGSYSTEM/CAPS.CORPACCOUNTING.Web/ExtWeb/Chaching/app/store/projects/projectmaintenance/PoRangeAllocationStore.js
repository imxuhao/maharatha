/**
 * The class is created to load/delete the data for Purchase Order range of a specific job/project
 * Author: Krishna Garad
 * Date:   11/05/2016
 */
/**
 * @class Chaching.store.projects.projectmaintenance.PoRangeAllocationStore
 * Data store load/delete the Purchase order range data for project/job
 */
Ext.define('Chaching.store.projects.projectmaintenance.PoRangeAllocationStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    model: 'Chaching.model.projects.projectmaintenance.PoRangeAllocationModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            jobId: 0
        },
        api: {///TODO : update urls once service is ready
            read: abp.appPath + '',
            destroy:abp.appPath+''
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'poRangeId'//important to set for add/update of records
});
