/**
 * DataStore to perform Read operation on Accounts Restricted.
 */
Ext.define('Chaching.store.financials.accounts.AccountRestrictionRightStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.AccountRestrictionsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            organizationUnitId: 0,
            subAccountId: 0
        },
        api: {
            
            read: abp.appPath + 'api/services/app/subAccountUnit/GetAccountRestrictionList'
            
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        },
    },
    idPropertyField: 'SubAccountRestrictionId'//important to set for add/update of records
});
