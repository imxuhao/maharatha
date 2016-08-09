/**
 * ProjectSecurityLeftStore for user security.
 */
Ext.define('Chaching.store.users.ProjectSecurityLeftStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.ProjectSecurityModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetProjectList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
