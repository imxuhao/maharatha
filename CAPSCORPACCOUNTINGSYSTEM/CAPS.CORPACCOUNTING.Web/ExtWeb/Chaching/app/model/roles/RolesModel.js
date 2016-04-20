Ext.define('Chaching.model.roles.RolesModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
            {
                name: 'id', type: 'int', convert: function (value, record) {
                    return value;
                }
            },
            { name: 'name', type: 'string' },
            { name: 'displayName', type: 'string' },
            { name: 'isStatic', type: 'boolean' },
            { name: 'isDefault', type: 'boolean' },
            { name: 'createTime', type: "date", format: 'Y-m-d' },
           // { name: 'grantedPermissionNames', type: "auto" },
          //  { name: 'Roles', reference: 'roles.RolesModel' },
           // { name: 'permissions', reference: 'roles.RolesModel' }
    ],
    idProperty: 'id'
   
});

