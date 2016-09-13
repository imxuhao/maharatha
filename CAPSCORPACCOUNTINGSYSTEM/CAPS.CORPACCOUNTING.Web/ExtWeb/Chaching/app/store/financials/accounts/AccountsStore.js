/**
 * DataStore to perform CRUD operation on Accounts.
 */
Ext.define('Chaching.store.financials.accounts.AccountsStore', {
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
            create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
            read: abp.appPath + 'api/services/app/accountUnit/GetAccountUnitsByCoaId',
            update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
            destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
        }
    },
    idPropertyField: 'accountId'//important to set for add/update of records
});
