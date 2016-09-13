/**
 * Model to represent entity schema for Credit Card Company.
 */
Ext.define('Chaching.model.creditcard.entry.CreditCardCompanyModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'BankAccount'
    },
    fields: [
        { name: 'bankAccountId', type: 'int', isPrimaryKey : true, defaultValue : null, convert : nullHandler },
        { name: 'description', type: 'string' },
        { name: 'bankAccountName', type: 'string' },
        { name: 'bankAccountNumber', type: 'string' },
        { name: 'batchDesc', type: 'string' },
        { name: 'typeOfBankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfBankAccount', type: 'string' },
        { name: 'clearingAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'jobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfUploadFileId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'batchId', type: 'int',defaultValue: null, convert: nullHandler  },
        { name: 'isClosed', type: 'boolean' }
    ]
});
