/**
 * Model to represent entity schema for Open Statement.
 */
Ext.define('Chaching.model.creditcard.entry.OpenStatementModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'CreditCard'
    },
    fields: [
        { name: 'controllingBankAccountId', type: 'int', defaultValue : null, convert:nullHandler },
        { name: 'description', type: 'string' }, // credit card company
        { name: 'documentDate', type: 'date', dateFormat: 'c' }, //statement date
        { name: 'transactionDate', type: 'date', dateFormat: 'c' },
        { name: 'controlTotal', type: 'float', defaultValue: null, convert: nullHandler }, //statementBalance
        { name: 'isPosted', type: 'boolean' },
        { name: 'status', type: 'string' }
    ]
});
