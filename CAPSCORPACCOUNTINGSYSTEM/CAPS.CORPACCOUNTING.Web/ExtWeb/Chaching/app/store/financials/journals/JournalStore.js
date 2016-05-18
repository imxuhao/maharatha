Ext.define('Chaching.store.financials.journals.JournalStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.journals.JournalModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/journalEntryDocument/CreateJournalEntryDocumentUnit',
            read: abp.appPath + 'api/services/app/journalEntryDocument/GetJournalEntryDocumentUnits',
            update: abp.appPath + 'api/services/app/journalEntryDocument/UpdateJournalEntryDocumentUnit',
            destroy: abp.appPath + 'api/services/app/journalEntryDocument/DeleteJournalEntryDocumentUnit'
        }
    },
    idPropertyField: 'accountingDocumentId'//important to set for add/update of records
});
