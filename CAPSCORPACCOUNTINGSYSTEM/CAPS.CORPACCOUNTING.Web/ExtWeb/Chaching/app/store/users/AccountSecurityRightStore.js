/**
 * DataStore to perform Read operation on Accounts Resrictions.
 */
Ext.define('Chaching.store.users.AccountSecurityRightStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.AccountSecurityModel',
    //remoteFilter:false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetAccountAccessList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
