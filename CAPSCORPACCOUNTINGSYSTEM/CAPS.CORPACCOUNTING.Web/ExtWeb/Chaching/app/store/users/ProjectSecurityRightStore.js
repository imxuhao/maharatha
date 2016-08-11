/**
 * ProjectSecurityRightStore for user security.
 */
Ext.define('Chaching.store.users.ProjectSecurityRightStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.ProjectSecurityModel',
    remoteFilter: false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetProjectAccessList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
