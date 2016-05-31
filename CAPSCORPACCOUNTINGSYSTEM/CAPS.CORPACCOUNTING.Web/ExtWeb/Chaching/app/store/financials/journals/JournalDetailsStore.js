Ext.define('Chaching.store.financials.journals.JournalDetailsStore', {
    extend: 'Chaching.store.base.TransactionDetailsStore',
    model: 'Chaching.model.financials.journals.JournalEntryDetail',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            accountingDocumentId:null
        },
        api: {
            create: abp.appPath + 'api/services/app/journalEntryDocDetail/JournalEntryDocumentTransactionUnit',
            read: abp.appPath + 'api/services/app/journalEntryDocDetail/GetJournalEntryDocDetailsByAccountingDocId',
            update: abp.appPath + 'api/services/app/journalEntryDocDetail/JournalEntryDocumentTransactionUnit',
            destroy: abp.appPath + 'api/services/app/journalEntryDocDetail/DeleteJournalEntryDocDetailUnit'
        }
    },
    serverKeyName:'updateJournalEntryDocDetailList',
    idPropertyField: 'accountingItemId'//important to set for add/update of records
});
