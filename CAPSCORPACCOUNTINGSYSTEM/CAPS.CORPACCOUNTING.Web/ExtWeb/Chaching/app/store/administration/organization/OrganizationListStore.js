/**
 * DataStore to perform Read operation on Organization.
 */
Ext.define('Chaching.store.administration.organization.OrganizationListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }],
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/organizationUnit/GetHostOrganizationsList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});