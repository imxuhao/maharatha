/**
 * DataStore to perform Read operation on Tenant users.
 */
Ext.define('Chaching.store.tenants.TenantUserListStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.tenants.TenantUserModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/commonLookup/FindUsersByTenant',
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    }
});