/**
 * DataStore to perform CRUD operation on User View Settings.
 */
Ext.define('Chaching.store.manageView.ManageViewStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.manageView.ManageViewModel',
    autoLoad: false,
    storeId: 'manageView.ManageViewStore',
    pageSize:1000,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userViewSettingsUnit/GetUserViewSettingsUnitsByUserId',
            create: abp.appPath + 'api/services/app/userViewSettingsUnit/CreateUserViewSettingsUnit',
            update: abp.appPath + 'api/services/app/userViewSettingsUnit/UpdateUserViewSettingsUnit',
            destroy: abp.appPath + 'api/services/app/userViewSettingsUnit/DeleteUserViewSettingsUnit'
        }
    },
    idPropertyField: 'userViewId'//important to set for add/update of records
});