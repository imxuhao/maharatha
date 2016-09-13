Ext.define('Chaching.store.utilities.TypeOfBatchStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'typeOfBatch', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'typeOfBatchId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter : false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/batchUnit/GetBatchTypeList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
