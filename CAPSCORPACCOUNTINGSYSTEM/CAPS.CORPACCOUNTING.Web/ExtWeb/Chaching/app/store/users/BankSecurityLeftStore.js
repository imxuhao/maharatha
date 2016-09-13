/**
 * BankSecurityLeftStore for user security.
 */
Ext.define('Chaching.store.users.BankSecurityLeftStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.BankSecurityModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetBankAccountList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
