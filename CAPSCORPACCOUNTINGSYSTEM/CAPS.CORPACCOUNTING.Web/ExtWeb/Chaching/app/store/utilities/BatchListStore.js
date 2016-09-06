Ext.define('Chaching.store.utilities.BatchListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name', type: 'string' }, { name: 'value', type: 'int' },  {
        name: 'description', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'batchId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter: false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/creditCardCompany/GetBatchList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
