/**
 * DataStore to perform Read operation on Customers.
 */
Ext.define('Chaching.store.customers.CustomersStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.customers.CustomersModel',
    pageSize: 1000,
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/customerUnit/GetCustomerUnits',
        reader: {
            type: 'json',
            rootProperty: 'result.items'
        }
    }
});
