/**
 * DataStore to perform CRUD operation on Users.
 */
Ext.define('Chaching.store.users.UsersStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.UsersModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/user/CreateOrUpdateUserUnit',
            read: abp.appPath + 'api/services/app/user/GetUserUnits',
            update: abp.appPath + 'api/services/app/user/CreateOrUpdateUserUnit',
            destroy: abp.appPath + 'api/services/app/user/DeleteUser'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});