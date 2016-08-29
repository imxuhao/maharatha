/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.ProjectsImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [{
        name: 'jobNumber',
        type: 'string',
        mapping: { source: 'Job #' },
        mandatory: true,
        duplicate: true
    }, {
        name: 'caption',
        type: 'string',
        mapping: { source: 'Job Name' },
        mandatory: true,
        duplicate: true
    }, {
        name: 'typeofProjectName',
        type: 'string',
        mapping: { source: 'Project type' }
    }, {
        name: 'typeofProjectId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Project_Type_Value' }
    }, {
        name: 'budgetFormatCaption',
        type: 'string',
        mapping: { source: 'Budget format' },
        mandatory: true
    }, {
        name: 'chartOfAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Budget_Format_Value' },
        mandatory: true
    }, {
        name: 'accountNumber',//this is rollupaccount
        type: 'string',
        mapping:{source:'RollUp Account'}
    }, {
        name: 'accountId',//this is rollupaccountId
        type: 'int',
        defaultValue: null,
        mapping: { source: 'RollUp_Account_Value' }
    }, {
        name: 'divisionJobNumber',
        type: 'string',
        mapping: { source: 'RollUp Division' }
    }, {
        name: 'rollupJobId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'RollUp_Division_Value' }
    }, {
        name: 'name',//this is tax credit
        type: 'string',
        mapping: { source: 'Tax Credit' }
    }, {
        name: 'taxCreditId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Tax_Credit_Value' }
    }, {
        name: 'jobStatusName',
        type: 'string',
        mapping: { source: 'Status' }
    }, {
        name: 'typeOfJobStatusId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Status_Value' }
    }, {
        name: 'typeOfCurrency',
        type: 'string',
        mapping: { source: 'Currency' }
    }, {
        name: 'typeOfCurrencyId',
        type: 'int',
        mapping: { source: 'Currency_Value' },
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
