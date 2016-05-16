Ext.define('Chaching.store.utilities.TypeOfTaxListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'typeofTax', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'typeofTaxId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/vendorUnit/GetTypeOfTaxList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
