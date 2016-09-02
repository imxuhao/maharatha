/**
 * DataModel to represent entity schema for Classifications.
 */
Ext.define('Chaching.model.financials.accounts.ClassificationsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'typeofaccounts'
    },
    fields: [
            { name: 'typeOfAccountId', type: 'int', isPrimaryKey: true },
            { name: 'description', type: 'string' },
            { name: 'caption', type: 'string' },
            { name: 'typeOfAccountClassificationId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'typeOfAccountClassificationDesc', type: 'string' },
            { name: 'isCurrencyCodeRequired', type: 'boolean' },
            { name: 'isPaymentType', type: 'boolean' },
            { name: 'notes', type: 'string' }
           
    ]
});