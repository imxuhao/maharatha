﻿/**
 * DataStore to perform CRUD operation on Accounts for Map to COA.
 */
Ext.define('Chaching.store.financials.accounts.AccountsMapToCOAStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.AccountsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            organizationUnitId: 0,
            coaId: 0
        },
        api: {
            read: abp.appPath + 'api/services/app/accountUnit/GetAccountListByCoaId',
            update: abp.appPath + 'api/services/app/accountUnit/CreateOrUpdateAccountLinkUnits'
        }
    },
    idPropertyField: 'accountId'//important to set for add/update of records
});
