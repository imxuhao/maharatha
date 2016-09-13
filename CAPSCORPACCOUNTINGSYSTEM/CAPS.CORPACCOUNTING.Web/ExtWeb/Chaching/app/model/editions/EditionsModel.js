/**
 * DataModel to represent entity schema for Editions.
 */
Ext.define('Chaching.model.editions.EditionsModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'id', type: 'int' },
        { name: 'actions', type: 'string' },
        { name: 'displayName', type: 'string' }
    ]
});
