Ext.define('Chaching.model.financials.fiscalperiod.FiscalPeriodModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'fiscalPeriodId', type: 'int', isPrimaryKey: true },
        { name: 'fiscalYearId', type: 'int' },
        { name: 'periodStartDate', type: 'date' },
        { name: 'periodEndDate', type: 'date' },
        { name: 'isPeriodOpen', type: 'boolean' },
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
