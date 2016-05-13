Ext.define('Chaching.store.utilities.TypeOfAddressListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'addressType', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'addressTypeId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/vendorUnit/GetTypeofAddressList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});

