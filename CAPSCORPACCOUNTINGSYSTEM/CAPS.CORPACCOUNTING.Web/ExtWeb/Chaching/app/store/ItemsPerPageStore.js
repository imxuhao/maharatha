Ext.define('Chaching.store.ItemsPerPageStore', {
    extend: 'Chaching.store.base.BaseStore',
    remoteSort: false,
    remoteFilter: false,
    autoLoad: true,
    fields: [{ name: 'pageSize', type: 'int' }],
    data: [{ pageSize: 10 },
            { pageSize: 20 },
            { pageSize: 50 },
            { pageSize: 100 }
    ]
});