﻿/**
 * BankSecurityRightStore for user security.
 */
Ext.define('Chaching.store.users.BankSecurityRightStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.BankSecurityModel',
    remoteFilter: false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetBankAccountAccessList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    }
});
