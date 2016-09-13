/**
 * DataModel to represent entity schema for Fiscal Periods.
 */
Ext.define('Chaching.model.financials.fiscalperiod.FiscalPeriodModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'fiscalPeriodId', type: 'int', isPrimaryKey: true },
        { name: 'fiscalYearId', type: 'int' },
        { name: 'periodStartDate', type: 'date',dateFormat : 'c' },
        { name: 'periodEndDate', type: 'date', dateFormat: 'c' },
        {name: 'isClose', type: 'boolean', convert: nullHandler },
        { name: 'isActive', type: 'boolean' },
        { name: 'isApproved', type: 'boolean'},
        { name: 'typeOfInactiveStatusId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isCpaClosed', type: 'boolean', defaultValue: null },
        { name: 'dateCpaClosed', type: 'date', defaultValue: null},
        { name: 'cpaUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isYearEndAdjustmentsAllowed', type: 'boolean', defaultValue: null, convert: nullHandler },
        { name: 'isPreClose', type: 'boolean', defaultValue: null, convert: nullHandler },
        { name: 'organizationUnitId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'monthYear', type: 'string'}
       ]
});
