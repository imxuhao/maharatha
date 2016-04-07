Ext.define('Chaching.store.users.UsersStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.UsersModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },


        api: {
            create: abp.appPath + 'api/services/app/user/CreateOrUpdateUser',
            read: abp.appPath + 'api/services/app/user/GetUsers',
            update: abp.appPath + 'api/services/app/user/CreateOrUpdateUser',
            destroy: abp.appPath + 'api/services/app/user/DeleteUser'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});