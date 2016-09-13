Ext.define('Chaching.store.utilities.BatchListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name', type: 'string' }, { name: 'value', type: 'int' },  {
        name: 'batchDesc', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'batchId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter: false,
    autoLoad : false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/list/GetBatchListByType',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
