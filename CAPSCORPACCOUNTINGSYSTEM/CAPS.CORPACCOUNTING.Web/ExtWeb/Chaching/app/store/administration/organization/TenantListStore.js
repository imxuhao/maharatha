Ext.define('Chaching.store.administration.organization.TenantListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'tenantId' }, { name: 'tenantName' }],
    remoteSort : false,
    remoteFilter : false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/organizationUnit/GetOrganizationsListByUserId',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});