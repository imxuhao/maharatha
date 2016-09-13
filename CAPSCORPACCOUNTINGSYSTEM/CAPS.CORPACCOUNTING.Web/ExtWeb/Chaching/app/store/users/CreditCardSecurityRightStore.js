/**
 * CreditCardSecurityRightStore for user security.
 */
Ext.define('Chaching.store.users.CreditCardSecurityRightStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.CreditCardSecurityModel',
    remoteFilter: false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetCreditCardAccessList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
