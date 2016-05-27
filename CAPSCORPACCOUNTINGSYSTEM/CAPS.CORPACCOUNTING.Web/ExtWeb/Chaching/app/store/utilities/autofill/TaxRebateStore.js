Ext.define('Chaching.store.utilities.autofill.TaxRebateStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'taxRebate', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'taxRebateId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/list/GetTaxCreditList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
