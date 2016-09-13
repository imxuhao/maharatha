/**
 * DataStore to perform CRUD operation on Languages Texts.
 */
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
            read: abp.appPath + 'api/services/app/language/GetLanguageTextUnits',
            update: abp.appPath + 'api/services/app/language/UpdateLanguageText',
            create: abp.appPath + 'api/services/app/language/UpdateLanguageText'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField: 'id',
   listeners: {
       beforeload:function(store, operation, eOpts) {
           var proxy = operation.getProxy(),
               params = proxy.getExtraParams();
           if (params && (!params.TargetValueFilter || !params.TargetLanguageName || !params.BaseLanguageName || !params.SourceName)) {
               return false;
           }
       }
   }
});