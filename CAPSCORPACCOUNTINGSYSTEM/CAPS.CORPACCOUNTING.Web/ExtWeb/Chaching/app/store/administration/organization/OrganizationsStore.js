Ext.define('Chaching.store.administration.organization.OrganizationsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.administration.organization.OrganizationModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/organizationUnit/CreateHostOrganizationUnit',
            read: abp.appPath + 'api/services/app/organizationUnit/GetHostOrganizationUnits',
            update: abp.appPath + 'api/services/app/organizationUnit/UpdateHostOrganizationUnit',
            destroy: abp.appPath + 'api/services/app/organizationUnit/DeleteOrganizationUnit'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});