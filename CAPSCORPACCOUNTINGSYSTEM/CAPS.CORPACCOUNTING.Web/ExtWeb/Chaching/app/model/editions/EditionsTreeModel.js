Ext.define('Chaching.model.editions.EditionsTreeModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'defaultValue', type: 'int' },
        { name: 'displayName', type: 'string' },
        { name: 'inputType'}
    ]
});
