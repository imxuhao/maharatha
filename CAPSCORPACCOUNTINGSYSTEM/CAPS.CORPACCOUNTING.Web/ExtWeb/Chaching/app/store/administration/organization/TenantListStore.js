/**
 * DataStore to perform Read operation on Tenants.
 */
Ext.define('Chaching.store.administration.organization.TenantListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'tenantId' }, { name: 'tenantName' }],
    remoteSort : false,
    remoteFilter: false,
    pageSize : 1000,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/tenant/GetTenantListByOrganizationId',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});