Ext.define('Chaching.store.roles.RolesTreeStore', {
    extend: 'Chaching.store.base.BaseTreeStore',
    model: 'Chaching.model.roles.RolePermissionsModel',
    //proxy: {
    //    type: 'chachingProxy',
    //    actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
    //    api: {
    //       // create: abp.appPath + 'api/services/app/role/CreateOrUpdateRole',
    //        read: abp.appPath + 'api/services/app/role/GetRoleForEdit',
    //       // update: abp.appPath + 'api/services/app/role/CreateOrUpdateRole',
    //       // destroy: abp.appPath + 'api/services/app/role/DeleteRole'
    //    }
    //},
    //listeners: {
    //    load: function (records, operation, success) {

    //    }
    //}
   // idPropertyField: 'id'//important to set for add/update of records
});