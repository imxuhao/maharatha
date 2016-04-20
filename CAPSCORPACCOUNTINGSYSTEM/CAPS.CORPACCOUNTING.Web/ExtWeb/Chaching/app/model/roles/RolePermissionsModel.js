Ext.define('Chaching.model.roles.RolePermissionsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [          
            { name: 'parentName', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'displayName', type: 'string' },
            { name: 'description', type: 'string' },
            { name: 'isGrantedByDefault', type: 'boolean' }
    ],
   
});

