Ext.define('Chaching.store.languages.LanguageTextsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.languages.LanguageTextsModel',  
    proxy: {
        type: 'chachingProxy',
        extraParams: {
            'TargetValueFilter':null,
            'TargetLanguageName':null,
            'BaseLanguageName':null,
            'SourceName': null
        },
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type':'application/json;charset=UTF-8'
        },
        writer: {
            type:'json'
        },
        api: {           
            read: abp.appPath + 'api/services/app/language/GetLanguageTexts',
            update: abp.appPath + 'api/services/app/language/UpdateLanguageText',          
        },
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField: 'id',
   
});