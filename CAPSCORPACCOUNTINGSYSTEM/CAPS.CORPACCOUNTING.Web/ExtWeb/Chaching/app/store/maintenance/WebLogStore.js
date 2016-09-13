Ext.define('Chaching.store.maintenance.WebLogStore', {
    extend: 'Ext.data.ArrayStore',
    fields: [
              'data'
            ],
    proxy: {
        type: 'ajax',
        actionMethods: { read: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/webLog/GetLatestWebLogs'            
        },
        reader: {
            type: 'json',
            rootProperty: 'result.latesWebLogLines'
        }
    }
});