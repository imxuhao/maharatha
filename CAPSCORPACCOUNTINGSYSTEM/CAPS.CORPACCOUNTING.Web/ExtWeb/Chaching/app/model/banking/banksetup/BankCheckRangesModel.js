/**
 * DataModel to represent entity schema for Bank Check Ranges.
 */
Ext.define('Chaching.model.banking.banksetup.BankCheckRangesModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'bankAccountPaymentRangeId', type: 'int', isPrimaryKey: true },
        { name: 'bankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'startingPaymentNumber', type: 'string' },
        { name: 'endingPaymentNumber', type: 'string' }
    ]
});
