/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.DivisionsImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [
    {
        name: 'jobNumber',
        type: 'string',
        mapping: { source: 'Number' },
        mandatory: true,
        duplicate: true
    },
    {
        name: 'caption',
        type: 'string',
        mandatory: true,
        duplicate: true,
        mapping: { source: app.localize("Description") }
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
        name: 'isActive',
        type: 'boolean',
        mapping: { source: app.localize("Active") }
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
