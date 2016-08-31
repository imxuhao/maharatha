/**
 * Model to represent entity schema for Credit Card Company.
 */
Ext.define('Chaching.model.creditcard.entry.CreditCardCompanyModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'creditCardCompany', type: 'string' },
        { name: 'accountName', type: 'string'},
        { name: 'accountNumber', type: 'string'},
        { name: 'batch', type: 'string' }
    ]
});
