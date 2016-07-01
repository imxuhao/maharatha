Ext.define('Chaching.model.payables.invoices.AccountsPayableDetailsModel', {
    extend: 'Chaching.model.base.TransactionDetailsModel',
    config: {
        searchEntityName: 'invoices'
    },
    fields: [
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorName', type: 'string' },
        { name: 'poHistoryItemId', type: 'int', defaultValue: null, convert: nullHandler },
    ]
});
