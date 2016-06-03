Ext.define('Chaching.model.banking.BankCheckRangesModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'bankAccountPaymentRangeId', type: 'int', isPrimaryKey: true },
        { name: 'bankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'startingPaymentNumber', type: 'string' },
        { name: 'endingPaymentNumber', type: 'string' }
    ]
});
