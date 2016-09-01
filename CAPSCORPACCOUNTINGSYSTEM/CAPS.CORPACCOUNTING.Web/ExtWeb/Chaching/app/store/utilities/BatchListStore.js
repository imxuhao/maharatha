Ext.define('Chaching.store.utilities.BatchListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'batchId', type : 'int' }, { name: 'description', type : 'string' }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/batchUnit/GetBatchList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
