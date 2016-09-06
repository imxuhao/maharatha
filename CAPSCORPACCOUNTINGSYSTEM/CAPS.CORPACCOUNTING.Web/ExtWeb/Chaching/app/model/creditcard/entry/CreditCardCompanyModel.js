/**
 * Model to represent entity schema for Credit Card Company.
 */
Ext.define('Chaching.model.creditcard.entry.CreditCardCompanyModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'bankAccountId', type: 'int', isPrimaryKey : true, defaultValue : null, convert : nullHandler },
        { name: 'description', type: 'string' },
        { name: 'bankAccountName', type: 'string' },
        { name: 'bankAccountNumber', type: 'string' },
        { name: 'batch', type: 'string' },
        { name: 'typeOfBankAccountId', type: 'int' },
        { name: 'typeOfBankAccount', type: 'string' },
        { name: 'clearingAccountId', type: 'int' },
        { name: 'jobId', type: 'int' },
        { name: 'vendorId', type: 'int' },
        { name: 'typeOfUploadFileId', type: 'int' },
        { name: 'batchId', type: 'int' },
        { name: 'isClosed', type: 'boolean' }
    ]
});
