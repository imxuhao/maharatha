/**
 * DataModel to represent entity schema for Accounts Receivable Header for Inquiry screen.
 */
Ext.define('Chaching.model.receivables.inquiry.InquiryModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Inquiry'
    },
    fields: [
        { name: 'customerId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'customerName', type: 'string'},
        { name: 'documentReference', type: 'string' },
        { name: 'transactionDate', type: 'date', dateFormat: 'c' },
        { name: 'dueDate', type: 'date', dateFormat: 'c' },
        { name: 'description', type: 'string' },
        { name: 'jobNumber', type: 'string' },
        { name: 'jobName', type: 'string' },
        { name: 'invoiceStatus', type: 'string' },
        { name: 'invoiceAmount', type: 'int',  defaultValue: null, convert: nullHandler},
        { name: 'dateOfDeposite', type: 'date', dateFormat: 'c' },
        { name: 'payments', type: 'string' },
        { name: 'balanceDue', type: 'string' },
        { name: 'notes', type: 'string' }
    ]
});
