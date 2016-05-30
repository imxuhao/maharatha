Ext.define('Chaching.store.utilities.autofill.JobDivisionStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'job', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'jobId', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditJob', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditJobId', convert: function (value, record) {
            return record.get('name');
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
