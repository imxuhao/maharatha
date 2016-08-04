Ext.define('Chaching.model.editions.EditionsTreeModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'defaultValue', type: 'string' },
        { name: 'displayName', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'description', type: 'string' },
        { name: 'inputType', type: 'auto' }
    ]
});
