Ext.define('Chaching.store.financials.accounts.SubAccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.SubAccountsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            organizationUnitId: 0,
            sorting:'description'
        },
        api: {
            create: abp.appPath + 'api/services/app/subAccountUnit/CreateSubAccountUnit',
            read: abp.appPath + 'api/services/app/subAccountUnit/GetSubAccountUnits',
            update: abp.appPath + 'api/services/app/subAccountUnit/UpdateSubAccountUnit',
            destroy: abp.appPath + 'api/services/app/subAccountUnit/DeleteBankAccountUnit'
        }
    },
    idPropertyField: 'subAccountId'//important to set for add/update of records
});
