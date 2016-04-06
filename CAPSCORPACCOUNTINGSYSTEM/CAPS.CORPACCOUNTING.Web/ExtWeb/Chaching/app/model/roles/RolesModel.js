Ext.define('Chaching.model.roles.RolesModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
            { name: 'id', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'displayName', type: 'string' },
            { name: 'isStatic', type: 'boolean' },
            { name: 'isDefault', type: 'boolean' },
            { name: 'createTime', type: "date", format: 'Y-m-d' }
    ]
});

