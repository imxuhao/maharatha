Ext.define('Chaching.store.administration.organization.OrganizationsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.administration.organization.OrganizationModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/organizationUnit/CreateOrganizationUnit',
            read: abp.appPath + 'api/services/app/organizationUnit/GetOrganizationUnits',
            update: abp.appPath + 'api/services/app/organizationUnit/UpdateOrganizationUnit',
            destroy: abp.appPath + 'api/services/app/organizationUnit/DeleteOrganizationUnit'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});