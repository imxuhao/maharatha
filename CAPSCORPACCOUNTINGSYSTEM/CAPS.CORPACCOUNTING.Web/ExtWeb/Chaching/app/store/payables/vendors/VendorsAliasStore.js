/**
 * DataStore to perform CRUD operation on Vendor Alias.
 */
Ext.define('Chaching.store.payables.vendors.VendorsAliasStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.payables.vendors.VendorAliasModel',
    proxy: {
        type: 'chachingProxy',
        extParams: {
            vendorId: 0
        },
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/vendorUnit/GetVendorAliasUnits',
            destroy: abp.appPath + 'api/services/app/vendorUnit/DeleteVendorAliasUnit'
        }
    },
    idPropertyField: 'vendorAliasId'

});