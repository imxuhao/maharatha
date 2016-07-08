Ext.define('Chaching.store.administration.organization.OrganizationListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }],
    proxy: {
        type: 'chachingProxy',
        //extraParams: {
        //    organizationUnitId: null
        //},
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/organizationUnit/GetHostOrganizationsList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
        //api: {
        //    create: abp.appPath + '',
        //    read: abp.appPath + 'api/services/app/organizationUnit/GetOrganizationsListByUserId',
        //    update: abp.appPath + '',
        //    destroy: abp.appPath + ''
        //}
    }
});