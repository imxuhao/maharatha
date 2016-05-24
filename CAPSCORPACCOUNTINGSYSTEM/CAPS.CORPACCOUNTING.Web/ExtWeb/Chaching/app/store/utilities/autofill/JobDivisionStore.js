Ext.define('Chaching.store.utilities.autofill.JobDivisionStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'job', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'jobId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
