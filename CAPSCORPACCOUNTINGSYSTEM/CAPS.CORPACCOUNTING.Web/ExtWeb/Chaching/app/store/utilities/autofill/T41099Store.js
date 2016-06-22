Ext.define('Chaching.store.utilities.autofill.T41099Store', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'typeOf1099T4', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'typeOf1099T4Id', convert: function (value, record) {
            return record.get('value');
        }
    }],
    remoteSort: false,
    remoteFilter: false,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/list/GetTypeof1099T4List',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
