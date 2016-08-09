/**
 * CreditCardSecurityLeftStore for user security.
 */
Ext.define('Chaching.store.users.CreditCardSecurityLeftStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.CreditCardSecurityModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetCreditCardList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
