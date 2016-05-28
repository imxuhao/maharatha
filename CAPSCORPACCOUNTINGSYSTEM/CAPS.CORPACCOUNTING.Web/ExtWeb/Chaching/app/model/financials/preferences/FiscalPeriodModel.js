Ext.define('Chaching.model.financials.preferences.FiscalPeriodModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'fiscalYearId', type: 'int', isPrimaryKey: true },
        { name: 'yearStartDate', type: 'date' },
        { name: 'YearEndDate', type: 'date' },
        { name: 'isYearOpen', type: 'boolean'},
        { name: 'isActive', type: 'boolean' },
        { name: 'isApproved', type: 'boolean'},
        { name: 'typeOfInactiveStatusId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isCpaClosed', type: 'boolean', defaultValue: null },
        { name: 'dateCpaClosed', type: 'date', defaultValue: null},
        { name: 'cpaUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isDefaultReportingYear', type: 'boolean', defaultValue: null, convert: nullHandler }
       ]
});
