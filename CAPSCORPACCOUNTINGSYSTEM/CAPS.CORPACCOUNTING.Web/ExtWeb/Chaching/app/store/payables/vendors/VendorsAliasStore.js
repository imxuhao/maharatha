Ext.define('Chaching.store.payables.vendors.VendorsAliasStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.payables.vendors.VendorAliasModel',
    proxy: {
        type: 'chachingProxy',
        extParams: {
            id: 0
        },
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/vendorUnit/GetVendorAliasUnits',
        }
    },
    idPropertyField: 'vendorAliasId'

});