/**
 * This class is created as a utility store to load project type list
 * Author: Krishna Garad
 * Date Created: 12/05/2016
 */
/**
 * @class Chaching.store.utilities.ProjectTypeStore
 * Utility Store for ProjectTypeList
 */
Ext.define('Chaching.store.utilities.ProjectTypeStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {

        name: 'typeofProjectName', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'typeofProjectId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter : false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/jobUnit/GetProjectTypeList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
