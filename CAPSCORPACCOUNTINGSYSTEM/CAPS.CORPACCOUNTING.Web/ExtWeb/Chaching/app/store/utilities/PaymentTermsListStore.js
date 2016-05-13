Ext.define('Chaching.store.utilities.PaymentTermsListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'paymentTerms', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'paymentTermsId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/vendorUnit/GetPaymentTermsList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});