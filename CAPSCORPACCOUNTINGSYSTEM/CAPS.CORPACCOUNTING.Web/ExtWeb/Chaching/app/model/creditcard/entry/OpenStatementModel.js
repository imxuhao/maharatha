/**
 * Model to represent entity schema for Open Statement.
 */
Ext.define('Chaching.model.creditcard.entry.OpenStatementModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'bankAccountId', type: 'int', isPrimaryKey: true },
        { name: 'creditCardCompany', type: 'string' },
        { name: 'statementDate', type: 'date', dateFormat: 'c' },
        { name: 'statementBalance', type: 'float', defaultValue: null, convert: nullHandler },
        { name: 'status', type: 'string' }
    ]
});
