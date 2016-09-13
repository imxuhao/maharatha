/**
 * DataModel to represent entity schema for Fiscal Year.
 */
Ext.define('Chaching.model.financials.fiscalperiod.FiscalYearModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'FiscalYear'
    },
    fields: [
        { name: 'fiscalYearId', type: 'int', isPrimaryKey: true },
        { name: 'yearStartDate', type: 'date', dateFormat: 'c' },
        { name: 'yearEndDate', type: 'date', dateFormat: 'c' },
        { name: 'isYearOpen', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'isApproved', type: 'boolean' },
        { name: 'typeOfInactiveStatusId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isCpaClosed', type: 'boolean', defaultValue: null },
        { name: 'dateCpaClosed', type: 'date', defaultValue: null },
        { name: 'cpaUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'organizationUnitId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isDefaultReportingYear', type: 'boolean', defaultValue: null, convert: nullHandler }
    ]
});
