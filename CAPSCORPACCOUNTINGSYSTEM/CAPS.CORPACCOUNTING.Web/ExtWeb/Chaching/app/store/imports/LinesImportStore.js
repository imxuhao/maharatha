/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.LinesImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [
    {
        name: 'accountNumber',
        type: 'string',
        mapping: { source: app.localize("LineNumber") },
        mandatory: true,
        duplicate: true
    },
    {
        name: 'caption',
        type: 'string',
        mapping: { source: app.localize("Caption") },
        mandatory: true,
        duplicate: true
    },
    {
        name: 'typeOfAccount',
        type: 'string',
        mapping: { source: app.localize("Classification") }
    },
    {
        name: 'typeOfAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("ClassificationValue") }
    },
    {
        name: 'typeofConsolidation',
        type: 'string',
        defaultValue: null,
        mapping: { source: app.localize("Consolidation") }
    },
    {
        name: 'typeofConsolidationId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("ConsolidationValue") }
    },
    {
        name: 'isEnterable',
        type: 'boolean',
        mapping: { source: app.localize("JournalsAllowed") }
    },
    {
        name: 'typeOfCurrency',
        type: 'string',
        mapping: { source: app.localize("Currency") }
    },
    {
        name: 'typeOfCurrencyId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("CurrencyValue") }
    },
    {
        name: 'rollUpAccountCaption',
        type: 'string',
        mapping: { source: app.localize("RollUpAccount") }
    },
    {
        name: 'rollupAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("RollUpAccountValue") }
    },
    {
        name: 'rollUpDivision',
        type: 'string',
        mapping: { source: app.localize("RollUpDivision") }
    },
    {
        name: 'rollupJobId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("RollUpDivisionValue") }
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
