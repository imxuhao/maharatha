Ext.define('Chaching.model.financials.journals.JournalEntryDetail', {
    extend: 'Chaching.model.base.TransactionDetailsModel',
    config: {
        searchEntityName: 'journals'
    },
    fields:
        [
            { name: 'vendorId ', type: 'int' },
            { name: 'vendor ', type: 'string' },
            { name: 'purchaseOrderItemId ', type: 'int' },
            { name: 'purchaseOrderItem ', type: 'string' },
            { name: 'batchName ', type: 'string' }
        ]
});