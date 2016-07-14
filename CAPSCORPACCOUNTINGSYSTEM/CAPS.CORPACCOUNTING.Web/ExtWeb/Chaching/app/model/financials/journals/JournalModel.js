/**
 * DataModel to represent entity schema for Journal Entry Header.
 */
Ext.define('Chaching.model.financials.journals.JournalModel', {
    extend: 'Chaching.model.base.TransactionHeaderModel',
    config: {
        searchEntityName: 'journals'
    },
    fields: [
        { name: 'batchId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isReversingEntry', type: 'boolean' },
        { name: 'dateOfReversal', type: 'date', defaultValue: null, convert: nullHandler },
        { name: 'isRecurringEntry', type: 'boolean' },
        { name: 'dateToRecur', type: 'date', defaultValue: null, convert: nullHandler },
        { name: 'finalDate', type: 'date', defaultValue: null, convert: nullHandler },
        { name: 'lastPostDate', type: 'date', defaultValue: null, convert: nullHandler },
        { name: 'batchInfo', type: 'string' },
        { name: 'isBatchRemoved', type: 'boolean' },
        { name: 'journalTypeId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'journalType', type: 'string' }
    ]
});