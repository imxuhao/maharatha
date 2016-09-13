Ext.define('Chaching.store.utilities.autofill.VendorsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    model: 'Chaching.model.payables.vendors.VendorsModel',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/vendorUnit/GetVendorUnitsById',
        api: {
            read: abp.appPath + 'api/services/app/list/GetVendorList',
            create: abp.appPath + 'api/services/app/vendorUnit/CreateVendorUnit',
            update: abp.appPath + 'api/services/app/vendorUnit/UpdateVendorUnit',
            destroy: abp.appPath + 'api/services/app/vendorUnit/DeleteVendorUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'vendorId'//important to set for add/update of records
});
