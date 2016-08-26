/**
 * Local file store to import accounts.
 */
Ext.define('Chaching.store.imports.ProjectsImportStore', {
    extend: 'Gearbox.data.file.Store',
    fields: [{
        name: 'jobNumber',
        type: 'string',
        mapping: { source: app.localize('JobNumber') },
        mandatory: true,
        duplicate: true
    }, {
        name: 'caption',
        type: 'string',
        mapping: { source: app.localize('JobName') },
        mandatory: true,
        duplicate: true
    }, {
        name: 'typeofProjectName',
        type: 'string',
        mapping: { source: app.localize('ProjectType') },
        mandatory: true
    }, {
        name: 'typeofProjectNameId',
        type: 'int',
        mapping: { source: app.localize('ProjectType') + ' Value' },
        mandatory: true
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
