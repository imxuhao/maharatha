Ext.define('Chaching.store.languages.LanguagesDataStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.languages.LanguageMainModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        writer: {
            type: 'json'
        },
        api: {
            read: abp.appPath + 'api/services/app/language/GetLanguageForEdit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField: 'id'

});