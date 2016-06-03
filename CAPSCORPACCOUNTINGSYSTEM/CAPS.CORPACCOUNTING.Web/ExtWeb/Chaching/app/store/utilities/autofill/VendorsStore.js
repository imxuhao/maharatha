Ext.define('Chaching.store.utilities.autofill.VendorsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'vendorName', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'vendorId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/list/GetVendorList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
