Ext.define('Chaching.store.receivables.customers.CustomersStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.receivables.customers.CustomersModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        //extraParams: {
        //    organizationUnitId: 0
        //},
        api: {
            create: abp.appPath + 'api/services/app/customerUnit/CreateCustomerUnit',
            read: abp.appPath + 'api/services/app/customerUnit/GetCustomerUnits',
            update: abp.appPath + 'api/services/app/customerUnit/UpdateCustomerUnit',
            destroy: abp.appPath + 'api/services/app/customerUnit/DeleteCustomerUnit'
        }
    },
    idPropertyField: 'customerId'//important to set for add/update of records
});