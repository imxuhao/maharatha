Ext.define('Chaching.store.utilities.JournalTypeListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'journalType', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'journalTypeId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/journalEntryDocument/GetJournalTypeList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
