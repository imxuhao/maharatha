/**
 * DataStore to perform Read operation on Accounts Resrictions.
 */
Ext.define('Chaching.store.users.SecurityAccessStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.users.SecurityAccessModel',
    remoteFilter:false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            chartOfAccountId: 0,
            userId:0,
            entityClassificationId: 0,
            organizationUnitId: 0
        },
        api: {
            read: abp.appPath + 'api/services/app/userSecuritySettings/GetAccountAccessList'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField: 'chartOfAccountId'//important to set for add/update of records
});
