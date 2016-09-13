Ext.define('Chaching.store.maintenance.CacheStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.maintenance.CacheModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { read: 'POST'},
        api: {
            read: abp.appPath + 'api/services/app/caching/GetAllCaches'
        }
    },
    idPropertyField: 'id'
});