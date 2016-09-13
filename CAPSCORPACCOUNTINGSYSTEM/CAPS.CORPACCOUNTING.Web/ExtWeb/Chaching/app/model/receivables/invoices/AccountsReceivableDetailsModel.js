/**
 * DataModel to represent entity schema for Accounts Receivable Distribution.
 */
Ext.define('Chaching.model.receivables.invoices.AccountsReceivableDetailsModel', {
    extend: 'Chaching.model.base.TransactionDetailsModel',
    config: {
        searchEntityName: 'invoices'
    },
    fields: [
        { name: 'customerId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'customerName', type: 'string' },
        { name: 'poHistoryItemId', type: 'int', defaultValue: null, convert: nullHandler }
    ]
});
