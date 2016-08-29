/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.LinesImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [
    {
        name: 'accountNumber',
        type: 'string',
        mapping: { source: 'Line#' },
        mandatory: true,
        duplicate: true
    },
    {
        name: 'caption',
        type: 'string',
        mapping: { source: 'Caption' },
        mandatory: true,
        duplicate: true
    },
    {
        name: 'typeOfAccount',
        type: 'string',
        mapping: { source: 'Classification' }
    },
    {
        name: 'typeOfAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Classification_Value' }
    },
    {
        name: 'typeofConsolidation',
        type: 'string',
        defaultValue: null,
        mapping: { source: 'Consolidation' }
    },
    {
        name: 'typeofConsolidationId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Consolidation_Value' }
    },
    {
        name: 'isEnterable',
        type: 'boolean',
        mapping: { source: 'Journals Allowed' }
    },
    {
        name: 'typeOfCurrency',
        type: 'string',
        mapping: { source: 'Currency' }
    },
    {
        name: 'typeOfCurrencyId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'Currency_Value' }
    },
    {
        name: 'rollUpAccountCaption',
        type: 'string',
        mapping: { source: 'RollUp Account' }
    },
    {
        name: 'rollupAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'RollUp_Account_Value' }
    },
    {
        name: 'rollUpDivision',
        type: 'string',
        mapping: { source: 'RollUp Division' }
    },
    {
        name: 'rollupJobId',
        type: 'int',
        defaultValue: null,
        mapping: { source: 'RollUp_Division_Value' }
    },
    {
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
