/**
 * DataModel to represent error schema for Imports.
 */
Ext.define('Chaching.model.imports.ImportsErrorModel', {
    extend: 'Ext.data.Model',
    //config: {
    //    searchEntityName: ''
    //},
    fields: [
        //{ name: 'id', type: 'int', isPrimaryKey: true, defaultValue: null, convert: nullHandler },
        { name: 'rowNumber', type: 'string' },
        { name: 'errorMessage', type: 'string' }
    ]
});

