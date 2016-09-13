Ext.define('Chaching.store.TimezoneStore', {
    extend: 'Chaching.store.base.BaseStore',
    remoteSort: false,
    remoteFilter: false,
    autoLoad: false,
    fields: [{ name: 'name', type: 'string' }, { name: 'value', type: 'string' }],
    proxy: {
        type: 'chachingProxy',
        actionMethods: {read: 'POST'},
        api: {
            read: abp.appPath + 'api/services/app/timing/GetTimezones'
        }
    }
});