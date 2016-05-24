Ext.define('Chaching.store.utilities.CustomerListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'customerId', convert: function (value, record) {
            return record.get('value');
        }
    }],
   
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/jobUnit/GetCustomersList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});


