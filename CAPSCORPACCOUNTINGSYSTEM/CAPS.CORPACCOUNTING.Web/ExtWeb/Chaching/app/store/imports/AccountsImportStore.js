/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.AccountsImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [{
        name: 'accountNumber',
        type: 'string',
        mapping: { source: app.localize("AccountNumber") },
        mandatory: true,
        duplicate:true
    }, {
        name: 'caption',
        type: 'string',
        mapping: { source: app.localize("Description") },
        mandatory: true,
        duplicate: true
    }, {
        name: 'typeOfAccount',
        type: 'string',
        mapping: { source: app.localize("Classification") },
        mandatory: true
    }, {
        name: 'typeOfAccountId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("ClassificationValue") }
    }, {
        name: 'typeofConsolidation',
        type: 'string',
        mapping: { source: app.localize("Consolidation") }
    }, {
        name: 'typeofConsolidationId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("ConsolidationValue") }
    },  {
        name: 'isEnterable',
        type: 'boolean',
        mapping: { source: app.localize("JournalsAllowed") }
    }, {
        name: 'isRollupAccount',
        type: 'boolean',
        mapping: { source: app.localize("RollUpAccount") }
    }, {
        name: 'typeOfCurrency',
        type: 'string',
        mapping: { source: app.localize("Currency") }
    }, {
        name: 'typeOfCurrencyId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("CurrencyValue") }
    }, {
        name: 'isElimination',
        type: 'boolean',
        mapping: { source: app.localize("EliminationAccount") }
    }, {
        name: 'isAccountRevalued',
        type: 'boolean',
        mapping: { source: app.localize("Multi-CurrencyReval") }
    },
    {
        name: 'typeOfCurrencyRate',
        type: 'string',
        mapping: { source: app.localize("RateTypeOverride") }
    }, {
        name: 'typeOfCurrencyRateId',
        type: 'int',
        defaultValue: null,
        mapping: { source: app.localize("RateTypeOverrideValue") }
    },
    {
        name: 'errorMessage',
        type:'string'
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
