Ext.define('Chaching.model.languages.LanguagesModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'id', type: 'int' },
        { name: 'actions', type: 'string' },       
         { name: 'displayName', type: 'string' },
            { name: 'name', type: 'string' },
          { name: 'creationTime', type: "date", format: 'Y/m/d H:i:s' },
          { name: 'icon', type: "string" },
            { name: 'isdefault', type: "boolean" }

    ]
});
