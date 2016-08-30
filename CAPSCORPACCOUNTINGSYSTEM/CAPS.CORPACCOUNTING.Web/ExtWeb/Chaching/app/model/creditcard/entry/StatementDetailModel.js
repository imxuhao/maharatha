/**
 * Model to represent entity schema for Open Statement.
 */
Ext.define('Chaching.model.creditcard.entry.StatementDetailModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'bankAccountId', type: 'int', isPrimaryKey: true },
        { name: 'creditCardCompany', type: 'string' },
        { name: 'statementDate', type: 'date', dateFormat: 'c' },
        { name: 'statementBalance', type: 'float', defaultValue: null, convert: nullHandler },
        { name: 'status', type: 'string' },
        { name: 'cardHolder', type: 'string' },
        { name: 'invoiceNumber', type: 'string' },
        { name: 'postingDate', type: 'date', dateFormat: 'c' },
        { name: 'creditCardTotal', type: 'float', defaultValue: null, convert: nullHandler },
        { name: 'apGenerated', type: 'boolean' },
        { name: 'buildAp', type: 'string' },
        { name: 'transactionNumber', type: 'int', defaultValue: null, convert: nullHandler }
    ]
});
