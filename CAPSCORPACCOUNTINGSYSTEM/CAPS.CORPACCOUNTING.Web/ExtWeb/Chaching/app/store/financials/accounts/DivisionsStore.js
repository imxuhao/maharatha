/**
 * DataStore to perform CRUD operation on Divisions.
 */
Ext.define('Chaching.store.financials.accounts.DivisionsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.DivisionsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
            read: abp.appPath + 'api/services/app/divisionUnit/GetDivisionUnits',
            update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
            destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
        }
    },
    idPropertyField: 'jobId'//important to set for add/update of records
});
