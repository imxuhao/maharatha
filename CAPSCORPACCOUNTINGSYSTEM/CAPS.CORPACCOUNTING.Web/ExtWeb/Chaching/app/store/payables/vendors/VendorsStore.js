Ext.define('Chaching.store.payables.vendors.VendorsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.payables.vendors.VendorsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        //extraParams: {
        //    organizationUnitId: 0
        //},
        api: {
            create: abp.appPath + 'api/services/app/vendorUnit/CreateVendorUnit',
            read: abp.appPath + 'api/services/app/vendorUnit/GetVendorUnits',
            update: abp.appPath + 'api/services/app/vendorUnit/UpdateVendorUnit',
            destroy: abp.appPath + 'api/services/app/vendorUnit/DeleteVendorUnit'
        }
    },
    idPropertyField: 'vendorId'//important to set for add/update of records
});