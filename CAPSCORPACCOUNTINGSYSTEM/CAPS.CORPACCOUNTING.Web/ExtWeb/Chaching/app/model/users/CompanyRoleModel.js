Ext.define('Chaching.model.users.CompanyRoleModel', {
    extend: 'Chaching.model.base.BaseModel',
    fields: [
        { name: 'id', type: 'int', isPrimaryKey: true, defaultValue: null, convert: nullHandler },
        { name: 'roleId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'tenantName', type: 'string' },
        { name: 'roleDisplayName', type: 'string' },
        { name: 'roleName', type: 'string' },
        { name: 'isEmptyRoles', type: 'bool' }
    ]
});

