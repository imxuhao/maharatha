/**
 * DataStore to perform CRUD operation on Petty Cash.
 */
Ext.define('Chaching.store.pettycash.entry.PettyCashStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.pettycash.entry.PettyCashModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/pettyCashEntryDocument/CreatePettyCashEntryDocumentUnit',
            read: abp.appPath + 'api/services/app/pettyCashEntryDocument/GetPettyCashEntryDocumentUnits',
            update: abp.appPath + 'api/services/app/pettyCashEntryDocument/UpdatePettyCashEntryDocumentUnit',
            destroy: abp.appPath + 'api/services/app/pettyCashEntryDocument/DeletePettyCashEntryDocumentUnit'
        }
    },
    idPropertyField: 'accountingDocumentId'//important to set for add/update of records
});