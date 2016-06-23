Ext.define('Chaching.model.payables.invoices.AccountsPayableModel', {
    extend: 'Chaching.model.base.TransactionHeaderModel',
    config: {
        searchEntityName: 'Invoices'
    },
    fields: [
         { name: 'batchId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'typeOfInvoiceId', type: 'int', defaultValue: 1, convert: nullHandler },
         { name: 'pettyCashAccountId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'paymentTermId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'typeOfCheckGroupId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'bankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'paymentDate', type: 'date', dateFormat: 'c' },
         { name: 'paymentNumber', type: 'string' },
         { name: 'purchaseOrderReference', type: 'string' },
         { name: 'reversedByUserId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'reversalDate', type: 'date', dateFormat: 'c' },
         { name: 'isInvoiceHistory', type: 'boolean' },
         { name: 'isEnterable', type: 'boolean' },
         { name: 'generatedAccountingDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'uploadDocumentLogID', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'batchInfo', type: 'string' },
         { name: 'paymentSelectedByUserId', type: 'int', defaultValue: null, convert: nullHandler },
         { name: 'batchName', type: 'string' },
         { name: 'vendorName', type: 'string' }
    ]
});