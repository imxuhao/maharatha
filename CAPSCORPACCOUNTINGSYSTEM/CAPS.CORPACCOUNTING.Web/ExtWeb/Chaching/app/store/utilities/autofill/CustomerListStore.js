Ext.define('Chaching.store.utilities.autofill.CustomerListStore', {
    extend: 'Chaching.store.base.BaseStore',
    requires: ['Chaching.model.utilities.autofill.CustomerModel'],
    model: 'Chaching.model.utilities.autofill.CustomerModel',

    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/customerUnit/GetCustomerUnitsById',
        api : {
            read: abp.appPath + 'api/services/app/jobUnit/GetCustomersList',
            create: abp.appPath + 'api/services/app/customerUnit/CreateCustomerUnit',
            update: abp.appPath + 'api/services/app/customerUnit/UpdateCustomerUnit',
            destroy: abp.appPath + 'api/services/app/customerUnit/DeleteCustomerUnit'
        },
       // url: abp.appPath + 'api/services/app/jobUnit/GetCustomersList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'customerId'//important to set for add/update of records
});


