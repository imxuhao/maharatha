/**
 * AccountSecurityLeftStore for user security.
 */
Ext.define('Chaching.store.users.AccountSecurityLeftStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.AccountSecurityModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetAccountList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
