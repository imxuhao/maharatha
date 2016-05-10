Ext.define('Chaching.store.address.AddressStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.address.AddressModel',
    proxy: {
        type: 'chachingProxy',
        extParams:{
            typeofObjectId: 0,
            objectId:0
        },
    actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
    api: {
        read: abp.appPath + 'api/services/app/addressUnit/GetAddressUnits',
        }
    },
    idPropertyField: 'addressId'
   
});