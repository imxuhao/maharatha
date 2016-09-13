/**
 * DataModel to represent entity schema for Tenants users.
 */
Ext.define('Chaching.model.tenants.TenantUserModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'name', type: 'string' },
        { name: 'value', type: 'int', defaultValue: null, convert: nullHandler }
    ]
});
