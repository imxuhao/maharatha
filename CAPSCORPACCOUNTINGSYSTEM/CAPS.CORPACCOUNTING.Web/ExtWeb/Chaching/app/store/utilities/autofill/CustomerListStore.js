Ext.define('Chaching.store.utilities.autofill.CustomerListStore', {
    extend: 'Chaching.store.base.BaseStore',
    requires: ['Chaching.model.utilities.autofill.CustomerModel'],
    model: 'Chaching.model.utilities.autofill.CustomerModel',

    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById : abp.appPath + 'api/services/app/vendorUnit/GetVendorUnitsById',
        api : {
            read: abp.appPath + 'api/services/app/jobUnit/GetCustomersList',
            create: abp.appPath + 'api/services/app/vendorUnit/CreateVendorUnit',
            update: abp.appPath + 'api/services/app/vendorUnit/UpdateVendorUnit',
            destroy: abp.appPath + 'api/services/app/vendorUnit/DeleteVendorUnit'
        },
       // url: abp.appPath + 'api/services/app/jobUnit/GetCustomersList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'vendorId'//important to set for add/update of records
});


