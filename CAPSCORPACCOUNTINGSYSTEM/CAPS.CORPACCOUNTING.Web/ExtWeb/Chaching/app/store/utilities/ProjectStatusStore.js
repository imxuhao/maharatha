/**
 * This class is created as a utility store to load project status list
 * Author: Krishna Garad
 * Date Created: 12/05/2016
 */
/**
 * @class Chaching.store.utilities.ProjectStatusStore
 * Utility Store for ProjectStatusList
 */
Ext.define('Chaching.store.utilities.ProjectStatusStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {

        name: 'jobStatusName', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'typeOfJobStatusId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter : false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/jobUnit/GetProjectStatusList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
