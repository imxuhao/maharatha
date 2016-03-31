Ext.define('Chaching.store.tenants.TenantsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.tenants.TenantsModel',
    autoLoad: true,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type':'application/json;charset=UTF-8'
        },
        writer: {
            type:'json'
        },
        api: {
            create: abp.appPath + 'api/services/app/tenant/CreateTenant',
            read: abp.appPath+'api/services/app/tenant/GetTenants',
            update: abp.appPath + 'api/services/app/tenant/UpdateTenant',
            destroy: abp.appPath + 'api/services/app/tenant/DeleteTenant'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    }
});