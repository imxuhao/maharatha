/**
 * DataModel to represent entity schema for Roles.
 */
Ext.define('Chaching.model.roles.RoleEditModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
            { name: 'id', type: 'int' },          
            { name: 'displayName', type: 'string' },           
            { name: 'isDefault', type: 'boolean' }
    ]
});

