Ext.define('Chaching.store.roles.RolesStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.roles.RolesModel',
    autoLoad: true,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/role/CreateOrUpdateRole',
            read: abp.appPath + 'api/services/app/role/GetRoles',
            update: abp.appPath + 'api/services/app/role/CreateOrUpdateRole',
            destroy: abp.appPath + 'api/services/app/role/DeleteRole'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});