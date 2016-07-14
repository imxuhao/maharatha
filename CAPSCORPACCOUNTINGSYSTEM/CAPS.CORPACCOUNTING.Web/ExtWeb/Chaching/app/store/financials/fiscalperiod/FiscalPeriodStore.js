/**
 * DataStore to perform CRUD operation on Fiscal Periods.
 */
Ext.define('Chaching.store.financials.fiscalperiod.FiscalPeriodStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.fiscalperiod.FiscalPeriodModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            organizationUnitId: null

        },
        api: {
            create: abp.appPath + 'api/services/app/fiscalPeriod/CreateFiscalPeriodUnit',
            read: abp.appPath + 'api/services/app/fiscalYear/GetFiscalPeriodUnits',
            update: abp.appPath + 'api/services/app/fiscalPeriod/UpdateFiscalPeriodUnit',
            destroy: abp.appPath + 'api/services/app/fiscalYear/DeleteFiscalPeriodUnit'
        }
    },
    idPropertyField: 'fiscalPeriodId'//important to set for add/update of records
});
