/**
 * DataStore to perform CRUD operation on Company Users.
 */
Ext.define('Chaching.store.administration.organization.CompanyUsersStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.UsersModel',
    pageSize : 100,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/user/CreateOrUpdateUser',
            read: abp.appPath + 'api/services/app/organizationUnit/GetOrganizationUnitUsers',
            update: abp.appPath + 'api/services/app/user/CreateOrUpdateUser',
            destroy: abp.appPath + 'api/services/app/user/DeleteUser'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});