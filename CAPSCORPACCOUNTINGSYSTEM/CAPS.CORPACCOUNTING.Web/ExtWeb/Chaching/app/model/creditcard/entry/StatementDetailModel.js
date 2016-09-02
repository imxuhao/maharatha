/**
 * Model to represent entity schema for all unposted Statement.
 */
Ext.define('Chaching.model.creditcard.entry.StatementDetailModel', {
    extend: 'Chaching.model.base.TransactionHeaderModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'batchId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'batchName', type: 'string' },
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorName', type: 'string' },
        { name: 'typeOfInvoiceId', type: 'int' },
        { name: 'bankAccountId', type: 'int' },
        { name: 'bankAccount', type: 'string' },
        { name: 'isEnterable', type: 'boolean' },
        { name: 'apInvoiceAccountingDocId', type: 'int' },
        { name: 'uploadDocumentLogId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'uploadDocumentLog', type: 'string' },
        { name: 'isApInvoiceGenSelected', type: 'boolean' },
        { name: 'buildAP', type: 'string' },
        { name: 'aPGenerated', type: 'string' }
    ]
});
