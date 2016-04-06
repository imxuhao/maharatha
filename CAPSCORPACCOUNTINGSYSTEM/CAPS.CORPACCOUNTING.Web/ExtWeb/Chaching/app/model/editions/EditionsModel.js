Ext.define('Chaching.model.editions.EditionsModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'id', type: 'int' },
        { name: 'actions', type: 'string' },       
         { name: 'displayName', type: 'string' },
          { name: 'creationTime', type: "date", format: 'Y/m/d H:i:s' }

    ]
});
