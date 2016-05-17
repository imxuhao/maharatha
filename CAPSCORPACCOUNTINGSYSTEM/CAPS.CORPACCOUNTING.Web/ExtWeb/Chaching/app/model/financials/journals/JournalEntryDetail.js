Ext.define('Chaching.model.financials.journals.JournalEntryDetail', {
    extend: 'Chaching.model.financials.AccountingItemModel',
    config: {
        searchEntityName: ''
    },
    fields:
        [{ name: 'vendorId ', type: 'int' },
        { name: 'purchaseOrderItemId ', type: 'int' },
          { name: 'batchName ', type: 'string' }
    ]
});