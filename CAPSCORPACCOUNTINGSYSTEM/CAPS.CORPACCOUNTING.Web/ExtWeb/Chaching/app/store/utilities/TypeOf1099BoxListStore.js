Ext.define('Chaching.store.utilities.TypeOf1099BoxListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'typeof1099Box', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'typeof1099BoxId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter: false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/vendorUnit/GetTypeof1099T4List',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
