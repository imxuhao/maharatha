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
        mapping: { source: 'Description' },
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
        mapping: { source: 'Classification Value' }
    },
    {
        name: 'typeofConsolidation',
        type: 'string',
        mapping: { source: 'Consolidation' }
    },
    {
        name: 'typeofConsolidationId',
        type: 'int',
        mapping: { source: 'Consolidation Value' }
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
        mapping: { source: 'Currency Value' }
    }, 
    {
        name: 'rollUpAccountCaption',
        type: 'string',
        mapping: { source: 'RollUp Account' }
    },
    {
        name: 'accountId',
        type: 'int',
        mapping: { source: 'RollUp Account Value' }
    },
    {
        name: 'rollUpDivision',
        type: 'string',
        mapping: { source: 'RollUp Division Value' }
    },
    {
        name: 'rollupJobId',
        type: 'int',
        mapping: { source: 'RollUp Job Value' }
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
