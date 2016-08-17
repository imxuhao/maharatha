/**
 * DataStore to perform CRUD operation on Tenants/Company.
 */
Ext.define('Chaching.store.tenants.TenantsStore', {
    extend: 'Ext.data.Store',
    remoteSort: true,
    remoteFilter: true,
    pageSize: 10, // items per page
    model: 'Chaching.model.tenants.TenantsModel',
    config : {
        searchEntityName : 'Tenant'
    },
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/tenant/CreateTenantUnit',
            read: abp.appPath + 'api/services/app/tenant/GetTenantUnits',
            update: abp.appPath + 'api/services/app/tenant/UpdateTenantUnit',
            destroy: abp.appPath + 'api/services/app/tenant/DeleteTenant'
        }
    },
    idPropertyField:'id'//important to set for add/update of records
});