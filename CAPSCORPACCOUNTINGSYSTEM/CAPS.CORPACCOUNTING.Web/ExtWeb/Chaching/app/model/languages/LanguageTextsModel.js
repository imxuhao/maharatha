Ext.define('Chaching.model.languages.LanguageTextsModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'id', type: 'int' },
        { name: 'key', type: 'string' },
         { name: 'baseValue', type: 'string' },
         { name: 'targetValue', type: 'string' }

    ]
});