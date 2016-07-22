/**
 * DataStore to perform CRUD operation on Languages.
 */
Ext.define('Chaching.store.languages.LanguagesStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.languages.LanguagesModel',  
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type':'application/json;charset=UTF-8'
        },
        writer: {
            type:'json'
        },
        api: {
            create: abp.appPath + 'api/services/app/language/CreateOrUpdateLanguage',
            read: abp.appPath + 'api/services/app/language/GetLanguages',
            update: abp.appPath + 'api/services/app/language/CreateOrUpdateLanguage',
            destroy: abp.appPath + 'api / services / app / language / DeleteLanguage'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField: 'id'
   
});