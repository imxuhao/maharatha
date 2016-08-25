/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.AccountsImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [{
        name: 'accountNumber',
        type: 'string',
        mapping: { source: 'Account Number' },
        mandatory: true,
        duplicate:true
    }, {
        name: 'caption',
        type: 'string',
        mapping: { source: 'Description' },
        mandatory: true,
        duplicate: true
    }, {
        name: 'typeOfAccount',
        type: 'string',
        mapping: { source: 'Classification' },
        mandatory: true
    }, {
        name: 'typeOfAccountId',
        type: 'int',
        mapping: { source: 'Classification Value' },
        mandatory: true
    }, {
        name: 'typeofConsolidation',
        type: 'string',
        mapping: { source: 'Consolidation' }
    }, {
        name: 'typeofConsolidationId',
        type: 'int',
        mapping: { source: 'Consolidation Value' }
    },  {
        name: 'isEnterable',
        type: 'boolean',
        mapping: { source: 'Journals Allowed' }
    }, {
        name: 'isRollupAccount',
        type: 'boolean',
        mapping: { source: 'RollUp Account' }
    }, {
        name: 'typeOfCurrency',
        type: 'string',
        mapping: { source: 'Currency' },
        mandatory: true
    }, {
        name: 'typeOfCurrencyId',
        type: 'int',
        mapping: { source: 'Currency Value' },
        mandatory: true
    }, {
        name: 'isElimination',
        type: 'boolean',
        mapping: { source: 'Elimination Account' }
    }, {
        name: 'isAccountRevalued',
        type: 'boolean',
        mapping: { source: 'Multi-Currency Revaluation' }
    },
    {
        name: 'typeOfCurrencyRate',
        type: 'string',
        mapping: { source: 'Rate Type Override' }
    }, {
        name: 'typeOfCurrencyRateId',
        type: 'int',
        mapping: { source: 'Rate Type Override Value' }
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
