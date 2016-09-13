/**
 * DataModel to represent CreditCard Security Model.
 */
Ext.define('Chaching.model.users.CreditCardSecurityModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'accountingDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'cardHolderName', type: 'string', width: '8%' },
        { name: 'cardNumber', type: 'string' },
        { name: 'isActive', type: 'boolean', width: '8%' },
        { name: 'wasActive', type: 'boolean' }//used for local operations only
    ]
});

