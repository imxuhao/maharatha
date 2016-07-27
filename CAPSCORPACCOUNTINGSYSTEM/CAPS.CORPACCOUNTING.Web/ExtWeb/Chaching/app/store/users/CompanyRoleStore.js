Ext.define('Chaching.store.users.CompanyRoleStore', {
    //extend: 'Chaching.store.base.BaseStore',
    extend: 'Ext.data.Store',
    model: 'Chaching.model.users.CompanyRoleModel',
    autoLoad: false,
    groupField: 'tenantName',
    config: {
        searchEntityName: ''
    },
    //groupHeaderTpl: 'Group: {tenantName}',
    proxy: {
        type: 'chachingProxy', //chachingProxy
       // actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        actionMethods: {read: 'POST'},
        api: {
            //create: abp.appPath + 'api/services/app/user/CreateOrUpdateUserUnit',
            read: abp.appPath + 'api/services/app/user/GetTenantListofOrganization'
            //update: abp.appPath + 'api/services/app/user/CreateOrUpdateUserUnit',
            //destroy: abp.appPath + 'api/services/app/user/DeleteUser'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});