Ext.define('Chaching.model.languages.LanguagesFlagModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'displayText', type: 'string' },
        { name: 'value', type: 'string' },
        { name: 'isSelected', type: 'boolean' }
    ]
});
