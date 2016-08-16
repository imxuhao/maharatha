/**
 * DataModel to represent error schema for Imports.
 */
Ext.define('Chaching.model.imports.ErrorModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'id', type: 'int', isPrimaryKey: true, defaultValue: null, convert: nullHandler },
        { name: 'rowNumber', type: 'string' },
        { name: 'errorMessage', type: 'string' }
    ]
});

