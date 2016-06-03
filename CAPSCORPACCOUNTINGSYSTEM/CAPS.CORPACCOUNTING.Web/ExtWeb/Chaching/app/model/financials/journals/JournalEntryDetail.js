Ext.define('Chaching.model.financials.journals.JournalEntryDetail', {
    extend: 'Chaching.model.base.TransactionDetailsModel',
    config: {
        searchEntityName: 'journals'
    },
    fields:
    [
        { name: 'vendorId', type: 'int',defaultValue: null, convert: nullHandler },
        { name: 'vendorName', type: 'string' },
        { name: 'purchaseOrderItemId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'purchaseOrderItem', type: 'string' },
        { name: 'batchName', type: 'string' },
        { name: 'creditAccountDesc', type: 'string' },
        { name: 'creditAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditJobDesc', type: 'string' },
        { name: 'creditJobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccount1Desc', type: 'string' },
        { name: 'creditSubAccount2Desc', type: 'string' },
        { name: 'creditSubAccount3Desc', type: 'string' },
        { name: 'creditSubAccount4Desc', type: 'string' },
        { name: 'creditSubAccount5Desc', type: 'string' },
        { name: 'creditSubAccount6Desc', type: 'string' },
        { name: 'creditSubAccount7Desc', type: 'string' },
        { name: 'creditSubAccount8Desc', type: 'string' },
        { name: 'creditSubAccount9Desc', type: 'string' },
        { name: 'creditSubAccount10Desc', type: 'string' },
        { name: 'creditSubAccountId1', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId2', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId3', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId4', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId5', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId6', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId7', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId8', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId9', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountId10', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfTransactionId', type: 'int', defaultValue: 1 },
        { name: 'typeOfTransaction', type: 'string', defaultValue: 'APInvoice' },
         { name: 'typeOfAmountId', type: 'int', defaultValue: 1 },
        { name: 'typeOfAmount', type: 'string', defaultValue: 'StandardEntry' }
    ]
});