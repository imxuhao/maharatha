Ext.define('Chaching.model.banking.banksetup.BankSetupModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'BankAccount'
    },
    fields: [
        { name: 'bankAccountId', type: 'int', isPrimaryKey: true },
        { name: 'displaySequence', type: 'string' },
        { name: 'typeOfBankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'accountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'jobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'bankAccountNumber', type: 'string', headerText: 'Account#', hidden: false, width: '30%', minWidth: 80 },
        { name: 'description', type: 'string', headerText: 'BankName', hidden: false, width: '31%', minWidth: 80 },
        { name: 'bankAccountName', type: 'string', headerText: 'AccountName', hidden: false, width: '31%', minWidth: 80 },
        { name: 'routingNumber', type: 'string' },
        { name: 'typeOfCheckStockId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'lastCheckNumberGenerated', type: 'string' },
        { name: 'controlAccount', type: 'string' },
        { name: 'clearingAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'clearingJobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'expirationMMYYYY', type: 'date', dateFormat: 'c' },
        { name: 'typeOfUploadFileId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'controllingBankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isClosed', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'isApproved', type: 'boolean' },
        { name: 'typeOfInactiveStatusId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'positivePayTypeOfUploadFileId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'positivePayTransmitterInfo', type: 'string' },
        { name: 'pettyCashAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isachEnabled', type: 'boolean' },
        { name: 'achDestinationCode', type: 'string' },
        { name: 'achDestinationName', type: 'string' },
        { name: 'achOriginCode', type: 'string' },
        { name: 'achOriginName', type: 'string' },
        { name: 'batchId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'ccFullAccountNO', type: 'string' },
        { name: 'ccFootNote', type: 'string' },
        { name: 'typeOfBankAccount', type: 'string' },
        { name: 'ledgerAccount', type: 'string' },
        { name: 'job', type: 'string' },
        { name: 'typeofCheckStock', type: 'string' },
        { name: 'clearingAccount', type: 'string' },
        { name: 'clearingJob', type: 'string' },
        { name: 'typeOfUploadFile', type: 'string' },
        { name: 'vendor', type: 'string' },
        { name: 'controllingBankAccounts', type: 'string' },
        { name: 'typeOfInactiveStatus', type: 'string' },
        { name: 'positivePayTypeOfUploadFile', type: 'string' },
        { name: 'pettyCashAccount', type: 'string' },
        { name: 'batch', type: 'string' },
        {
            name: 'address',
            reference: {
                parent: 'address.AddressModel'
            }
        }
        //,
        //{
        //    name: 'bankAccountPaymentRange',
        //    reference: {
        //        parent: 'banking.BankCheckRangesModel'
        //    }
        //}
    ]
});
