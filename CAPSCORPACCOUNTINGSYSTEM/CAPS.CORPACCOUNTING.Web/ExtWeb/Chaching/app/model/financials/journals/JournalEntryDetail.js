/**
 * DataModel to represent entity schema for Journal Entry Distribution.
 */
Ext.define('Chaching.model.financials.journals.JournalEntryDetail', {
    extend: 'Chaching.model.base.TransactionDetailsModel',
    config: {
        searchEntityName: 'journals'
    },
    fields:
    [
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorName', type: 'string' },
        { name: 'batchName', type: 'string' },
        { name: 'debitCreditGroup', type: 'string' },
        { name: 'creditAccountingItemId', type: 'auto' },
        { name: 'creditAccountNumber', type: 'string' },
        { name: 'creditAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditJobNumber', type: 'string' },
        { name: 'creditJobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creditSubAccountNumber1', type: 'string' },
        { name: 'creditSubAccountNumber2', type: 'string' },
        { name: 'creditSubAccountNumber3', type: 'string' },
        { name: 'creditSubAccountNumber4', type: 'string' },
        { name: 'creditSubAccountNumber5', type: 'string' },
        { name: 'creditSubAccountNumber6', type: 'string' },
        { name: 'creditSubAccountNumber7', type: 'string' },
        { name: 'creditSubAccountNumber8', type: 'string' },
        { name: 'creditSubAccountNumber9', type: 'string' },
        { name: 'creditSubAccountNumber10', type: 'string' },
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
        { name: 'debitAccountingItemId', type: 'int', defaultValue: null, convert: nullHandler }
    ]
});