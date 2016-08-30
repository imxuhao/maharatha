/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.ProjectsImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [{
        name: 'jobNumber',
        type: 'string',
        mapping: { source: app.localize("JobNumber") },
        mandatory: true,
        duplicate: true
    }, {
        name: 'caption',
        type: 'string',
        mapping: { source: app.localize("JobName") },
        mandatory: true,
        duplicate: true
    }, {
        name: 'typeofProjectName',
        type: 'string',
        mapping: { source: app.localize("ProjectType") }
    }, {
        name: 'typeofProjectId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("ProjectTypeValue") }
    }, {
        name: 'budgetFormatCaption',
        type: 'string',
        mapping: { source: app.localize("BudgetFormat") },
        mandatory: true
    }, {
        name: 'chartOfAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("BudgetFormatValue") },
        mandatory: true
    }, {
        name: 'accountNumber',//this is rollupaccount
        type: 'string',
        mapping: { source: app.localize("RollUpAccount") }
    }, {
        name: 'accountId',//this is rollupaccountId
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("RollUpAccountValue") }
    }, {
        name: 'divisionJobNumber',
        type: 'string',
        mapping: { source: app.localize("RollUpDivision") }
    }, {
        name: 'rollupJobId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("RollUpDivisionValue") }
    }, {
        name: 'name',//this is tax credit
        type: 'string',
        mapping: { source: app.localize("TaxCredit") }
    }, {
        name: 'taxCreditId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("TaxCreditValue") }
    }, {
        name: 'jobStatusName',
        type: 'string',
        mapping: { source: app.localize("Status") }
    }, {
        name: 'typeOfJobStatusId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("StatusValue") }
    }, {
        name: 'typeOfCurrency',
        type: 'string',
        mapping: { source: app.localize("Currency") }
    }, {
        name: 'typeOfCurrencyId',
        type: 'int',
        mapping: { source: app.localize("CurrencyValue") },
        defaultValue:null
    }, {
        name: 'errorMessage',
        type: 'string'
    }],
    autoGuessMapping: false,
    proxy: {
        type: 'file',
        reader: {
            type: 'file.xlsx'
        },
        writer: 'file.xlsx'
    }
});
