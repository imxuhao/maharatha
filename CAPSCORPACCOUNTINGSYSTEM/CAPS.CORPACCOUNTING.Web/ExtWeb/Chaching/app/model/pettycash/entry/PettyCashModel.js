Ext.define('Chaching.model.pettycash.entry.PettyCashModel', {
    extend: 'Chaching.model.base.TransactionHeaderModel',
    config: {
        searchEntityName: 'PettyCash'
    },
    fields: [
       { name: 'batchId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'typeOfInvoiceId', type: 'int', defaultValue: 1, convert: nullHandler },
       { name: 'pettyCashAccountId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'paymentTermId', type: 'int', defaultValue: null, convert: nullHandler },
       //{ name: 'typeOfCheckGroupId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'bankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'bankAccount', type: 'string' },
       //{ name: 'paymentDate', type: 'date', dateFormat: 'c' },
       //{ name: 'paymentNumber', type: 'string' },
       { name: 'dueDate', type: 'date', dateFormat: 'c' },
       { name: 'purchaseOrderReference', type: 'string' },
       { name: 'reversedByUserId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'reversalDate', type: 'date', dateFormat: 'c' },
       //{ name: 'isInvoiceHistory', type: 'boolean' },
       //{ name: 'isEnterable', type: 'boolean' },
       //{ name: 'generatedAccountingDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'uploadDocumentLogID', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'batchInfo', type: 'string' },
       //{ name: 'paymentSelectedByUserId', type: 'int', defaultValue: null, convert: nullHandler },
       { name: 'batchName', type: 'string' },
       { name: 'vendorName', type: 'string' },
       {name:'lastName',type:'string'},
       //{ name: 'adjustInvoice', type: 'string' },
        { name: 'reimbursementAmount', type: 'string' },
        { name: 'advanceAmount', type: 'float' },
        { name: 'typeOfPaymentMethodId',type:'int',defaultValue:null,convert:nullHandler }
    ]
});
