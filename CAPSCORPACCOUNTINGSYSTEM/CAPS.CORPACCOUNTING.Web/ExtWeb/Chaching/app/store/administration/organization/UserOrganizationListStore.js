/**
 * DataStore to perform Read operation on Users Organization.
 */
Ext.define('Chaching.store.administration.organization.UserOrganizationListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields : [{name : 'name'},{name : 'value'}],
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/organizationUnit/GetOrganizationsListByUserId',
        reader: {
            type: 'json',
            rootProperty : 'result'
        }
    }
});