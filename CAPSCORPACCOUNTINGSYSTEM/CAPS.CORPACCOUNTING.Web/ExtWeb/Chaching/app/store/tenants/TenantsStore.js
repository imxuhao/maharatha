Ext.define('Chaching.store.tenants.TenantsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.tenants.TenantsModel',
    autoLoad: true,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        
        
        api: {
            create: abp.appPath + 'api/services/app/tenant/CreateTenant',
            read: abp.appPath+'api/services/app/tenant/GetTenants',
            update: abp.appPath + 'api/services/app/tenant/UpdateTenant',
            destroy: abp.appPath + 'api/services/app/tenant/DeleteTenant'
        }
    },
    idPropertyField:'id'//important to set for add/update of records
});